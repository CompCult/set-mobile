using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slider : MonoBehaviour {

	public Text actualValue;
	
	// Update is called once per frame
	void Update () {
		float sliderValue = this.GetComponent<UnityEngine.UI.Slider>().value;

		actualValue.text = string.Format("{0:N1}", sliderValue);
	}
}
