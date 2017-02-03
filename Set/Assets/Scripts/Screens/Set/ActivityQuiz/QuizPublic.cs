using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuizPublic : GenericScreen 
{
	public GameObject quizCard, continueButton;
	public Text quizName;

	public List<Quiz> quizList;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Search Quiz";

		ReceivePublicQuizzes ();
	}

	private void ReceivePublicQuizzes ()
	{
		WWW quizzesRequest = Authenticator.RequestPublicQuizzes();

		string Response = quizzesRequest.text,
		Error = quizzesRequest.error;

		if (Error == null)
		{
			FillQuizList(Response);
			CreateQuizCards();
		}
		else 
		{
			UnityAndroidExtras.instance.makeToast("Falha ao receber os quizzes públicos", 1);
			LoadBackScene();
		}
	}

	private void FillQuizList(string quizzes)
    {
		string[] quizzesJSON = quizzes.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
     	quizList = new List<Quiz>();

		foreach (string quizJSON in quizzesJSON)
        {
			Quiz quiz = QuestManager.CreateQuiz(quizJSON);
        	quizList.Add(quiz);
        }
    }

    private void CreateQuizCards ()
     {
     	Vector3 Position = quizCard.transform.position;

     	foreach (Quiz quiz in quizList)
        {
        	quizName.text = quiz.name;
            Position = new Vector3(Position.x, Position.y, Position.z);

            GameObject Card = (GameObject) Instantiate(quizCard, Position, Quaternion.identity);
            Card.transform.SetParent(GameObject.Find("Area").transform, false);

            Debug.Log(quiz.name);
        }

        Destroy(quizCard);
     }

     public void SelectQuiz(Text quizName)
     {
     	foreach (Quiz quiz in quizList)
     	{
     		if (quiz.name == quizName.text)
     		{
     			QuestManager.UpdateQuiz(quiz);
     			continueButton.SetActive(true);
     			break;
     		}
     		else 
     		{
     			continueButton.SetActive(false);
     		}
     	}
     }
}
