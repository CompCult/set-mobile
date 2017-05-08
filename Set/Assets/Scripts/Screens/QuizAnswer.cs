using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public class QuizAnswer : GenericScreen 
{
	public GameObject answerCard;
	public Text quizName, quizQuestion, answerText;

	public void Start () 
	{
		AlertsAPI.instance.Init();
		backScene = "Quizzes";

		FillQuizInfo();
		CreateAnswerCards();
	}

	private void FillQuizInfo ()
	{
		quizName.text = QuizManager.quiz.name;
		quizQuestion.text = QuizManager.quiz.question;
	}

    private void CreateAnswerCards ()
    {
     	Vector3 Position = answerCard.transform.position;
     	string[] answers = QuizManager.quiz.answers;

     	for (int i=0; i < answers.Length; i++)
        {
        	if (!answers[i].Equals(""))
        	{
	        	answerText.text = answers[i];
	            Position = new Vector3(Position.x, Position.y, Position.z);

	            GameObject Card = (GameObject) Instantiate(answerCard, Position, Quaternion.identity);
	            Card.transform.SetParent(GameObject.Find("Area").transform, false);
        	}
        }

        Destroy(answerCard);
    }

    public void SendQuizResponse (Text choosenAnswer)
	{
		QuizManager.quizResponse.user_id = UserManager.user.id;
		QuizManager.quizResponse.quiz_id = QuizManager.quiz.id;

		string[] answers = QuizManager.quiz.answers;

		for (int i=0; i < answers.Length; i++)
		{
			if (choosenAnswer.Equals(answers[i]))
			{
				QuizManager.quizResponse.answer = i+1;
				break;
			}
		}

		WWW responseForm = QuizAPI.SendQuizResponse(QuizManager.quiz, QuizManager.quizResponse);
		ProcessSend(responseForm);
	}

	private void ProcessSend (WWW responseForm)
	{
		string Error = responseForm.error,
		Response = responseForm.text;

		if (Error == null) 
		{
			AlertsAPI.instance.makeToast("Resposta enviada", 1);

			if (QuizManager.quiz.next_quiz == 0)
				LoadScene("Quizzes");
			else 
				GoToNextQuiz (QuizManager.quiz.next_quiz);
		}
		else 
		{
			Debug.Log("Error sending: " + Error);

			if (Error.Contains("404 "))
				AlertsAPI.instance.makeAlert("Que pena!\nParece que esse quiz foi removido ou já expirou.", "Tudo bem");
			else 
				AlertsAPI.instance.makeAlert("Ops!\nHouve um problema no servidor. Verifique sua conexão e tente novamente.", "Tudo bem");
		}
	}

	private void GoToNextQuiz (int nextQuiz)
    {
    	string nextQuizCode = CalculateSHA1(nextQuiz);
    	Debug.Log(nextQuizCode);
     	WWW quizRequest = QuizAPI.RequestPrivateQuiz(nextQuizCode);

     	string Error = quizRequest.error,
     	Response = quizRequest.text;

     	if (Error == null)
     	{
     		if (Response.Contains(LocalizationManager.GetText("InvalidSecretQuiz")))
     		{
     			AlertsAPI.instance.makeAlert("Ops!\nNão encontramos a próxima pergunta no banco de dados. Contate um tutor.", "OK");
     			LoadScene("Quizzes");
     			return;
     		}

     		QuizManager.UpdateQuiz(Response);
            LoadScene("Quiz");
     	}
     	else
     	{
     		Debug.Log("Error get next quiz: " + Error);
     		AlertsAPI.instance.makeAlert("Ops, falha ao receber a próxima pergunta!\nVerifique sua conexão e tente novamente.", "OK");
     	}
    }

   	private string CalculateSHA1 (int nextQuiz)
	{
		StringBuilder sb = new StringBuilder();
		foreach (byte b in GetHash("compcult-set-quiz-" + nextQuiz))
			sb.Append(b.ToString("x2"));
 
		return sb.ToString().Substring(0, 8);
	}

	private byte[] GetHash (string inputString)
	{
		HashAlgorithm algorithm = SHA1.Create();  // SHA1.Create()
		return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
	}
}
