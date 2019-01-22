using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Wokarol.SerializationSystem.Serializers
{
	public class PCFileSerializer : ISerializer
	{
		// Consts
		private const string FILEEXTENSION = ".sav";
		private const string TEMPFILEEXTENSION = "_temp.sav";

		private const string debugPrefix = "<b>PCFileSerializer: </b>";

		public PCFileSerializer ()
		{
		}

		public bool FileExist (string fileName, string folderName)
		{
			string path = Path.Combine(Application.persistentDataPath, folderName);
			return File.Exists(Path.Combine(path, fileName + FILEEXTENSION));
		}

		public void Delete (string fileName, string folderName, System.Action beforeDelete = null, System.Action afterDelete = null)
		{
			beforeDelete?.Invoke();
			string path = Path.Combine(Application.persistentDataPath, folderName);
			if (File.Exists(Path.Combine(path, fileName + FILEEXTENSION))) {
				File.Delete(Path.Combine(path, fileName + FILEEXTENSION));
			}
			afterDelete?.Invoke();
		}

		/// <summary>
		/// Serializes data to file
		/// </summary>
		/// <param name="dataToSerialize">Data that is meant to be serialized</param>
		public void SerializeData (Dictionary<string, object> dataToSerialize, string fileName, string folderName)
		{
			var bf = new BinaryFormatter();
			string path = Path.Combine(Application.persistentDataPath, folderName);

			// Checking and creating path
			if (!Directory.Exists(path)) {
				Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, folderName));
			}

			// Making stream out of data
			var stream = new FileStream(Path.Combine(path, fileName + TEMPFILEEXTENSION), FileMode.Create);
			bf.Serialize(stream, dataToSerialize);
			stream.Close();

			// Moving data from _temp.sav to .sav
			File.Delete(Path.Combine(path, fileName + FILEEXTENSION));
			File.Copy(Path.Combine(path, fileName + TEMPFILEEXTENSION), Path.Combine(path, fileName + FILEEXTENSION));
			File.Delete(Path.Combine(path, fileName + TEMPFILEEXTENSION));
		}

		/// <summary>
		/// Deserializes data from file
		/// </summary>
		/// <returns>Deserialized data</returns>
		public Dictionary<string, object> DeserializeData (string fileName, string folderName)
		{
			var deserializationData = new Dictionary<string, object>();
			string path = Path.Combine(Application.persistentDataPath, folderName);

			if (File.Exists(Path.Combine(path, fileName + FILEEXTENSION))) {
				Debug.Log(debugPrefix + "Deserializing data");
				var bf = new BinaryFormatter();
				var stream = new FileStream(Path.Combine(path, fileName + FILEEXTENSION), FileMode.Open);
				deserializationData = (Dictionary<string, object>)bf.Deserialize(stream);
				stream.Close();
			}

			return deserializationData;
		}
	}
}

