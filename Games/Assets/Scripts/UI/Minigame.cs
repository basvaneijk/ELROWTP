using System.Linq;
using Assets.Scripts.Serialization;
using UnityEngine;
using UnityEngine.UI;

//   This script is part of the ELRO Minigame Launcher
//   Copyright (C) 2015 Robin Kuijt
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//
//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     This script is attached to the Minigame prefab and is responsible for the UI logic and can be accessed to identify
    ///     the associated Minigame.
    /// </summary>
    public class Minigame : MonoBehaviour
    {
        /// <summary>
        ///     Contains the information about the minigame.
        /// </summary>
        private MinigameInfo minigameInfo;

        /// <summary>
        ///     Indicates whether or not the state is isSelected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        ///     This method is being called by Unity on startup.
        /// </summary>
        private void Start()
        {
            //Add a listener so the player can click on the minigame to select it.
            GetComponent<Button>().onClick.AddListener(Select);
        }

        /// <summary>
        ///     Used to get or set information about the minigame, when set the UI will automatically update to the new minigame
        ///     information.
        /// </summary>
        public MinigameInfo MinigameInfo
        {
            get
            {
                return minigameInfo;
            }
            set
            {
                minigameInfo = value;
                UpdateUiElements();
            }
        }

        /// <summary>
        ///     Get or set the selected state, when set to true it will automatically deselect all other minigames.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (value)
                {
                    //Deselect all items before selecting this one.
                    transform.parent.GetComponent<MinigameContainer>()
                             .Minigames.ToList()
                             .ForEach(x => x.IsSelected = false);
                }
                isSelected = value;
                GameObject selectedImageObj = transform.Find("Selected").gameObject;
                selectedImageObj.SetActive(value);
            }
        }

        /// <summary>
        ///     Used to select this minigame.
        /// </summary>
        public void Select()
        {
            IsSelected = true;
        }

        /// <summary>
        ///     Updates the UI elements according to the current MinigameInfo.
        /// </summary>
        private void UpdateUiElements()
        {
            //Update text label
            Text textComponent = transform.Find("Text").GetComponent<Text>();
            textComponent.text = minigameInfo.Name;

            //Update image
            if (minigameInfo.RelativePathToSprite != null)
            {
                Sprite sprite = minigameInfo.Sprite;
                Image image = transform.Find("Image").GetComponent<Image>();
                image.sprite = sprite;
            }
        }
    }
}