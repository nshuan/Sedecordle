using UnityEditor;
using UnityEngine;

namespace Runtime.Const
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ColorConst", fileName = "ColorConst", order = 1)]
    public class ColorConst : ScriptableObject
    {
        [SerializeField] public Color activeLineColor;
        [SerializeField] public Color pendingLineColor;

        #region SINGLETON

        private const string Path = "ScriptableObjects/ColorConst";

        public static ColorConst Default
        {
            get
            {
                var result = Resources.Load<ColorConst>(Path);
                if (result != null) return result;
                var instance = ScriptableObject.CreateInstance<ColorConst>();
                AssetDatabase.CreateAsset(instance, "Assets/Resources/" + Path);
                AssetDatabase.SaveAssets();
                return instance;
            }
        }

        #endregion
    }
}