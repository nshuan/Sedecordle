using System;
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

        private BoardLineEntity[] _lineEntities;
        private int _currentLine = 0;
        
        public KeyWord Target { get; set; }
        
        private void Awake()
        {
            _lineCreator = GetComponent<BoardLineCreator>();
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
            var result = WordChecker.Instance.CheckWord(word, Target);

            var line = _lineEntities[_currentLine];
            for (var i = 0; i < line.CellEntities.Length; i++)
            {
                line.CellEntities[i].CellImage.color = result[i].GetMatchColor();
            }
            
            _currentLine += 1;
            _lineEntities[_currentLine].LineColor = ColorConst.Default.activeLineColor;
        }
    }
}