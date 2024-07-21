using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;
using Runtime.Const;
using Runtime.InGame.WordChecking;

namespace Runtime.InGame.Board
{
    [RequireComponent(typeof(BoardLineCreator))]
    public class BoardEntity : MonoBehaviour
    {
        private BoardLineCreator _lineCreator;
        private VerticalLayoutGroup _verticalLayoutGroup;

        private BoardLineEntity[] _lineEntities;
        private int _currentLine = 0;
        
        public KeyWord Target { get; set; }
        
        private void Awake()
        {
            _lineCreator = GetComponent<BoardLineCreator>();
            TryGetComponent<VerticalLayoutGroup>(out _verticalLayoutGroup);
        }

        public void BuildBoard(int numberOfLetter)
        {
            _lineEntities = _lineCreator.CreateLines(numberOfLetter);
        }

        public void SetTarget(string target)
        {
            Target = target.ToKeyWord();
        }
        
        public void Write(KeyCode key)
        {
            if (_lineEntities == null) return;
            if (_lineEntities.Length == 0) return;
            if (_currentLine >= _lineEntities.Length) return;
            
            _lineEntities[_currentLine].Write(key);
        }

        public void Backspace()
        {
            if (_lineEntities == null) return;
            if (_lineEntities.Length == 0) return;
            if (_currentLine >= _lineEntities.Length) return;
            
            _lineEntities[_currentLine].Backspace();
        }

        public void CheckWord(KeyWord word)
        {
            DoCheckWord(word).Play().OnComplete(() =>
            {
                _currentLine += 1;
            });
        }
        
        private Tween DoCheckWord(KeyWord word)
        {
            var result = WordChecker.Instance.CheckWord(word, Target);

            transform.DOKill();
            var sequence = DOTween.Sequence(transform);
            var line = _lineEntities[_currentLine];
            for (var i = 0; i < line.CellEntities.Length; i++)
            {
                var cell = line.CellEntities[i];
                var cellColor = result[i].GetMatchColor();
                sequence.Append(cell.transform.DOScaleY(0f, 0.2f).SetEase(Ease.InQuad))
                    .AppendCallback(() => cell.CellImage.color = cellColor)
                    .Append(cell.transform.DOScaleY(1f, 0.05f));
            }

            var lineRect = (RectTransform)line.transform;
            var deltaSize = lineRect.sizeDelta;
            sequence.AppendCallback(() =>
            {
                lineRect.sizeDelta = new Vector2(deltaSize.x, deltaSize.y / BoardLineCreator.ActiveLineScale);
            });
            
            if (_currentLine + 1 < _lineEntities.Length)
            {
                var inactiveLine = _lineEntities[_currentLine + 1];
                var inactiveLineRect = (RectTransform)inactiveLine.transform;
                // inactiveLineRect.pivot = new Vector2(0.5f, 0);
                // sequence.JoinCallback(() => inactiveLineRect.pivot = new Vector2(0.5f, 0));
                sequence.AppendCallback(() =>
                {
                    inactiveLineRect.sizeDelta = deltaSize;
                });
                sequence.AppendCallback(() =>
                {
                    inactiveLine.LineColor = ColorConst.Default.activeLineColor;
                    // inactiveLineRect.pivot = new Vector2(0.5f, 1f);
                    // if (_verticalLayoutGroup is not null) _verticalLayoutGroup.enabled = true;
                });
            }

            sequence.AppendCallback(() =>
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(lineRect);
            });

            return sequence;
        }
    }
}