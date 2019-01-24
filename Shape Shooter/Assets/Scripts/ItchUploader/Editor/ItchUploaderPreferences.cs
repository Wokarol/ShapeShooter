using UnityEngine;

namespace Wokarol.ItchUploader
{
    public class ItchUploaderPreferences : ScriptableObject
    {
        [SerializeField] string _buildFolder;
        [SerializeField] string _username;
        [SerializeField] string _gameName;
        [SerializeField] string _zipName;

        public string BuildFolder => _buildFolder;
        public string Username => _username;
        public string GameName => _gameName;
        public string ZipName => _zipName;


        public static ItchUploaderPreferences Init(string buildFolder = "Build", string username = "", string gameName = "", string zipName = "") {
            var preferences = (ItchUploaderPreferences)CreateInstance(typeof(ItchUploaderPreferences));
            preferences._buildFolder = buildFolder;
            preferences._username = "";
            preferences._gameName = Application.productName;
            preferences._zipName = Application.productName;
            return preferences;
        }
    }
}