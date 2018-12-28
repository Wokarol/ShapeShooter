using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommandTerminal;
using UnityEngine.SceneManagement;

namespace Wokarol.TerminalCommands
{
	public static class TerminalCommands
	{
		[RegisterCommand("Scenes", Help = "Show all loaded scenes", MaxArgCount = 0)]
		public static void ShowScenes (CommandArg[] args)
		{
			string text = "Loaded scenes:";
			for (int i = 0; i < SceneManager.sceneCount; i++) {
				var scene = SceneManager.GetSceneAt(i);
				text += $"\n\t{scene.name}";
			}
			Terminal.Log(text);
		}

        [RegisterCommand("Application_Data", Help = "Shows version, author, project name, ...", MaxArgCount = 0)]
        public static void Data (CommandArg[] args)
        {
            Terminal.Log(
                $"\tVersion: {Application.version} \n" + 
                $"\tCompanny Name: {Application.companyName} \n" + 
                $"\tProduct Name: {Application.productName} \n" +
                $"\tPlatform: {Application.platform} \n" +
                $"\tPersistent Data Path: {Application.persistentDataPath} \n" +
                $"\tUnity Version: {Application.unityVersion} \n");
        }
	}
}

