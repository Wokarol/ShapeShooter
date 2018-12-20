using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol {
	[CreateAssetMenu]
	public class Logger : ScriptableObject {
		public void Log (string text) {
			Debug.Log(text);
		}

		public void WarningLog (string text) {
			Debug.LogWarning(text);
		}

		public void ErrorLog (string text) {
			Debug.LogError(text);
		}
	}
}