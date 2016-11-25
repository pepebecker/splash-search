using UnityEngine;
using UnityEngine.UI;

public class DetailController : MonoBehaviour
{
	public static DetailController Instance;

	public Text usernameText;
	public Text likesText;
	public Image picture;

	void Main()
	{
		Instance = this;
	}

	public void setUsername(string name)
	{
		this.usernameText.text = name;
	}

	public void setLikes(string likes)
	{
		this.likesText.text = likes;
	}

	public void setPicture(Sprite sprite)
	{
		this.picture.sprite = sprite;
	}
}
