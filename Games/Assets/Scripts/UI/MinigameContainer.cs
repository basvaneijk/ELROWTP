using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     This script is attached to the container (wrapper) of the Minigame prefabs. It serves like a List style object.
    /// </summary>
    public class MinigameContainer : MonoBehaviour
    {
        /// <summary>
        ///     All minigames which are currently displayed in the UI.
        /// </summary>
        public IEnumerable<Minigame> Minigames
        {
            get
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    yield return transform.GetChild(i).GetComponent<Minigame>();
                }
            }
        }
    }
}