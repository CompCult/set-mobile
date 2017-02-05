using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RankingAPI 
{
	public static WWW RequestRanking()
	{
		WebAPI.apiPlace = "/user/rank/";
		WebAPI.pvtKey = "6b2b7f9bc0";

		return WebAPI.Get();
	}
}