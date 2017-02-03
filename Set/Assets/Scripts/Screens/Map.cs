using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Map : GenericScreen {

	public Dropdown ReportDropdown;
	public Text ReportStatus;
	public GameObject MapField;

	private List<Report> ReportList;

	public void Start () 
	{
		UnityAndroidExtras.instance.Init();
		backScene = "Home";

		ReceiveNotifications();
	}

	public void ReceiveNotifications()
	{
		User user = UsrManager.user;
		WWW requestNotifications = Authenticator.RequestNotifications(user.id);

		string Response = requestNotifications.text,
        Error = requestNotifications.error;

        if (Error == null) 
		{
			// Filter the JSON to receive a Split
			Response = Response.Replace("[","").Replace("]","").Replace("},{","}@{");

			string[] Reports = Response.Split('@');
			ReportList = new List<Report>();

			foreach(string Report in Reports)
			{
				Report aux = new Report();
				if (!String.IsNullOrEmpty(Report))
					ReportList.Add(aux.CreateReport(Report));
			}

			FillOptionsOnDropDown();
		} 
		else 
		{
			Debug.Log("Error trying to get reports: " + Error);
		}
	}

	public void FillOptionsOnDropDown()
	{
		ReportDropdown.options.Clear();
		ReportDropdown.options.Add(new Dropdown.OptionData() {text = "Selecione uma notificação"});
		
		foreach (Report rp in ReportList)
			ReportDropdown.options.Add(new Dropdown.OptionData() {text = "Notificação - ID " + rp.id});

	  	ReportDropdown.RefreshShownValue();
	}

	public void MarkSelectedLocation()
	{
		Text AddressSelected = ReportDropdown.captionText;
		Report SelectedReport = null;	

		foreach (Report rp in ReportList)
		{
			if (AddressSelected.text.Equals("Notificação - ID " + rp.id))
			{
				SelectedReport = rp;
				break;
			}
		}

		if (SelectedReport != null)
		{
			StartCoroutine(UpdateMapOnScreen(SelectedReport));
			
			ReportStatus.enabled = true;
			
			switch (SelectedReport.status)
			{
				case "pending":
					ReportStatus.text = "Pendente"; break;
				case "invalid":
					ReportStatus.text = "Inválida"; break;
				case "validated":
					ReportStatus.text = "Validada"; break;
				default:
					ReportStatus.text = ""; break;
			}
		}
	}

	private IEnumerator UpdateMapOnScreen(Report SelectedReport)
	{
		var request = new WWW(SelectedReport.GetQueryToAddress());
        yield return request;

        MapField.GetComponent<Renderer>().material.mainTexture = request.texture;
	}
	
}
