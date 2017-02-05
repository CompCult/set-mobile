using UnityEngine;
using System.Collections;

public class AlertsAPI : MonoBehaviour {
	
	/** Instance */
	static AlertsAPI _instance = null;
	public static AlertsAPI instance
	{
		get
		{
			if(!_instance){
				_instance = FindObjectOfType(typeof(AlertsAPI)) as AlertsAPI;
				
				if(!_instance)
				{
					var obj = new GameObject("AlertsAPI");
					_instance = obj.AddComponent<AlertsAPI>();
				}
			}
			return _instance;
		}
	}

	public void Init(){}

	/// <summary>
	/// Makes the toast.
	/// </summary>
	/// <param name="toast">Toast.</param>
	/// <param name="length"(must be either 0 or 1!)>Length.</param>
	public void makeToast(string toast,int length)
	{
		Debug.Log("Toast: " + toast);
		
		if (Application.platform != RuntimePlatform.Android) 
			return;

		#if !DEBUGMODE && UNITY_ANDROID
		using(AndroidJavaObject jo =  new AndroidJavaObject("com.nevzatarman.unityextras.UnityExtras"))
			jo.Call("makeToast",toast,length);
		#endif
	}

	/// <summary>
	/// Alert the specified message with neutralButton.
	/// </summary>
	/// <param name="message">Message.</param>
	/// <param name="neutralButtonText">Neutral button text.</param>
	public void makeAlert(string message, string neutralButtonText)
	{
		Debug.Log("Toast: " + message + " / Button: " + neutralButtonText);

		if (Application.platform != RuntimePlatform.Android) 
			return;

		#if !DEBUGMODE && UNITY_ANDROID
		using(AndroidJavaObject jo =  new AndroidJavaObject("com.nevzatarman.unityextras.UnityExtras"))
			jo.Call("alert",message,neutralButtonText,gameObject.name);
		#endif
	}

	/// <summary>
	/// Alert the specified message,with neutralButton and negativeButton.
	/// </summary>
	/// <param name="message">Message.</param>
	/// <param name="neutralButtonText">Neutral button text.</param>
	/// <param name="negativeButtonText">Negative button text.</param>
	public void makeAlert(string message, string neutralButtonText, string negativeButtonText)
	{
		if (Application.platform != RuntimePlatform.Android) 
			return;

		#if !DEBUGMODE && UNITY_ANDROID
		using(AndroidJavaObject jo =  new AndroidJavaObject("com.nevzatarman.unityextras.UnityExtras"))
			jo.Call("alert",message,neutralButtonText,negativeButtonText,gameObject.name);
		#endif
	}
}