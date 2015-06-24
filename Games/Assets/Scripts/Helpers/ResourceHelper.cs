using UnityEngine;

namespace Assets.Scripts.Helpers
{
    /// <summary>
    ///     Used to load internal resources so they can be referenced by name instead of using strings.
    /// </summary>
    internal static class ResourceHelper
    {
        /// <summary>
        ///     The Minigame panel which is being instantiated by the launcher (contains an image for the minigame and text to
        ///     display the name).
        /// </summary>
        public static GameObject MinigameUiPanel;

        /// <summary>
        ///     Called by the GameManager on startup, used to load all resources.
        /// </summary>
        public static void Load()
        {
            MinigameUiPanel = (GameObject) Resources.Load("Prefabs/MinigameUiPanel");
        }
    }
}