using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wokarol
{
	
	public class StartScene : MonoBehaviour
	{
		// Variables
		[SerializeField] StringVariable nameOfTheStartScene = null;

		// Functions
		private void Start ()
		{
			Debug.Log("Starting scene process started");
			if (SceneManager.sceneCount == 1) {
				ScenesController.Instance.ChangeScene/*Imidiate*/(nameOfTheStartScene.Value);
			}
		}
	}
}

