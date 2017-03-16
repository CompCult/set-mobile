using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersAPI
{
	public static WWW RequestAnswers()
	{
		WebAPI.apiPlace = "/user/" + UserManager.user.id + "/show-answers/";
		return WebAPI.Get();
	}

	public static WWW RequestGroupAnswers()
	{
		WebAPI.apiPlace = "/group/" + GroupManager.group.id + "/show-answers/";
		return WebAPI.Get();
	}
}
