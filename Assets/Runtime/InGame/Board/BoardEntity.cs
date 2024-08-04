using System;
using System.Collections.Generic;
using System.Linq;
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
        public static Dictionary<int, Action<KeyWord, List<CharMatch>>> OnBoardChecks = new Dictionary<int, Action<KeyWord, List<CharMatch>>>();
        public static Dictionary<int, Action> OnBoardCompletes = new Dictionary<int, Action>();
        
        private BoardLineCreator _lineCreator;
        private VerticalLayoutGroup _verticalLayoutGroup;

        private BoardLineEntity[] _lineEntities;
        private int _currentLine = 0;
        
        public int BoardId { get; set; }
        public KeyWord Target { get; set; }
        public bool IsBoardComplete { get; private set; } = false;
        
        private void Awake()
        {
            _lineCreator = GetComponent<BoardLineCreator>();
            TryGetComponent<VerticalLayoutGroup>(out _verticalLayoutGroup);
        }

        public void BuildBoard(int numberOfLetter)
        {
            _lineEntities = _lineCreator.CreateLines(numberOfLetter);
            _lineEntities[0].IsLineActive = true;
        }

        public void SetTarget(string target)
        {
            Target = target.ToKeyWord();
        }
        
        public void Write(KeyCode key)
        {
            if (IsBoardComplete) return;
            if (_lineEntities == null) return;
            if (_lineEntities.Length == 0) return;
            if (_currentLine >= _lineEntities.Length) return;
            
            _lineEntities[_currentLine].Write(key);
        }

        public void Backspace()
        {
            if (IsBoardComplete) return;
            if (_lineEntities == null) return;
            if (_lineEntities.Length == 0) return;
            if (_currentLine >= _lineEntities.Length) return;
            
            _lineEntities[_currentLine].Backspace();
        }

        public Tween CheckWord(KeyWord word)
        {
            if (IsBoardComplete) return DOTween.Sequence();
            
            var result = WordChecker.Instance.CheckWord(word, Target);
            
            _lineEntities[_currentLine].LineMatch = result;
            _lineEntities[_currentLine].IsLineActive = false;
            _lineEntities[_currentLine + 1].IsLineActive = true;
            
            return DoCheckWord(result).OnComplete(() =>
            {
                _currentLine += 1;
                if (OnBoardChecks.ContainsKey(BoardId))
                    OnBoardChecks[BoardId]?.Invoke(word, result);
            });
        }
        
        private Tween DoCheckWord(List<CharMatch> result)
        {
            DOTween.Kill(transform);
            var sequence = DOTween.Sequence(transform);
            var line = _lineEntities[_currentLine];
            for (var i = 0; i < line.CellEntities.Length; i++)
            {
                var cell = line.CellEntities[i];
                var cellColor = result[i].GetMatchColor();
                sequence.Append(cell.transform.DOScaleY(0f, 0.2f).SetEase(Ease.InQuad))
                    .AppendCallback(() =>
                    {
                        cell.CellImage.color = cellColor;
                        cell.CellText.color = ColorConst.Default.passedTextColor;
                    })
                    .Append(cell.transform.DOScaleY(1f, 0.05f));
            }

            sequence.AppendInterval(0.1f);

            var lineRect = (RectTransform)line.transform;
            var deltaSize = lineRect.sizeDelta;
            sequence.AppendCallback(() =>
            {
                lineRect.sizeDelta = new Vector2(deltaSize.x, deltaSize.y / BoardLineCreator.ActiveLineScale);
            });

            if (result.IsAllCorrect())
            {
                IsBoardComplete = true;
                if (OnBoardCompletes.ContainsKey(BoardId))
                    OnBoardCompletes[BoardId]?.Invoke();
                sequence.AppendCallback(() =>
                {
                    for (var i = _currentLine + 1; i < _lineEntities.Length; i++)
                    {
                        _lineEntities[i].gameObject.SetActive(false);
                    }
                });
            }
            
            if (!IsBoardComplete && _currentLine + 1 < _lineEntities.Length)
            {
                var inactiveLine = _lineEntities[_currentLine + 1];
                var inactiveLineRect = (RectTransform)inactiveLine.transform;
                sequence.AppendCallback(() =>
                {
                    inactiveLineRect.sizeDelta = deltaSize;
                });
                sequence.AppendCallback(() =>
                {
                    inactiveLine.LineColor = ColorConst.Default.activeLineColor;
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