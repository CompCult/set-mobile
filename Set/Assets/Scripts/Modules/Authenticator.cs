using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public static class Authenticator
{
	public static WWW RequestUserID (string email, string password) 
	{
		WWWForm loginForm = new WWWForm ();
		loginForm.AddField ("login", email);
		loginForm.AddField ("password", CalculateSHA1(password));

		WebFunctions.apiPlace = "/auth/";
		WebFunctions.pvtKey = "f51e8e6754";

		return WebFunctions.Post(loginForm);
	}

	public static WWW RequestRegister (string name, string email, string password) 
	{
		WWWForm registerForm = new WWWForm ();
		registerForm.AddField ("name", name);
		registerForm.AddField ("email", email);
		registerForm.AddField ("password", CalculateSHA1(password));

		WebFunctions.apiPlace = "/user/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Post(registerForm);
	}

	public static WWW RequestUser (int id)
	{
		WebFunctions.apiPlace = "/user/" + id + "/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Get();
	}

	public static WWW RequestAddress(int id)
	{
		WebFunctions.apiPlace = "/address/" + id + "/";
		WebFunctions.pvtKey = "fc64ec6244";

		return WebFunctions.Get();
	}

	public static WWW RequestNotifications(int id)
	{
		WebFunctions.apiPlace = "/notification/user/" + id + "/";
		WebFunctions.pvtKey = "d86c362f4b";

		return WebFunctions.Get();
	}

	public static WWW RequestPublicActivities()
	{
		WebFunctions.apiPlace = "/mission/public/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Get();
	}

	public static WWW RequestPublicQuizzes()
	{
		WebFunctions.apiPlace = "/quiz/public/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Get();
	}

	public static WWW RequestRanking()
	{
		WebFunctions.apiPlace = "/user/rank/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Get();
	}

	public static WWW UpdateUser (string name, string email, string birth, string cpf, string address, string phone, string pass)
	{
		WWWForm updateForm = new WWWForm();
		updateForm.AddField ("name", name);
		updateForm.AddField ("email", email);
		updateForm.AddField ("password", CalculateSHA1(pass));
		updateForm.AddField ("birth", birth);
		updateForm.AddField ("cpf", cpf);
		if (address != "0") // Indicates that the user have an address
			updateForm.AddField ("address", address);
		updateForm.AddField ("phone", phone);

		WebFunctions.apiPlace = "/user/" + UsrManager.user.id + "/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Post(updateForm);
	}

	public static WWW UpdateAddress (string zipcode, string street, string number, string district, string city, string state, string complement)
	{
		WWWForm updateForm = new WWWForm ();
		updateForm.AddField ("zipcode", zipcode);
		updateForm.AddField ("street", street);
		updateForm.AddField ("number", number);
		updateForm.AddField ("district", district);
		updateForm.AddField ("city", city);
		updateForm.AddField ("state", state);
		updateForm.AddField ("complement", complement);

		WebFunctions.apiPlace = "/address/" + AddressManager.address.id + "/";
		WebFunctions.pvtKey = "fc64ec6244";

		return WebFunctions.Post(updateForm);
	}

	public static WWW CreateAddress (string zipcode, string street, string number, string district, string city, string state, string complement)
	{
		WWWForm updateForm = new WWWForm ();
		updateForm.AddField ("zipcode", zipcode);
		updateForm.AddField ("street", street);
		updateForm.AddField ("number", number);
		updateForm.AddField ("district", district);
		updateForm.AddField ("city", city);
		updateForm.AddField ("state", state);
		updateForm.AddField ("complement", complement);

		WebFunctions.apiPlace = "/address/";
		WebFunctions.pvtKey = "fc64ec6244";

		return WebFunctions.Post(updateForm);
	}

	public static WWW SendPhoto (int id, string latitude, string longitude, string type, byte[] bytes)
	{
		WWWForm photoForm = new WWWForm ();
		photoForm.AddField ("user_id", id);
		photoForm.AddField ("latitude", latitude);
		photoForm.AddField ("longitude", longitude);
		photoForm.AddField ("status", "pending");
		photoForm.AddField ("type", type);
		photoForm.AddBinaryData("photo", bytes, "Photo.png", "image/png");

		WebFunctions.apiPlace = "/notification/";
		WebFunctions.pvtKey = "d86c362f4b";

		return WebFunctions.Post(photoForm);
	}

	public static WWW RequestHQ ()
	{
		WebFunctions.apiPlace = "/hq/random/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Get();
	}

	public static WWW SendHQ (HQResponse hqResponse)
	{
		WWWForm hqForm = new WWWForm ();
		hqForm.AddField ("user_id", hqResponse.user_id);
		hqForm.AddBinaryData("photo", hqResponse.photo, "Photo.png", "image/png");

		WebFunctions.apiPlace = "/hq/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Post(hqForm);
	}

	public static WWW SendHQRate (HQ currentHQ, int hqRate)
	{
		WWWForm rateForm = new WWWForm ();
		rateForm.AddField ("user_id", currentHQ.user_id);
		rateForm.AddField ("hq_id", currentHQ.photo.hq_id);
		rateForm.AddField ("value", hqRate);

		WebFunctions.apiPlace = "/rating/";
		WebFunctions.pvtKey = "6b2b7f9bc0";

		return WebFunctions.Post(rateForm);
	}

	public static WWW RequestQuiz(string quizID)
	{
		WebFunctions.apiPlace = "/quiz/" + quizID + "/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Get();
	}

	public static WWW SendQuiz(QuizResponse quizResponse)
	{
		Debug.Log(quizResponse.ToString());
		
		WWWForm quizForm = new WWWForm ();
		quizForm.AddField("quiz_id", quizResponse.quiz_id);
		quizForm.AddField ("user_id", quizResponse.user_id);
		quizForm.AddField ("quiz_answer", quizResponse.quiz_answer);

		WebFunctions.apiPlace = "/answer/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Post(quizForm);
	}

	public static WWW RequestActivity(string activityID)
	{
		WebFunctions.apiPlace = "/mission/" + activityID + "/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Get();
	}

	public static WWW SendActivity(ActivityResponse activityResponse, Activity activity)
	{
		Debug.Log(activityResponse.ToString());

		WWWForm responseForm = new WWWForm ();
 		responseForm.AddField("user_id", activityResponse.user_id);
 		responseForm.AddField ("mission_id", activityResponse.activity_id);

 		if (activity.gps_enabled) 
 			responseForm.AddField ("coordinates", activityResponse.coord_start); 
 			//responseForm.AddField ("coord_start", activityResponse.coord_mid);
 			//responseForm.AddField ("coord_mid", activityResponse.coord_mid);
 			//responseForm.AddField ("coord_end", activityResponse.coord_end);

 		if (activity.text_enabled)
 			responseForm.AddField ("text", activityResponse.text);

		if (activity.photo_file)
 			responseForm.AddBinaryData("photo", activityResponse.photo, "Photo.png", "image/png");

 		if (activity.audio_file)
 			responseForm.AddBinaryData("audio", activityResponse.audio, "voice.wav", "audio/wav");

		WebFunctions.apiPlace = "/answer/";
		WebFunctions.pvtKey = "ec689306c5";

		return WebFunctions.Post(responseForm);
	}

	public static WWW CheckVersion ()
	{
		WebFunctions.apiPlace = "/sysinfo/mobile-version";
		WebFunctions.pvtKey = "";

		return WebFunctions.Get();
	}

	// Convert input string to SHA1
	private static string CalculateSHA1 (string input)
	{
		using (SHA1Managed sha1 = new SHA1Managed())
		{
			var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
			var sb = new StringBuilder(hash.Length * 2);

			foreach (byte b in hash)
			{
				// X2 to uppercase and x2 to lowercase
				sb.Append(b.ToString("x2"));
			}

			return sb.ToString();
		}
	}
}
