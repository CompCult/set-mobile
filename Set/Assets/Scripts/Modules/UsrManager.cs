using UnityEngine;
using System.Collections;

public static class UsrManager
{
	private static User _user;
	public static User user { get { return _user; } }

	public static void UpdateUser(User user)
	{
		_user = user;
	}

	public static void UpdateUser(string JSON)
	{
		_user = JsonUtility.FromJson<User>(JSON);
	}

	public static User CreateUserFromJSON(string JSON)
	{
		return JsonUtility.FromJson<User>(JSON);
	}

	public static void SetAddressID(int address)
	{
		_user.address = address;
	}
}
