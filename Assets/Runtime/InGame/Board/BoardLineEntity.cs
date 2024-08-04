using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;
using Runtime.Const;
using Runtime.DarkMode;
using Runtime.InGame.WordChecking;

namespace Runtime.InGame.Board
{
    [RequireComponent(typeof(BoardCellCreator))]   
    public class BoardLineEntity : MonoBehaviour, IAffectedByDarkMode
    {
        private BoardCellCreator _cellCreator;

        public List<CharMatch> LineMatch { get; set; }
        public bool IsLineActive { get; set; }
        
        public Color LineColor
        {
            get
            {
                if (CellEntities == null) return ColorConst.Default.pendingLineColor;
                if (CellEntities.Length == 0) return ColorConst.Default.pendingLineColor;
                return CellEntities[0].CellImage.color;
            }
            set
            {
                if (CellEntities == null) return;
                foreach (var cell in CellEntities)
                {
                    cell.CellImage.color = value;
                }
            }
        }
        
        public BoardCellEntity[] CellEntities { get; private set; }
        private int _currentCell = 0;
        
        private void Awake()
        {
            _cellCreator = GetComponent<BoardCellCreator>();
        }

        public void InitBoardLine(int cellAmount)
        {
            CellEntities = _cellCreator.CreateCells(cellAmount);
        }

        public void Write(KeyCode key)
        {
            if (CellEntities == null) return;
            if (CellEntities.Length == 0) return;
            if (_currentCell >= CellEntities.Length) return;
            
            CellEntities[_currentCell].Write(key);
            _currentCell += 1;
        }

        public void Backspace()
        {
            if (CellEntities == null) return;
            if (CellEntities.Length == 0) return;
            _currentCell -= 1;
            if (_currentCell >= CellEntities.Length) return;

            CellEntities[_currentCell].Delete();
        }

        public Tween DoChangeColorMode(ColorConst colorPalette)
        {
            var seq = DOTween.Sequence(transform);
            
            if (LineMatch == null)
            {
                if (!IsLineActive)
                {
                    foreach (var cell in CellEntities)
                    {
                        seq.Join(cell.CellImage.DOColor(colorPalette.pendingLineColor, 0.2f).SetEase(Ease.OutQuint));
                    }
                }
                else
                {
                    foreach (var cell in CellEntities)
                    {
                        seq.Join(cell.CellImage.DOColor(colorPalette.activeLineColor, 0.2f).SetEase(Ease.OutQuint))
                            .Join(cell.CellText.DOColor(colorPalette.activeTextColor, 0.2f).SetEase(Ease.OutQuint));
                    }
                }
            }
            else
            {
                for (var i = 0; i < LineMatch.Count; i++)
                {
                    seq.Join(
                        CellEntities[i].CellText.DOColor(colorPalette.passedTextColor, 0.2f).SetEase(Ease.OutQuint));
                    switch (LineMatch[i])
                    {
                        case CharMatch.Correct:
                            seq.Join(CellEntities[i].CellImage.DOColor(colorPalette.correctColor, 0.2f)
                                .SetEase(Ease.OutQuint));
                            break;
                        case CharMatch.NotPlace:
                            seq.Join(CellEntities[i].CellImage.DOColor(colorPalette.notPlaceColor, 0.2f)
                                .SetEase(Ease.OutQuint));
                            break;
                        default:
                            seq.Join(CellEntities[i].CellImage.DOColor(colorPalette.notExistColor, 0.2f)
                                .SetEase(Ease.OutQuint));
                            break;
                    }
                }
            }

            return seq;
        }
    }
}