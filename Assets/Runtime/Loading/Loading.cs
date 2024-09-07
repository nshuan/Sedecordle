using System;
using System.Collections;
using Core.Singleton;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;
using Runtime.Const;
using Runtime.DarkMode;

namespace Runtime.LoadingEffect
{
    public class Loading : MonoSingleton<Loading>, IAffectedByDarkMode
    {
        [SerializeField] private Image loadingImage;
        [SerializeField] private MonoLoadingEffect[] effects;

        private Canvas _canvas;

        public static bool IsLoading { get; set; } = false;

        protected override void Awake()
        {
            base.Awake();
            
            loadingImage.SetColor(ColorConst.Default.backgroundColor);
            loadingImage.gameObject.SetActive(false);
            _canvas = GetComponent<Canvas>();
        }

        [Button]
        public void DoShow(float duration)
        {
            loadingImage.gameObject.SetActive(true);
            IsLoading = true;
            
            foreach (var effect in effects)
            {
                effect.DoEffect();
            }

            StartCoroutine(IEWaiting(duration, Hide));
        }

        public void Hide()
        {
            var sequence = DOTween.Sequence();
            const float hideDuration = 0.3f;

            sequence.Append(loadingImage.DOFade(0f, hideDuration));
            foreach (var effect in effects)
            {
                sequence.Join(effect.DoHide(hideDuration));
            }

            sequence.Play().OnComplete(() =>
            {
                IsLoading = false;
                loadingImage.gameObject.SetActive(false);
                loadingImage.color += new Color(0f, 0f, 0f, 1f);
            });
        }

        private IEnumerator IEWaiting(float duration, Action onFinish)
        {
            yield return new WaitForSeconds(duration);
            _canvas.worldCamera = Camera.current;
            onFinish?.Invoke();
        }

        public static void Show(float duration)
        {
            Instance.DoShow(duration);
        }

        public Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return loadingImage.DOColor(colorPalette.backgroundColor, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}