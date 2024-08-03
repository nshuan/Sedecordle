using UnityEditor;
using UnityEngine;

namespace Runtime.Const
{
    [CreateAssetMenu(menuName = "Scriptable Objects/GameConst", fileName = "GameConst", order = 1)]
    public class GameConst : ScriptableObject
    {
        public int minNumberOfLetter = 4;
        public int maxNumberOfLetter = 6;

        #region SINGLETON

        private const string Path = "ScriptableObjects/GameConst";

        public static GameConst Default
        {
            get
            {
                var result = Resources.Load<GameConst>(Path);
                if (result != null) return result;
                var instance = ScriptableObject.CreateInstance<GameConst>();
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