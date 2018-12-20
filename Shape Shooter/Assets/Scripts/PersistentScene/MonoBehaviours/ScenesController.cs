using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wokarol
{
	public class ScenesController : MonoBehaviour
	{
		// Variables
		[SerializeField] CanvasGroup FadeImageGroup = null;
		[SerializeField] float fadeTime = 0;

		[Space]
		[SerializeField] bool usePersistentScene = true;

		public static ScenesController Instance { get; private set; }

		// Functions
		private void Awake ()
		{
			if (Instance == null) {
				Instance = this;
			} else {
				Debug.LogError("There is more that one SceneManager");
				Destroy(gameObject);
				return;
			}
		}

		public void ChangeScene (string nameOfNewScene)
		{
			Debug.Log($"Finding {nameOfNewScene}");		
			if (usePersistentScene && SceneManager.GetSceneByName(nameOfNewScene).buildIndex == 0) {
				Debug.LogWarning("Persistent Scene shouldn't be loaded");
				return;
			}
			if (SceneUtility.GetBuildIndexByScenePath(nameOfNewScene) < 0) {
				throw new System.Exception($"There's no {nameOfNewScene} scene");
			}

			Debug.Log($"{nameOfNewScene} is being loaded");
			StartCoroutine(ChangeSceneCoroutine(nameOfNewScene, () => { Debug.Log($"Finished loading"); }));
		}

		IEnumerator ChangeSceneCoroutine (string scene, System.Action callback)
		{
			yield return StartCoroutine(Fade(1));
			yield return StartCoroutine(UnloadActiveScene());
			yield return StartCoroutine(LoadScene(scene));
			yield return StartCoroutine(Fade(0));
			callback?.Invoke();
		}

		IEnumerator LoadScene (string scene)
		{
			Debug.Log($"<b>LOAD:</b> Loaded scene {scene}");
			yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
			SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
		}

		IEnumerator UnloadActiveScene ()
		{
			var activeScene = SceneManager.GetActiveScene();
			if (usePersistentScene && activeScene.buildIndex == 0) {
				Debug.Log($"<b>LOAD:</b> Denied unloading scene {activeScene.name}");
				yield return null;
			} else {
				Debug.Log($"<b>LOAD:</b> Unloaded scene {activeScene.name}");
				yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
			}
		}

		IEnumerator Fade (float targetAlpha)
		{
			float speed = 1 / fadeTime;
			float startAlpha = FadeImageGroup.alpha;
			float progress = 0;
			while (progress < 1) {
				progress += Time.deltaTime * speed;
				FadeImageGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, progress);
				yield return null;
			}
			FadeImageGroup.alpha = targetAlpha;
		}
	}
}

