using UnityEngine;
using System.Collections;

public static class AddressManager
{
	private static Address _address;
	public static Address address { get { return _address; } }

	public static void UpdateAddress(string JSON)
	{
		_address = JsonUtility.FromJson<Address>(JSON);
	}

	public static Address CreateUserFromJSON(string JSON)
	{
		return JsonUtility.FromJson<Address>(JSON);
	}
}
