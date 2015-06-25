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