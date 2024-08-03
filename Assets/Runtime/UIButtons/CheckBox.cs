using System;
using DG.Tweening;
using Runtime.Const;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EasyButtons;

namespace Runtime.UIButtons
{
    public class CheckBox : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image handle;
        [SerializeField] private Transform checkAnchor;
        [SerializeField] private Transform uncheckAnchor;
        [SerializeField] private Image bgImage;

        [Space]
        [SerializeField] private Button.ButtonClickedEvent actionOnCheck;
        [SerializeField] private Button.ButtonClickedEvent actionOnUncheck;

        [Space] 
        [SerializeField] private float duration = 0.2f;    
        
        private bool _isCheck;
        private bool _blockClick = false;
        
        public void SetCheck(bool isCheck)
        {
            _isCheck = isCheck;
            if (isCheck) DoCheck();
            else DoUncheck();
        }

        [Button]
        private Tween DoCheck()
        {
            DOTween.Kill(transform);
            var seq = DOTween.Sequence(transform);

            seq.Append(handle.transform.DOLocalMove(checkAnchor.localPosition, duration).SetEase(Ease.OutQuint))
                .Join(bgImage.DOColor(ColorConst.Default.correctColor, duration).SetEase(Ease.OutQuint));

            return seq;
        }

        [Button]
        private Tween DoUncheck()
        {
            DOTween.Kill(transform);
            var seq = DOTween.Sequence(transform);

            seq.Append(handle.transform.DOLocalMove(uncheckAnchor.localPosition, duration).SetEase(Ease.OutQuint))
                .Join(bgImage.DOColor(ColorConst.Default.notExistColor, duration).SetEase(Ease.OutQuint));

            return seq;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_blockClick) return;

            _blockClick = true;
            if (_isCheck)
            {
                _isCheck = false;
                DoUncheck().OnComplete(() => _blockClick = false);
                actionOnUncheck?.Invoke();
            }
            else
            {
                _isCheck = true;
                DoCheck().OnComplete(() => _blockClick = false);
                actionOnCheck?.Invoke();
            }
        }
    }
}