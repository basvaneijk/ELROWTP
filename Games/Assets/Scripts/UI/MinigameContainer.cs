using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// The currently selected minigame, is null when nothing is selected.
        /// </summary>
        public Minigame SelectedMinigame
        {
            get
            {
                return Minigames.FirstOrDefault(x => x.IsSelected);
            }
        }
    }
}