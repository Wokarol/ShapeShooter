using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using Wokarol.SerializationSystem.Serializers;

namespace Wokarol.SerializationSystem
{
	[CreateAssetMenu]
	public class SaveData : ScriptableObject
	{
		// Consts
		private const string debugPrefix = "<b>Serializer: </b>";

		// Values changed by user
		public string FolderName = "Saves";
		public string FileName = "Save";

		// -----
		public event Action BeforeSave;
		public event Action BeforeLoad;
		public event Action BeforeDelete;

		public event Action AfterSave;
		public event Action AfterLoad;
		public event Action AfterDelete;

		Dictionary<string, object> currentData = new Dictionary<string, object>();
        public Dictionary<string, object> AllEntries => currentData;
        public string[] CurrentEntries
        {
            get {
                return new List<string>(currentData.Keys).ToArray();
            }
        }


		ISerializer Serializer {
			get {
				return PlatformSerializer.Get();
			}
		}

		public bool FileExist {
			get {
				return Serializer.FileExist(FileName, FolderName);
			}
		}

		public void Delete ()
		{
            Debug.Log($"{debugPrefix}Deleted {FolderName}/{FileName}");
			Serializer.Delete(FileName, FolderName, () => { BeforeDelete?.Invoke(); }, () => { AfterDelete?.Invoke(); });
            Load();
		}

		// Calling Save and Load

		/// <summary>
		/// Initializes Save process
		/// </summary>
		public void Save ()
		{
			BeforeSave?.Invoke();
			if (currentData == null) {
				currentData = new Dictionary<string, object>();
			}
			Serializer.SerializeData(currentData, FileName, FolderName);
			Debug.Log(debugPrefix + "Saved " + currentData.Count + " entries to " + Path.Combine(Application.persistentDataPath, FolderName) + " to " + FileName);
			AfterSave?.Invoke();
		}

		/// <summary>
		/// Initializes Load process
		/// </summary>
		public void Load ()
		{
			BeforeLoad?.Invoke();
			currentData = Serializer.DeserializeData(FileName, FolderName);
			Debug.Log(debugPrefix + "Loaded " + currentData.Count + " entries from " + Path.Combine(Application.persistentDataPath, FolderName) + " from " + FileName);
			AfterLoad?.Invoke();
		}

		/// <summary>
		/// Copies data from other serializer if it loaded data (executes load if not)
		/// </summary>
		/// <param name="source"></param>
		public void CopyLoadedDataFrom (SaveData source)
		{
			BeforeLoad?.Invoke();
			if (source.currentData == null) {
				source.Load();
			}
			currentData = new Dictionary<string, object>(source.currentData);
			FolderName = source.FolderName;
			FileName = source.FileName;
			AfterLoad?.Invoke();
			Debug.Log(debugPrefix + "Copied " + currentData.Count + " entries from " + source.name);
		}

		public void ClearAndSave ()
		{
			BeforeSave?.Invoke();
			currentData = new Dictionary<string, object>();
			Serializer.SerializeData(currentData, FileName, FolderName);
			Debug.Log(debugPrefix + "Cleared " + currentData.Count + " entries in " + Path.Combine(Application.persistentDataPath, FolderName) + " in " + FileName);
			AfterSave?.Invoke();
		}

		// Entry managing

		/// <summary>
		/// Sends entry to current data dictionary
		/// </summary>
		/// <param name="key">"Name" of the entry</param>
		/// <param name="value">Value</param>
		public void SendEntry (string key, object value)
		{
			if (currentData == null)
				Load();

			if (currentData.ContainsKey(key)) {
				currentData[key] = value;
			} else {
				currentData.Add(key, value);
			}
		}

		/// <summary>
		/// Gets entry from current data dictionary
		/// </summary>
		/// <typeparam name="T">Type of the value stored in entry</typeparam>
		/// <param name="key">"Name" of the entry</param>
		/// <param name="defaultValue">Value used when entry doesn't exist</param>
		/// <returns>Returned value</returns>
		public T GetEntry<T> (string key, T defaultValue)
		{
			if (currentData == null)
				Load();

			if (currentData.ContainsKey(key)) {
				if (currentData[key] is T) {
					return (T)currentData[key];
				}
			}
			SendEntry(key, defaultValue);
			return defaultValue;
		}

        /// <summary>
        /// Removes entry from current data dictionary
        /// </summary>
        /// <param name="key">"Name" of the entry</param>
        public void RemoveEntry(string key) {
            if (currentData == null)
                Load();
            if (currentData.ContainsKey(key)) {
                currentData.Remove(key);
            }
        }

        /// <summary>
        /// Checks if  entry exist in current data dictionary
        /// </summary>
        /// <param name="key">"Name" of the entry</param>
        public bool ContainsEntry(string key) {
            return currentData.ContainsKey(key);
        }
    }
}