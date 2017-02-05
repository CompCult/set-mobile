using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GenericScreen : MonoBehaviour 
{
	protected static string backScene,
	nextScene;

	public virtual void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
			LoadBackScene();
	}

	public void LoadScene(string sceneName) 
	{
		if (sceneName != null) 
			SceneManager.LoadScene(sceneName);
		else
			Application.Quit();
	}

	private void LoadNextScene()
	{
		LoadScene(nextScene);
	}

	private void LoadBackScene()
	{
		LoadScene(backScene);
	}

	private void ReloadScene()
	{
		Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
	}
}


