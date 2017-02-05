using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Group
{
	public int id, owner_id;
	public string name;
	public List<User> members;
}
