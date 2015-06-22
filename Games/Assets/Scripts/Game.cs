using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts.Serialization;
using Newtonsoft.Json;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;

#endif

namespace Assets.Scripts
{
    /// <summary>
    ///     Contains static logic about the game. Does not interfere with the game itself but only provides information.
    /// </summary>
    public static class Game
    {
        /// <summary>
        ///     The resources directory in the main Unity launcher.
        /// </summary>
        private const string ResourcesDir = "Assets\\Resources";

        /// <summary>
        ///     The folder in the resources directory which contains the minigames.
        /// </summary>
        private const string MinigameFolder = "Minigames";

        /// <summary>
        ///     The file which is being compiled by this script containing all necessary information about the minigames in json
        ///     format.
        /// </summary>
        private const string MinigameFile = "GeneratedMinigames";

        /// <summary>
        ///     The file which is located in the root of any minigame directory, contains information about the scene to launch and
        ///     the sprite to display in the main menu launcher.
        /// </summary>
        private const string MinigameJsonFile = "Minigame.json";

        /// <summary>
        ///     Needed for post-processing builds, this way the minigame information will only be generated once and not for every
        ///     scene.
        /// </summary>
        private static bool _hasCompiledFirstScene;

        /// <summary>
        ///     Retrieves all registered and active (pre-build) minigames which are ready to launch.
        /// </summary>
        /// <returns>A list of MinigameInfo</returns>
        public static List<MinigameInfo> GetActiveMinigames()
        {
            string json = Resources.Load<TextAsset>(MinigameFile).text;
            List<MinigameInfo> minigames = JsonConvert.DeserializeObject<List<MinigameInfo>>(json);
            return minigames;
        }

#if UNITY_EDITOR
        /// <summary>
        ///     This method is called when post-processing a scene, which occurs in either the editor when running a scene or at
        ///     build time when building a scene.
        /// </summary>
        /// <remarks>
        ///     This method is being called on PostProcessScene on purpose, this is because scenes are being processed before the
        ///     build actually happens. This way the generated file will be included when the build starts.
        /// </remarks>
        [PostProcessScene]
        private static void PostProcessScene()
        {
            //Determine if this script has already runned, if so then stop executing this script.
            if (_hasCompiledFirstScene)
            {
                return;
            }
            //Only generate files when build by the Unity Editor.
            if (!Application.isEditor)
            {
                return;
            }
            //Flip the bool to ensure this script is only being runned once per build.
            _hasCompiledFirstScene = true;
            LoadMinigames();
        }

        private static void LoadMinigames()
        {
            //List of all games that will be added to the MinigameFile.
            List<MinigameInfo> activeMinigames = new List<MinigameInfo>();
            //Used to assign the full path to a MinigameInfo, we do not need this information after this script completes so it wouldn't make sense to add it as a property.
            Dictionary<MinigameInfo, string> registeredMinigames = new Dictionary<MinigameInfo, string>();

            //Directory which contains all minigames.
            string minigameDir = Path.Combine(ResourcesDir, MinigameFolder);
            //Full path on local drive to the minigame directory.
            string searchDir = Path.Combine(Directory.GetCurrentDirectory(), minigameDir);

            //Loop through all minigame directories.
            foreach (string dir in Directory.GetDirectories(searchDir))
            {
                //Check if the directory contains the minigame json file (used to specify information about the minigame).
                string file = Directory.GetFiles(dir).FirstOrDefault(x => x == Path.Combine(dir, MinigameJsonFile));
                if (file == null)
                {
                    //Skip to next directory if json file has not been found.
                    continue;
                }

                //Retrieve json and retrieve minigame information.
                string serializedObject = ReadFileText(file);

                MinigameInfo minigameInfo = JsonConvert.DeserializeObject<MinigameInfo>(serializedObject);
                minigameInfo.Directory = Path.GetFileName(dir);
                registeredMinigames.Add(minigameInfo, dir);
            }

            //Loop through all scenes speciied in the buildsettings.
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                EditorBuildSettingsScene buildSettingsScene = EditorBuildSettings.scenes[i];
                if (!buildSettingsScene.enabled)
                {
                    //If the scene is not enabled in the buildsettings it won't be compiled properly so skip the scene.
                    continue;
                }

                //Loop through the minigame information (enriched with full paths).
                foreach (KeyValuePair<MinigameInfo, string> registeredMinigame in registeredMinigames)
                {
                    //The path according to the buildsettings
                    string buildSettingsScenePath = Path.Combine(Directory.GetCurrentDirectory(),
                                                                 buildSettingsScene.path);
                    //The current path (in this loop)
                    string localScenePath = Path.Combine(registeredMinigame.Value,
                                                         registeredMinigame.Key.RelativePathToScene);

                    //If the paths match up then this scene is configured properly and can be launched by Unity.
                    if (string.Equals(Path.GetFullPath(buildSettingsScenePath), localScenePath
                                      ,
                                      StringComparison.OrdinalIgnoreCase))
                    {
                        //Set the scene ID and add the minigame information to the active minigames list.
                        registeredMinigame.Key.Id = i;

                        //Create a sprite for the minigame
                        string imagePath = Path.Combine(registeredMinigame.Value,
                                                        registeredMinigame.Key.RelativePathToSprite);
                        byte[] imageBytes = ReadFileBytes(imagePath);
                        registeredMinigame.Key.SpriteBase64 = Convert.ToBase64String(imageBytes);

                        activeMinigames.Add(registeredMinigame.Key);
                        break;
                    }
                }
            }
            //Write all active minigames to disk.
            WriteMinigameConfigToFile(activeMinigames);
        }


        /// <summary>
        ///     Used to write a collection of active minigames to file in json format.
        /// </summary>
        /// <remarks>
        ///     This method writes a json formatted string to a file with a .txt extension, this is on purpose since Unity is
        ///     retarded and can only load TextAssets from it's resources. tl;dr Keep this .txt
        /// </remarks>
        /// <param name="minigames">List of minigames you want to serialize and write to disk.</param>
        private static void WriteMinigameConfigToFile(List<MinigameInfo> minigames)
        {
            //Serialize the minigames.
            string json = JsonConvert.SerializeObject(minigames, Formatting.Indented);

            //Directory in which the minigame file will be saved.
            string relativePath = Path.Combine(ResourcesDir, MinigameFile);

            //IMPORTANT: Keep this thing a .txt, if you change it I'll haunt you in your dreams!
            string path = Path.Combine(Directory.GetCurrentDirectory(), relativePath) + ".txt";

            WriteFileText(path, json);
        }

        /// <summary>
        ///     Used to write files to disk.
        /// </summary>
        /// <param name="path">The full path including the filename and extension.</param>
        /// <param name="content">The content you want to write to the file.</param>
        private static void WriteFileText(string path, string content)
        {
            string dir = Path.GetDirectoryName(path);

            if (dir == null)
            {
                throw new DirectoryNotFoundException("Directory does not exist");
            }
            if (!Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException("Directory does not exist");
            }

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(content);
            }
        }

        /// <summary>
        ///     Used to read files from disk.
        /// </summary>
        /// <param name="path">The full path including the filename and extension.</param>
        /// <returns>The file content as a string.</returns>
        private static string ReadFileText(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File does not exist", path);
            }
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     Used to read binary data files from disk.
        /// </summary>
        /// <param name="path">The filepath to be read.</param>
        /// <returns></returns>
        private static byte[] ReadFileBytes(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File does not exist", path);
            }
            return File.ReadAllBytes(path);
        }
#endif
    }
}