using System;
using UnityEngine;

namespace Wokarol.SerializationSystem.Serializers
{
	internal class PlatformSerializer
	{
		static ISerializer serializer;
		internal static ISerializer Get ()
		{
			if (serializer != null)
				return serializer;

			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
				return serializer = new PCFileSerializer();
			}

			if (Application.platform == RuntimePlatform.Android) {
				return serializer = new PCFileSerializer();
			}

			Debug.LogError("Can't find serializer for this platform, trying standard PC Serializer");
			return serializer = new PCFileSerializer();
		}
	}
}