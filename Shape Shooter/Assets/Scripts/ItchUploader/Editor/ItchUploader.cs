using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading.Tasks;

namespace Wokarol.ItchUploader
{
    public class ItchUploader : EditorWindow
    {
        static TimeSpan maxTimeSinceLastWrite = new TimeSpan(0, 15, 0);

        [MenuItem("Commands/Upload to Itch %&b")]
        public static void Upload() {
            ItchUploaderPreferences preferences = GetPreferences();
            if (preferences != null) {
                string path = Path.Combine(Application.dataPath, "..", preferences.BuildFolder);
                string zipPath = Path.Combine(path, preferences.ZipName + ".zip");
                Debug.Log($"Attempting to push build located at {path}");

                string[] subfoldersPaths = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
                string crashHandlerPath = Directory.GetFiles(path, "*.exe").FirstOrDefault((s) => Regex.IsMatch(s, @"^.*(\/|\\)UnityCrashHandler(64|32).exe$"));
                string winPixEventRuntimeDll = Directory.GetFiles(path, "*.dll").FirstOrDefault((s) => Regex.IsMatch(s, @"^.*(\/|\\)WinPixEventRuntime.dll$"));
                string[] otherFiles = Directory.GetFiles(path).Where((s) => s != crashHandlerPath && s != winPixEventRuntimeDll && s != zipPath).ToArray();

                DateTime writeTime = Directory.GetLastWriteTime(path);
                TimeSpan timeSinceLastWrite = DateTime.Now - writeTime;

                if (TimeSpan.Compare(timeSinceLastWrite, maxTimeSinceLastWrite) > 0 &&
                    !EditorUtility.DisplayDialog("Old Build!!!", "Build is older than 15 minutes, are you sure that it's recent one?", "Upload", "Don't upload"))
                    return;

                ZipAndUpload(preferences, path, zipPath, subfoldersPaths, otherFiles);
            }
        }

        private static void ZipAndUpload(ItchUploaderPreferences preferences, string path, string zipPath, string[] subfoldersPaths, string[] otherFiles) {
            using (ZipOutputStream s = new ZipOutputStream(File.Open(zipPath, FileMode.Create))) {
                s.SetLevel(2);

                byte[] buffer = new byte[4096];

                List<string> files = new List<string>();
                files.AddRange(otherFiles);

                ZipFiles(s, buffer, files.ToArray(), path);
                ZipDirectories(subfoldersPaths, s, buffer, path);

                s.Finish();
                s.Close();
            }

            System.Diagnostics.Process.Start($"cmd.exe", $"/C butler push \"{zipPath}\" {preferences.Username}/{preferences.GameName}:win & pause");
        }

        private static void ZipDirectories(string[] subfoldersPaths, ZipOutputStream s, byte[] buffer, string basePath) {
            foreach (var directory in subfoldersPaths) {
                ZipFiles(s, buffer, Directory.GetFiles(directory), basePath);
            }
        }

        private static void ZipFiles(ZipOutputStream s, byte[] buffer, string[] files, string basePath) {
            foreach (var file in files) {
                ZipFile(s, buffer, file, basePath);
            }
        }

        private static void ZipFile(ZipOutputStream s, byte[] buffer, string file, string basePath) {
            string relativePath = file.Replace(basePath, "").Substring(1).Replace('\\', '/');
            var entry = new ZipEntry(relativePath) {
                DateTime = DateTime.Now,
            };
            s.PutNextEntry(entry);

            using (FileStream fs = File.OpenRead(file)) {
                int sourceBytes;
                do {
                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                    s.Write(buffer, 0, sourceBytes);
                } while (sourceBytes > 0);
            }
        }

        private static ItchUploaderPreferences GetPreferences() {
            var allPreferences = AssetDatabase.FindAssets($"t:{nameof(ItchUploaderPreferences)}");
            if (allPreferences.Length == 0) {
                // No preferences found
                var preferences = ItchUploaderPreferences.Init();
                AssetDatabase.CreateAsset(preferences, $"Assets/Settings/Itch Uploader Preferences.asset");
                Debug.Log("Created a new preferences asset");
                return null;
            } else if (allPreferences.Length == 1) {
                // Only one preferences asset
                return AssetDatabase.LoadAssetAtPath<ItchUploaderPreferences>(AssetDatabase.GUIDToAssetPath(allPreferences[0]));
            } else if (allPreferences.Length > 1) {
                // Multiple preferences found
                Debug.LogWarning($"Found multiple preferences assets at: <b>{string.Join("</b> and <b>", allPreferences)}</b>, you should have only one preference at the moment");
                return null;
            }
            return null;
        }
    }
}
