using System;
using Newtonsoft.Json;
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

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class MinigameInfo
    {
        /// <summary>
        ///     The name of the minigame which is being displayed by the launcher.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The Id of the scene to launch for this minigame.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The relative path to the scene to launch.
        /// </summary>
        public string RelativePathToScene { get; set; }

        /// <summary>
        ///     The relative path to the sprite to associate with this minigame (displayed by the launcher).
        /// </summary>
        public string RelativePathToSprite { get; set; }

        /// <summary>
        ///     The name of the root directory which contains the minigame.
        /// </summary>
        /// <remarks>
        ///     This property does not contain a relative or full path, it contains the directory name only, for example:
        ///     "CoinCollector".
        /// </remarks>
        public string Directory { get; set; }

        /// <summary>
        ///     The Sprite associated with the minigame, used by the launcher.
        /// </summary>
        [JsonIgnore]
        public Sprite Sprite
        {
            get
            {
                Texture2D texture = new Texture2D(512, 512, TextureFormat.DXT1, true);
                texture.LoadImage(Convert.FromBase64String(SpriteBase64));
                Sprite sprite = Sprite.Create(texture,
                                              new Rect(0, 0, texture.width, texture.height),
                                              new Vector2(0, 0));
                return sprite;
            }
        }

        /// <summary>
        /// Used to serialize the sprite for the minigame because Unity is retarded and wont pack sprites when they reside inside the resources folder.
        /// </summary>
        public string SpriteBase64 { get; set; }
    }
}