using UnityEngine;
using System.Text;
using System.Security.Cryptography;

public static class UserAPI 
{
	public static WWW UpdateUser (int id, string name, string email, string cpf, string registry, string phone, string course, string instituition)
	{
		WWWForm updateForm = new WWWForm();
		updateForm.AddField ("id", id);
		updateForm.AddField ("name", name);
		updateForm.AddField ("email", email);
		updateForm.AddField ("cpf", cpf);
		updateForm.AddField ("registry", registry);
		updateForm.AddField ("phone", phone);
		updateForm.AddField ("course", course);
		updateForm.AddField ("instituition", instituition);

		WebAPI.apiPlace = "/user/update/";

		return WebAPI.Post(updateForm);
	}
}
