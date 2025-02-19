using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Runtime.Const;
using Runtime.DarkMode;
using Runtime.InGame.Board;
using Runtime.InGame.WordChecking;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runtime.InGame.Keyboard
{
    public class KeyboardKey : MonoBehaviour, IPointerClickHandler, IAffectedByDarkMode
    {
        public static event Action<KeyCode> OnVirtualKeyPressed;

        [SerializeField] private Text letterText;
        [SerializeField] protected Image letterImage;
        [SerializeField] private KeyCode letter;
        [SerializeField] private Transform cover;
        private GridLayoutGroup _coverGrid;
        private RectTransform _coverRect;

        public KeyCode Letter => letter;
        private List<KeyboardKeyLetterView> _reviewCells;
        
        private void Awake()
        {
            letterText.text = letter.ToString();
            letterText.color = ColorConst.Default.keyboardLetterColor;
            letterImage.color = ColorConst.Default.keyboardKeyColor;

            // if (!cover.TryGetComponent<GridLayoutGroup>(out _coverGrid))
            // {
            //     _coverGrid = cover.AddComponent<GridLayoutGroup>();
            // }

            _coverRect = (RectTransform)cover;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnVirtualKeyPressed?.Invoke(letter);
        }

        public void SetupKey(int col, int row)
        {
            _reviewCells = new List<KeyboardKeyLetterView>();
            foreach (Transform child in cover)
            {
                Destroy(child.gameObject);
            }
            
            // _coverGrid.padding = new RectOffset();
            // _coverGrid.spacing = Vector2.zero;
            // _coverGrid.startCorner = GridLayoutGroup.Corner.UpperLeft;
            // _coverGrid.startAxis = GridLayoutGroup.Axis.Horizontal;
            // _coverGrid.childAlignment = TextAnchor.UpperLeft;
            // _coverGrid.constraint = GridLayoutGroup.Constraint.Flexible;
            // LayoutRebuilder.ForceRebuildLayoutImmediate(_coverRect);

            var coverSize = _coverRect.rect.size;
            var cellSize = new Vector2(coverSize.x / col, coverSize.y / row);
            // _coverGrid.cellSize = cellSize;
            
            _reviewCells = new List<KeyboardKeyLetterView>();
            for (var i = 0; i < col * row; i++)
            {
                var newPreviewCell = new GameObject("PreviewCell");
                var rectTransform = newPreviewCell.AddComponent<RectTransform>();
                rectTransform.SetParent(cover);
                rectTransform.localScale = Vector3.one;
                rectTransform.anchorMin = new Vector2(0, 1);
                rectTransform.anchorMax = new Vector2(0, 1);
                rectTransform.pivot = new Vector2(0, 1);
                rectTransform.sizeDelta = cellSize;
                rectTransform.localPosition = 
                    new Vector2(i%col*cellSize.x, -(i/col)*cellSize.y) + new Vector2(-coverSize.x/2, coverSize.y/2);
                var image = newPreviewCell.AddComponent<Image>();
                var script = newPreviewCell.AddComponent<KeyboardKeyLetterView>();
                script.SetImage(image);
                image.color = Color.clear;
                _reviewCells.Add(script);
            }
            
            // Setup events
            var amount = BoardManager.BoardAmount;
            for (var i = 0; i < amount; i++)
            {
                if (i >= _reviewCells.Count) break;

                var cell = _reviewCells[i];
                Action<KeyWord, List<CharMatch>> checkAction = (checkWord, match) =>
                {
                    if (cell.CurrentValue == CharMatch.Correct) return;
                    for (var index = 0; index < checkWord.Count; index++)
                    {
                        if (checkWord[index] != letter) continue;
                        if (match[index] < cell.CurrentValue) continue;
                        cell.CurrentValue = match[index];
                        if (cell.CurrentValue == CharMatch.Correct) break;
                    }
                };
                if (!BoardEntity.OnBoardChecks.TryAdd(i, checkAction))
                    BoardEntity.OnBoardChecks[i] += checkAction;
                BoardEntity.OnBoardChecks[i] += FixCells;
            }
        }

        private void FixCells(KeyWord keyword, List<CharMatch> result)
        {
            if (!keyword.Contains(letter)) return;
            foreach (var cell in _reviewCells.Where(cell => cell.CurrentValue <= CharMatch.NotExist))
            {
                cell.CurrentValue = CharMatch.NotExist;
            }
        }

        public virtual Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return DOTween.Sequence(transform)
                .Join(letterText.DOColor(colorPalette.keyboardLetterColor, 0.2f).SetEase(Ease.OutQuint))
                .Join(letterImage.DOColor(colorPalette.keyboardKeyColor, 0.2f).SetEase(Ease.OutQuint));
        }
    }
}