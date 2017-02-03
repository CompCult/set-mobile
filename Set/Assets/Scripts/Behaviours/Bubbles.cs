using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Bubbles : MonoBehaviour
{
	public ParticleSystem bubblesLeft;
	public ParticleSystem bubblesRigth;
	
	public void Start () 
	{
		bubblesLeft.GetComponent<ParticleSystem>().playbackSpeed = 0.15f;
		bubblesRigth.GetComponent<ParticleSystem>().playbackSpeed = 0.15f;

		bubblesLeft.Play();
		bubblesRigth.Play();
	}
}
