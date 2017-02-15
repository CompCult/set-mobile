using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public static class MiscAPI
{
	private static string version = "0.1.3";

	public static WWW RequestVersion ()
	{
		WebAPI.apiPlace = "/sysinfo/mobile-version/";

		return WebAPI.Get();
	}

	public static string GetVersion()
	{
		return version;
	}
}
