using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
	[ExecuteInEditMode()]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Camera/Ratio Modifier")]
	public class AspectRatioModifier : MonoBehaviour
	{
		// Variables
		public float ratio = 1f;
		new Camera camera;
		// Functions
		private void Awake ()
		{
			camera = GetComponent<Camera>();
			camera.aspect = ratio;
		}

		private void Update ()
		{
			var rect = camera.rect;
			if(Screen.width > (Screen.height * ratio)) {
				// Horizontal
				rect.height = 1;
				rect.width = ((float)Screen.height * ratio) / (float)Screen.width;
			} else {
				// Vertical
				rect.width = 1;
				rect.height = ((float)Screen.width / ratio) / (float)Screen.height;
			}
			rect.x = 0.5f - (rect.width / 2f);
			rect.y = 0.5f - (rect.height / 2f);
			camera.rect = rect;
		}

		private void OnValidate ()
		{
			camera = GetComponent<Camera>();
			camera.aspect = ratio;
		}
	}
}

