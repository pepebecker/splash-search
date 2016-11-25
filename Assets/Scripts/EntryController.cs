using UnityEngine;
using UnityEngine.UI;

public class EntryController : MonoBehaviour
{
	public Text usernameText;
	public Text likesText;
	public Image picture;

	private string username;
	private string likes;

	public void setUsername(string username)
	{
		this.username = username;
		this.usernameText.text = username;
	}

	public string getUsername()
	{
		return this.username;
	}

	public void setLikes(string likes)
	{
		this.likes = likes;
		this.likesText.text = likes;
	}

	public string getLikes()
	{
		return this.likes;
	}

	public void setPicture(string file)
	{
		string path = "Profiles/" + file;
		Texture2D texture = (Texture2D)Resources.Load(path, typeof(Texture2D));
		picture.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
	}

	public void ShowDetailView()
	{
		DetailController.Instance.setUsername(usernameText.text);
		DetailController.Instance.setLikes(likesText.text);
		DetailController.Instance.setPicture(picture.sprite);
		transform.root.SendMessage("ShowDetailView", SendMessageOptions.DontRequireReceiver);
	}
}
