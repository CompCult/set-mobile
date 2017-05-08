using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Quizzes : GenericScreen 
{
	public GameObject quizCard;
	public Text quizName;
	public InputField privateQuizID;

	public List<Quiz> quizList;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Home";

		ReceivePublicQuizzes();
	}

	private void ReceivePublicQuizzes()
	{
		WWW quizzesRequest = QuizAPI.RequestPublicQuizzes();

		string Response = quizzesRequest.text,
		Error = quizzesRequest.error;

		if (Error == null)
		{
			FillQuizzesList(Response);
			CreateQuizzesCards();
		}
		else 
		{
			AlertsAPI.instance.makeAlert("Ops!\nHouve um problema ao receber os quizzes. Tente novamente em alguns instantes.", "Tudo bem");
			LoadBackScene();
		}
	}

	private void FillQuizzesList(string quizzes)
    {
		string[] quizzesJSON = quizzes.Replace ("[", "").Replace ("]", "").Replace ("},{", "}%{").Split ('%');
     	quizList = new List<Quiz>();

		foreach (string quizJSON in quizzesJSON)
        {
			Quiz quiz = JsonUtility.FromJson<Quiz>(quizJSON);
        	quizList.Add(quiz);
        }
    }

    private void CreateQuizzesCards ()
     {
     	Vector3 Position = quizCard.transform.position;

     	if (quizList.Count < 1)
     		return;

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

     public void SearchPrivateQuiz ()
     {
     	if (privateQuizID.text == "")
     		return;

     	WWW quizRequest = QuizAPI.RequestPrivateQuiz(privateQuizID.text);

     	string Error = quizRequest.error,
     	Response = quizRequest.text;

     	if (Error == null)
     	{
     		if (Response.Contains(LocalizationManager.GetText("InvalidSecretQuiz")))
     		{
     			AlertsAPI.instance.makeAlert("Quiz não encontrado!\nVerifique se inseriu o código corretamente.", "OK");
     			return;
     		}

     		QuizManager.UpdateQuiz(Response);
            LoadScene("Quiz");
     	}
     	else
     	{
     		Debug.Log("Error get quiz: " + Error);
     		AlertsAPI.instance.makeAlert("Ops, falha ao receber quiz!\nVerifique sua conexão e tente novamente.", "OK");
     	}
     }

     public void StartPublicQuiz (Text quizName)
     {
     	foreach (Quiz quiz in quizList)
     	{
     		if (quiz.name == quizName.text)
     		{
     			QuizManager.UpdateQuiz(quiz);
                LoadScene("Quiz");
     			break;
     		}
     	}
     }
}
