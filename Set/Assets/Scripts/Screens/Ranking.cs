using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Ranking : GenericScreen 
{
	public GameObject userCard;
	public Text userName, userXP, userPlace;

	public void Start() 
	{
		AlertsAPI.instance.Init();
		backScene = "Home";

		RequestRanking();
	}

	private void RequestRanking()
	{
		WWW rankingRequest = UserAPI.RequestRanking();

		string Response = rankingRequest.text,
		Error = rankingRequest.error;

		if (Error == null)
		{
			Debug.Log("Response:" + Response);

			UserManager.UpdateRanking(Response);
			CreateUserCards();
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao receber o ranking. Tente novamente em alguns instantes.", "Tudo bem");
			LoadBackScene();
		}
	}

	private void CreateUserCards ()
     {
     	int place = 1;
     	Vector3 Position = userCard.transform.position;
     	foreach (User user in UserManager.ranking)
        {
        	userName.text = user.name;
        	userXP.text = user.xp + " EXP";
        	userPlace.text = place.ToString();

            Position = new Vector3(Position.x, Position.y, Position.z);

            GameObject Card = (GameObject) Instantiate(userCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);

            place++;
        }

        Destroy(userCard);
    }
}
