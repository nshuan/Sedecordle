using System;
using System.Collections.Generic;
using Core.Singleton;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Core.PopUp
{
    public class PopUp : MonoSingleton<PopUp>
    {
        [SerializeField] private GameObject[] popUpPrefabs;

        private static Dictionary<Type, IPopUp> _popUpMap;
        private static Dictionary<Type, IPopUp> _popUpCache;
        
        protected override void Awake()
        {
            base.Awake();

            SceneManager.sceneLoaded += OnLoadScene;

            _popUpMap = new Dictionary<Type, IPopUp>();
            _popUpCache = new Dictionary<Type, IPopUp>();
            foreach (var prefab in popUpPrefabs)
            {
                if (!prefab.TryGetComponent<IPopUp>(out var component))
                {
#if UNITY_EDITOR
                    Debug.LogError("Prefab " + prefab.name + " must have component derived from IPopUp!");
#endif
                    continue;
                }
                var t = component.GetType();
                _popUpMap.Add(t, component);
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnLoadScene;
        }

        public static T Get<T>() where T : MonoBehaviour, IPopUp
        {
            var t = typeof(T);
            if (_popUpCache.TryGetValue(t, out var value))
            {
                if (value != null) 
                    return (T)value;
            }

            if (!_popUpMap.TryGetValue(t, out value))
            {
#if UNITY_EDITOR
                Debug.LogError("There is no pop-up of type " + t);
#endif
                return null;
            }
            var prefab = (T)value;
            var popUp = Instantiate(prefab);
            popUp.gameObject.SetActive(false);
            _popUpCache[t] = popUp;
            return popUp;
        }

        public static void Release<T>(T popUp) where T : MonoBehaviour, IPopUp
        {
            popUp.gameObject.SetActive(false);
        }

        public static Tween Show<T>(T popUp) where T : MonoBehaviour, IPopUp
        {
            popUp.gameObject.SetActive(true);
            
            // Todo show animation
            return DOTween.Sequence();
        }

        public static Tween Hide<T>(T popUp) where T : MonoBehaviour, IPopUp
        {
            Release(popUp);
            
            // Todo hide animation
            return DOTween.Sequence();
        }
        
        public static Tween Hide(GameObject popUp)
        {
            popUp.gameObject.SetActive(false);
            
            // Todo hide animation
            return DOTween.Sequence();
        }

        private void OnLoadScene(Scene scene, LoadSceneMode loadSceneMode)
        {
            _popUpCache.Clear();
        }
    }
}