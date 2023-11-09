﻿using Il2CppTMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlasII.ModdingAPI.UI
{
    /// <summary>
    /// Provides convenient methods for creating and modifying UI elements
    /// </summary>
    public static class UIModder
    {
        /// <summary>
        /// Contains TMP_FontAsset objects
        /// </summary>
        public static Fonts Fonts { get; private set; } = new();

        /// <summary>
        /// Contains RectTransform objects
        /// </summary>
        public static Parents Parents { get; private set; } = new();

        /// <summary>
        /// Creates a RectTransform with default parameters
        /// </summary>
        public static RectTransform CreateRect(string name, Transform parent)
        {
            var rect = new GameObject(name).AddComponent<RectTransform>();
            rect.SetParent(parent, false);
            return rect.ResetToDefault();
        }

        /// <summary>
        /// Creates a RectTransform with default parameters
        /// </summary>
        public static RectTransform CreateRect(string name) => CreateRect(name, Parents.Default);

        /// <summary>
        /// Creates an Image with default parameters
        /// </summary>
        public static Image CreateImage(string name, Transform parent)
        {
            var rect = CreateRect(name, parent);
            return rect.AddImage();
        }

        /// <summary>
        /// Creates an Image with default parameters
        /// </summary>
        public static Image CreateImage(string name) => CreateImage(name, Parents.Default);

        /// <summary>
        /// Creates a TextMeshProUGUI with default parameters
        /// </summary>
        public static TextMeshProUGUI CreateText(string name, Transform parent)
        {
            var rect = CreateRect(name, parent);
            return rect.AddText();
        }

        /// <summary>
        /// Creates a TextMeshProUGUI with default parameters
        /// </summary>
        public static TextMeshProUGUI CreateText(string name) => CreateText(name, Parents.Default);
    }

    /// <summary>
    /// Contains TMP_FontAsset objects
    /// </summary>
    public class Fonts
    {
        internal TMP_FontAsset Default => Blasphemous;

        /// <summary> Pixelated Blasphemous font </summary>
        public TMP_FontAsset Blasphemous { get; internal set; }

        /// <summary> Standard Arial font </summary>
        public TMP_FontAsset Arial { get; internal set; }

        /// <summary>
        /// Locates and stores font objects
        /// </summary>
        internal void Initialize()
        {
            Blasphemous = Object.FindObjectOfType<TextMeshProUGUI>()?.font;
            Arial = TMP_FontAsset.CreateFontAsset(Resources.GetBuiltinResource<Font>("Arial.ttf"));
        }
    }

    /// <summary>
    /// Contains RectTransform objects
    /// </summary>
    public class Parents
    {
        internal Transform Default => Canvas;

        /// <summary> Parent of all UI </summary>
        public Transform Canvas { get; internal set; }

        /// <summary> Parent of the main menu UI </summary>
        public Transform MainMenu => Canvas?.Find("Interfaces/MainMenuWindow_prefab(Clone)");

        /// <summary> Parent of the game logic UI </summary>
        public Transform GameLogic => Canvas?.Find("InGame/InGameWindow_prefab(Clone)");

        /// <summary>
        /// Locates and stores transform objects
        /// </summary>
        internal void Initialize()
        {
            Canvas = Object.FindObjectOfType<CanvasScaler>()?.transform;
        }
    }
}
