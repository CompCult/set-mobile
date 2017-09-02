using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Answers : GenericScreen 
{
	public GameObject answerCard, pendentIcon, approvedIcon, deniedIcon, noMissions;
	public Text answerCode, answerDate;

	public List<Answer> answerList;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Home";

		ReceiveUserAnswers();
	}

	private void ReceiveUserAnswers()
	{
		WWW answersRequest = AnswersAPI.RequestUserAnswers();

		string Response = answersRequest.text,
		Error = answersRequest.error;

		if (Error == "")
		{
			FillAnswersList(Response);
			//CreateActivitiesCards();
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao receber as missões. Tente novamente em alguns instantes.", "Tudo bem");
			LoadBackScene();
		}
	}

	private void FillAnswersList(string answers)
    {
		string[] answersJSON = answers.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
     	answerList = new List<Answer>();

        if (answersJSON[0].Length == 0)
        {
            noMissions.SetActive(true);
            Destroy(answerCard);
            return;
        }

		foreach (string answerJSON in answersJSON)
        {
			Answer answer = JsonUtility.FromJson<Answer>(answerJSON);
        	answerList.Add(answer);

        	Debug.Log("ID: " + answer.answerable_id + " / Created at: " + answer.created_at);
        }

        CreateCards();
    }

    private void CheckAnswerStatus(Answer answer)
    {
	    if (answer.validated)
		{
			pendentIcon.SetActive(false);
			approvedIcon.SetActive(true);
			deniedIcon.SetActive(false);
		}
		else
		{
			pendentIcon.SetActive(false);
			approvedIcon.SetActive(false);
			deniedIcon.SetActive(true);
		}

    	if (answer.created_at.Equals(answer.updated_at))
    	{
    		pendentIcon.SetActive(true);
    		approvedIcon.SetActive(false);
    		deniedIcon.SetActive(false);
    	}
    }

    private void CreateCards ()
    {
     	Vector3 Position = answerCard.transform.position;
     	foreach (Answer answer in answerList)
        {
        	answerCode.text = "#" + answer.id;
            answerDate.text = answer.created_at;
            Position = new Vector3(Position.x, Position.y, Position.z);

            CheckAnswerStatus(answer);

            GameObject Card = (GameObject) Instantiate(answerCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);
        }

        Destroy(answerCard);
    }
}
