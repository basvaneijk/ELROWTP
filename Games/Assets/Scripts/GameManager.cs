using Assets.Scripts.Helpers;
using UnityEngine;

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