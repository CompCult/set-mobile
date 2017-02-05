using UnityEngine;
using System.Text;
using System.Security.Cryptography;

public static class LoginAPI 
{
	public static WWW RequestUser (string email, string password)
	{
		WWWForm loginForm = new WWWForm ();
		loginForm.AddField ("email", email);
		loginForm.AddField ("password", CalculateSHA1(password));

		WebAPI.apiPlace = "/user/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Post(loginForm);
	}

	public static WWW RequestRegister (string name, string email, string password) 
	{
		WWWForm registerForm = new WWWForm ();
		registerForm.AddField ("name", name);
		registerForm.AddField ("email", email);
		registerForm.AddField ("password", CalculateSHA1(password));

		WebAPI.apiPlace = "/user/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Post(registerForm);
	}

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
