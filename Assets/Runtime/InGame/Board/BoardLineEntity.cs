using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;
using Runtime.Const;

namespace Runtime.InGame.Board
{
    [RequireComponent(typeof(BoardCellCreator))]   
    public class BoardLineEntity : MonoBehaviour
    {
        private BoardCellCreator _cellCreator;

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
    }
}