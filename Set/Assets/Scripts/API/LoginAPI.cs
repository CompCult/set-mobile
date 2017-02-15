using UnityEngine;
using System.Text;
using System.Security.Cryptography;

public static class LoginAPI 
{
	public static WWW RequestUser (string email, string password)
	{
		WWWForm loginForm = new WWWForm ();
		loginForm.AddField ("email", email);
		loginForm.AddField ("password", password);

		WebAPI.apiPlace = "/user/authenticate/";

		return WebAPI.Post(loginForm);
	}

	public static WWW RequestRegister (string name, string email, string cpf, string registry, string phone, string institution, string course, string password) 
	{
		WWWForm registerForm = new WWWForm ();
		registerForm.AddField ("name", name);
		registerForm.AddField ("email", email);
		registerForm.AddField ("cpf", cpf);
		registerForm.AddField ("registry", registry);
		registerForm.AddField ("phone", phone);
		registerForm.AddField ("institution", institution);
		registerForm.AddField ("course", course);
		registerForm.AddField ("password", password);

		WebAPI.apiPlace = "/user/create/";

		return WebAPI.Post(registerForm);
	}
}
