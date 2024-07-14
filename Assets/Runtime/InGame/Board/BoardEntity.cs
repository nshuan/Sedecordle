using System;
using UnityEngine;
using UnityEngine.UI;
using EasyButtons;

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
            // Todo Check word

            _currentLine += 1;
        }
    }
}