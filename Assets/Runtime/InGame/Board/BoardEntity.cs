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
        
        private void Awake()
        {
            _lineCreator = GetComponent<BoardLineCreator>();
        }

        private void Start()
        {
            _lineEntities = _lineCreator.CreateLines(17);
        }

        private void OnEnable()
        {
            InputManager.OnLetterPressed += OnLetterPressed;
        }

        private void OnDisable()
        {
            InputManager.OnLetterPressed -= OnLetterPressed;
        }

        private void OnLetterPressed(KeyCode key)
        {
            if (_lineEntities == null) return;
            if (_currentLine >= _lineEntities.Length) return;
            
            _lineEntities[_currentLine].Write(key);
        }
    }
}