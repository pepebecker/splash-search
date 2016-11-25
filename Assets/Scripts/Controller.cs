using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Controller : MonoBehaviour
{

	public GameObject entryObject;
	public GameObject contentObject;
	public InputField inputField;
	public RectTransform detailView;
	public TextAsset dbJSON;

	private List<Dictionary<string, string>> users = new List<Dictionary<string, string>>();
	private bool animatig;
	private float positionX;
	private float speed = 0.1f;
	private string mode = "open";
	private bool cleared;

	void Start()
	{
		detailView.anchorMin = new Vector2(1, 0);
		detailView.anchorMax = new Vector2(2, 1);
		positionX = 1;

		LoadJSON();

		PopulateList();
	}

	void LoadJSON()
	{
		var data = JSON.Parse(dbJSON.text)["users"];

		for (int i = 0; i < data.Count; i++)
		{
			Dictionary<string, string> user = new Dictionary<string, string>();

			user.Add("username", data[i]["username"]);
			user.Add("likes", data[i]["likes"]);
			user.Add("picture", data[i]["picture"]);

			users.Add(user);
		}

		users.Sort(delegate (Dictionary<string, string> a, Dictionary<string, string> b)
		{
			int likesA = int.Parse(a["likes"]);
			int likesB = int.Parse(b["likes"]);
			return likesB.CompareTo(likesA);
		});
	}

	public void PopulateList()
	{
		StartCoroutine("AsyncPopulateList");
	}

	IEnumerator AsyncPopulateList()
	{
		foreach (Transform entry in contentObject.transform)
		{
			Destroy(entry.gameObject);
		}

		for (int i = 0; i < users.Count; i++)
		{
			GameObject entry = (GameObject)Instantiate(entryObject, Vector3.zero, Quaternion.identity, contentObject.transform);
			entry.GetComponent<EntryController>().setUsername(users[i]["username"]);
			entry.GetComponent<EntryController>().setLikes(users[i]["likes"]);
			entry.GetComponent<EntryController>().setPicture(users[i]["picture"]);
		}

		yield return null;
	}

	public void UpdateList(string input = "")
	{
		StartCoroutine("AsyncUpdateList", input);
	}

	IEnumerator AsyncUpdateList(string input = "")
	{
		if (cleared) yield return null;

		input = input.ToLower();

		foreach (Transform entry in contentObject.transform)
		{
			string username = entry.GetComponent<EntryController>().getUsername().ToLower();

			if (username.Contains(input))
			{
				entry.gameObject.SetActive(true);
			}
			else {
				entry.gameObject.SetActive(false);
			}
		}

		yield return null;
	}

	public void ClearInput()
	{
		cleared = true;
		//inputField.Select();
		inputField.text = "";
	}

	public void ShowDetailView()
	{
		mode = "open";
		positionX = 1;
		detailView.anchorMin = new Vector2(1, 0);
		detailView.anchorMax = new Vector2(2, 1);
		animatig = true;
	}

	public void HideDetailView()
	{
		mode = "close";
		positionX = 0;
		detailView.anchorMin = new Vector2(0, 0);
		detailView.anchorMax = new Vector2(1, 1);
		animatig = true;
	}

	void FixedUpdate()
	{
		if (animatig)
		{
			if (mode == "open")
			{
				if (positionX > 0)
				{
					positionX -= speed;
				}
				else {
					positionX = 0;
					animatig = false;
				}
			}

			if (mode == "close")
			{
				if (positionX < 1)
				{
					positionX += speed;
				}
				else {
					positionX = 1;
					animatig = false;
				}
			}

			detailView.anchorMin = new Vector2(positionX, 0);
			detailView.anchorMax = new Vector2(positionX + 1, 1);
		}
	}
}
