using System;
using System.Collections;
using Core.Singleton;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;

namespace Runtime.LoadingEffect
{
    public class Loading : MonoSingleton<Loading>
    {
        [SerializeField] private Image loadingImage;
        [SerializeField] private MonoLoadingEffect[] effects;

        public static bool IsLoading { get; set; } = false;

        protected override void Awake()
        {
            base.Awake();
            
            loadingImage.gameObject.SetActive(false);
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
            });
        }

        private IEnumerator IEWaiting(float duration, Action onFinish)
        {
            yield return new WaitForSeconds(duration);
            onFinish?.Invoke();
        }

        public static void Show(float duration)
        {
            Instance.DoShow(duration);
        }
    }
}