using System;
using Core.PopUp;
using DG.Tweening;
using Runtime.Const;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime.Home.SelectHardness
{
    public class HardnessSelector : MonoBehaviour, IPopUp
    {
        [SerializeField] private Button subButton, addButton;
        [SerializeField] private Text hardnessText, leftText, rightText;
        [SerializeField] private Button saveButton;

        private Text[] _hardnessTexts;
        private int _hardness = 5;
        private bool _blockUI = false;
        public bool Saved { get; set; } = false;

        private void Awake()
        {
            _hardnessTexts = new Text[3];
            _hardnessTexts[0] = leftText;
            _hardnessTexts[1] = hardnessText;
            _hardnessTexts[2] = rightText;
        }

        private void OnEnable()
        {
            Saved = false;
            _hardnessTexts[0].text = (_hardness - 1).ToString();
            _hardnessTexts[1].text = _hardness.ToString();
            _hardnessTexts[2].text = (_hardness + 1).ToString();
            
            subButton.onClick.AddListener(Sub);
            addButton.onClick.AddListener(Add);
            saveButton.onClick.AddListener(Save);
        }

        private void OnDisable()
        {
            subButton.onClick.RemoveAllListeners();
            addButton.onClick.RemoveAllListeners();
            saveButton.onClick.RemoveAllListeners();
        }

        private void Sub()
        {
            if (_blockUI) return;
            if (_hardness == GameConst.Default.minNumberOfLetter) return;
            _hardness -= 1;
            _blockUI = true;
            DoSub().OnComplete(() => _blockUI = false);
        }

        private void Add()
        {
            if (_blockUI) return;
            if (_hardness == GameConst.Default.maxNumberOfLetter) return;
            _hardness += 1;
            _blockUI = true;
            DoAdd().OnComplete(() => _blockUI = false);
        }

        private void Save()
        {
            GameSettings.Load.hardness = _hardness;
            GameSettings.Save();
            Saved = true;
        }

        private Tween DoSub()
        {
            var seq = DOTween.Sequence(transform);

            foreach (var text in _hardnessTexts)
            {
                seq.Join(text.transform.DOLocalMoveX(128f, 0.2f).SetEase(Ease.InQuad).SetRelative());
            }

            seq.Join(_hardnessTexts[0].transform.DOScale(2f, 0.2f).SetEase(Ease.InQuad));
            seq.Join(_hardnessTexts[1].transform.DOScale(1f, 0.2f).SetEase(Ease.InQuad));
            seq.Join(_hardnessTexts[2].DOFade(0f, 0.2f).SetEase(Ease.InQuad));
            seq.Join(_hardnessTexts[1].DOFade(0.2f, 0.2f).SetEase(Ease.InQuad));
            seq.Join(_hardnessTexts[0].DOFade(1f, 0.2f).SetEase(Ease.InQuad));

            seq.AppendCallback(() =>
            {
                _hardnessTexts[2].transform.SetAsFirstSibling();
                _hardnessTexts[2].transform.localPosition += new Vector3(-128 * _hardnessTexts.Length, 0f, 0f);
                _hardnessTexts[2].text = (_hardness - 1).ToString();
                var sub = _hardnessTexts[2];
                _hardnessTexts[2] = _hardnessTexts[1];
                _hardnessTexts[1] = _hardnessTexts[0];
                _hardnessTexts[0] = sub;
            });

            if (_hardness > GameConst.Default.minNumberOfLetter)
                seq.Append(_hardnessTexts[2].DOFade(0.2f, 0.15f).SetEase(Ease.InQuad));

            return seq;
        }

        private Tween DoAdd()
        {
            var seq = DOTween.Sequence(transform);

            foreach (var text in _hardnessTexts)
            {
                seq.Join(text.transform.DOLocalMoveX(-128f, 0.2f).SetEase(Ease.InQuad).SetRelative());
            }

            seq.Join(_hardnessTexts[2].transform.DOScale(2f, 0.2f).SetEase(Ease.InQuad));
            seq.Join(_hardnessTexts[1].transform.DOScale(1f, 0.2f).SetEase(Ease.InQuad));
            seq.Join(_hardnessTexts[0].DOFade(0f, 0.2f).SetEase(Ease.InQuad));
            seq.Join(_hardnessTexts[1].DOFade(0.2f, 0.2f).SetEase(Ease.InQuad));
            seq.Join(_hardnessTexts[2].DOFade(1f, 0.2f).SetEase(Ease.InQuad));

            seq.AppendCallback(() =>
            {
                _hardnessTexts[0].transform.SetAsLastSibling();
                _hardnessTexts[0].transform.localPosition += new Vector3(128 * _hardnessTexts.Length, 0f, 0f);
                _hardnessTexts[0].text = (_hardness + 1).ToString();
                var sub = _hardnessTexts[0];
                _hardnessTexts[0] = _hardnessTexts[1];
                _hardnessTexts[1] = _hardnessTexts[2];
                _hardnessTexts[2] = sub;
            });

            if (_hardness < GameConst.Default.maxNumberOfLetter)
                seq.Append(_hardnessTexts[0].DOFade(0.2f, 0.15f).SetEase(Ease.InQuad));

            return seq;
        }
    }
}