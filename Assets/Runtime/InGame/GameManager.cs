using System.Collections.Generic;
using Core.Singleton;
using EasyButtons;
using Runtime.InGame.Board;
using UnityEngine;

namespace Runtime.InGame
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public BoardManager boardManager;

        private Stack<KeyCode> _input;
        private const int NumberOfLetter = 5;
        
        protected override void Awake()
        {
            base.Awake();

            _input = new Stack<KeyCode>();
            // LoadGame(NumberOfLetter);
        }

        private void OnEnable()
        {
            InputManager.OnEnterPressed += OnEnterPressed;
            InputManager.OnBackspacePressed += OnBackspacePressed;
            InputManager.OnLetterPressed += OnLetterPressed;
        }

        private void OnDisable()
        {
            InputManager.OnEnterPressed -= OnEnterPressed;
            InputManager.OnBackspacePressed -= OnBackspacePressed;
            InputManager.OnLetterPressed -= OnLetterPressed;
        }

        private void OnLetterPressed(KeyCode key)
        {
            if (_input.Count >= NumberOfLetter)
                return;
            
            _input.Push(key);

            foreach (var board in boardManager.BoardEntities)
            {
                board.Write(key);
            }
        }

        private void OnEnterPressed()
        {
            if (_input.Count == NumberOfLetter) EndTurn();    
        }
        
        private void OnBackspacePressed()
        {
            if (_input.Count > 0)
            {
                _input.Pop();
                foreach (var board in boardManager.BoardEntities)
                {
                    board.Backspace();
                }
            }
        }
        
        [Button]
        private void LoadGame(int numberOfLetter)
        {
            boardManager.BuildBoards(numberOfLetter);
            boardManager.SetTargets(new []{ "virus" });
        }

        private void EndTurn()
        {
            
            // Check word
            var inputWord = _input.ToKeyWord();
            Debug.Log("Turn ended! Checking word: " + inputWord);
            _input.Clear();
            foreach (var board in boardManager.BoardEntities)
            {
                board.CheckWord(inputWord);
            }
        }
        
    }
}