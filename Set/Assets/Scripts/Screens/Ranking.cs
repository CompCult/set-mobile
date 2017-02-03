using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Ranking : GenericScreen 
{
	public GameObject userCard, userEmblem, xpEmblem;
	public Text userPlace, userName, userLevel, userXP;

	// Cards used to replace sprits using the
	public Sprite fpPlate, spPlate, tpPlate, normalPlate,
	fpEmblem, spEmblem, tpEmblem, normalEmblem;

	public List<User> userList;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Home";

		ReceiveRanking();
	}

	private void ReceiveRanking()
	{
		WWW rankingRequest = Authenticator.RequestRanking();

		string Response = rankingRequest.text,
		Error = rankingRequest.error;

		if (Error == null)
		{
			FillRanking(Response);
			CreatePlayerCards();
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("Falha ao receber o Ranking", 1);
			LoadBackScene();
		}
	}

	private void FillRanking(string ranking)
    {
		string[] rankingJSON = ranking.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
     	userList = new List<User>();

		foreach (string userJSON in rankingJSON)
        {
			User user = UsrManager.CreateUserFromJSON(userJSON);
        	userList.Add(user);

        	Debug.Log(">>" + userJSON + "<<");
        }
    }

    private void CreatePlayerCards ()
     {
     	Vector3 Position = userCard.transform.position;

     	if (userList.Count < 1)
     		return;

     	for (int i=1; i <= userList.Count; i++)
     	{
     		if (i == 1)
     		{
     			userCard.GetComponent<Image>().sprite = fpPlate;
     			userEmblem.GetComponent<Image>().sprite = fpEmblem;
     			xpEmblem.GetComponent<Image>().sprite = fpEmblem;
     		}
     		else if (i == 2)
     		{
     			userCard.GetComponent<Image>().sprite = spPlate;
     			userEmblem.GetComponent<Image>().sprite = spEmblem;
     			xpEmblem.GetComponent<Image>().sprite = spEmblem;
     		}
     		else if (i == 3)
     		{
     			userCard.GetComponent<Image>().sprite = tpPlate;
     			userEmblem.GetComponent<Image>().sprite = tpEmblem;
     			xpEmblem.GetComponent<Image>().sprite = tpEmblem;
     		}
     		else
     		{
     			userCard.GetComponent<Image>().sprite = normalPlate;
     			userEmblem.GetComponent<Image>().sprite = normalEmblem;
     			xpEmblem.GetComponent<Image>().sprite = normalEmblem;
     		}

     		User user = userList[i-1];

     		userPlace.text = "" + i;
     		userName.text = user.name;
        	userLevel.text = "Nível " + (1 + user.xp/1000);
        	userXP.text = user.xp.ToString();

        	Position = new Vector3(Position.x, Position.y, Position.z);

            GameObject Card = (GameObject) Instantiate(userCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);
     	}

        Destroy(userCard);
     }
}
