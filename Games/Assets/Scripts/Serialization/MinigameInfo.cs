using System;
using Newtonsoft.Json;
using UnityEngine;

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