using DG.Tweening;
using EasyButtons;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.LoadingEffect
{
    public class LoadingIconEffect : MonoLoadingEffect
    {
        [SerializeField] private float duration = 0.1f;
        [SerializeField] private Image iconImage;

        private void OnDisable()
        {
            DoKill();
        }

        public override Tween DoEffect()
        {
            transform.localRotation = Quaternion.identity;
            return DOVirtual.DelayedCall(duration, () =>
                {
                    transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -45f) + transform.eulerAngles);
                })
                .SetTarget(transform)
                .SetLoops(-1, LoopType.Incremental)
                .SetRelative();
        }

        public override Tween DoHide(float hideDuration)
        {
            return iconImage.DOFade(0f, hideDuration);
        }

        [Button]
        private void DoKill()
        {
            transform.DOKill();
        }
    }
}