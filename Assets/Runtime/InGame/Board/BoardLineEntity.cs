using System;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;

namespace Runtime.InGame.Board
{
    [RequireComponent(typeof(BoardCellCreator))]   
    public class BoardLineEntity : MonoBehaviour
    {
        private BoardCellCreator _cellCreator;

        private BoardCellEntity[] _cellEntities;
        private int _currentCell = 0;
        
        private void Awake()
        {
            _cellCreator = GetComponent<BoardCellCreator>();
        }

        public void InitBoardLine(int cellAmount)
        {
            _cellEntities = _cellCreator.CreateCells(cellAmount, Vector2Int.zero, 5);
        }

        public void Write(KeyCode key)
        {
            if (_cellEntities == null) return;
            if (_cellEntities.Length == 0) return;
            if (_currentCell >= _cellEntities.Length) return;
            
            _cellEntities[_currentCell].Write(key);
            _currentCell += 1;
        }

        public void Backspace()
        {
            if (_cellEntities == null) return;
            if (_cellEntities.Length == 0) return;
            _currentCell -= 1;
            if (_currentCell >= _cellEntities.Length) return;

            _cellEntities[_currentCell].Delete();
        }
    }
}