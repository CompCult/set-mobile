using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Group
{
	public int id, owner_id, points;
	public string name;
	public List<User> members;
}
