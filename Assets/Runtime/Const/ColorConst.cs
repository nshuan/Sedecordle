using UnityEditor;
using UnityEngine;

namespace Runtime.Const
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ColorConst", fileName = "ColorConst", order = 1)]
    public class ColorConst : ScriptableObject
    {
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

        #region SINGLETON

        private const string Path = "ScriptableObjects/ColorConst";

        public static ColorConst Default
        {
            get
            {
                var result = Resources.Load<ColorConst>(Path);
                if (result != null) return result;
                var instance = ScriptableObject.CreateInstance<ColorConst>();
#if UNITY_EDITOR
                AssetDatabase.CreateAsset(instance, "Assets/Resources/" + Path);
                AssetDatabase.SaveAssets();
#endif
                return instance;
            }
        }

        #endregion
    }
}