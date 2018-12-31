using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEditor;


namespace Wokarol
{
	public class PersistentSceneLoader
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void GetPersistentScene ()
		{
			Debug.Log("Finding Persistent Scene");
			//var key = $"{Application.productName}_{SceneManager.GetActiveScene().name}";
			var key = $"Scene_{AssetDatabase.FindAssets(SceneManager.GetActiveScene().name)[0]}_NoPersistentScene";
			if (EditorPrefs.HasKey(key)) {
				return;
			}

			for (int i = 0; i < SceneManager.sceneCount; i++) {
				if(SceneManager.GetSceneAt(i).buildIndex == 0){
					// Scene found
					return;
				}
			}

			// Scene not found
			Debug.Log("<b>LOAD:</b> Loaded Persistent Scene");
			SceneManager.LoadScene(0, LoadSceneMode.Additive);
		}

	}
}
