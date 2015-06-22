using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    ///     The gamemanager will be accessible to all minigames and contains global gamelogic.
    /// </summary>
    internal class GameManager : MonoBehaviour
    {
        /// <summary>
        ///     Called by Unity on startup
        /// </summary>
        private void Awake()
        {
            //Keep this script and the gameobject attached to it alive when another scene loads.
            DontDestroyOnLoad(this);

            //Load all resources.
            ResourceHelper.Load();
        }

        /// <summary>
        ///     Used by minigames to return to the main menu.
        /// </summary>
        public void BackToMainMenu()
        {
            //todo: I'm not sure if another gamemanager will be created with this.
            Application.LoadLevel(0);
        }
    }
}