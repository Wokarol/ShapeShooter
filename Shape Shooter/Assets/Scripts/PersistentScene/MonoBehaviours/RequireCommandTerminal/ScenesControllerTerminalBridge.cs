using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.Bridges
{
	[RequireComponent(typeof(ScenesController))]
	public class ScenesControllerTerminalBridge : MonoBehaviour
	{
		ScenesController scenesController;

		// Terminal Commands
		private void Start ()
		{
			scenesController = GetComponent<ScenesController>();
			CommandTerminal.Terminal.Shell.AddCommand("Change_scene", ChangeSceneCommand, 1, 1, "Changes scene by name");
			CommandTerminal.Terminal.Autocomplete.Register("change_scene");
		}
		public void ChangeSceneCommand (CommandTerminal.CommandArg[] args)
		{
			scenesController.ChangeScene(args[0].String);
		}
	}
}

