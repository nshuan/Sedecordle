using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Const
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ColorConst", fileName = "ColorConst", order = 1)]
    public class ColorConst : ScriptableObject
    {
        [SerializeField] public ColorMode colorMode;

        [Space] 
        [SerializeField] public Color backgroundColor;
        
        [Space]
        [SerializeField] public Color activeLineColor;
        [SerializeField] public Color pendingLineColor;
        
        [Space]
        [Header("Text")]
        [SerializeField] public Color activeTextColor;
        [SerializeField] public Color passedTextColor;
        [SerializeField] public Color keyboardLetterColor; 
        [SerializeField] public Color keyboardKeyColor; 

        [Space] 
        [Header("Passed line colors")] 
        [SerializeField] public Color notExistColor;
        [SerializeField] public Color notPlaceColor;
        [SerializeField] public Color correctColor;

        [Space] 
        [Header("Icons")] 
        [SerializeField] public Color iconColor;

        #region SINGLETON

        private const string Path = "ScriptableObjects/ColorConst";

        private static ColorConst _defaultInstance;
        public static ColorConst Default
        {
            get
            {
                if (_defaultInstance == null)
                {
                    var gameSetting = GameSettings.Load;
                    var allColor = Resources.LoadAll<ColorConst>(Path);
                    _defaultInstance = allColor[0];
                    foreach (var palette in allColor)
                    {
                        if (palette.colorMode != gameSetting.colorMode) continue;
                        _defaultInstance = palette;
                        break;
                    }
                }
                return _defaultInstance;
            }
        }

        public static ColorConst Reload
        {
            get
            {
                var gameSetting = GameSettings.Load;
                var allColor = Resources.LoadAll<ColorConst>(Path);
                _defaultInstance = allColor[0];
                foreach (var palette in allColor)
                {
                    if (palette.colorMode != gameSetting.colorMode) continue;
                    _defaultInstance = palette;
                    break;
                }

                return _defaultInstance;
            }
        }

        #endregion
    }

    [Serializable]
    public enum ColorMode
    {
        Light,
        Dark
    }

    public static class ColorUtility
    {
        public static void SetColor(this Graphic graphic, Color color, bool keepAlpha = true)
        {
            if (keepAlpha)
            {
                var a = graphic.color.a;
                graphic.color = color;
                graphic.color += new Color(0f, 0f, 0f, a - graphic.color.a);
            }
            else
            {
                graphic.color = color;
            }
        }
    }
}