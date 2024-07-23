using System.Collections.Generic;
using Core.Singleton;
using DG.Tweening;
using EasyButtons;
using Runtime.InGame.Board;
using Runtime.InGame.WordService;
using UnityEngine;

namespace Runtime.InGame
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private const int NumberOfLetter = 5;
        private const int MaxTurn = 21;
        public const int NumberOfWords = 16;
        
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private WinLoseManager winLoseManager;
        private IWordService _wordService = new DummyWordService();

        public List<BoardEntity> BoardEntities => boardManager.BoardEntities;
        
        private Stack<KeyCode> _input;
        private int _currentTurn = 1;
        
        protected override void Awake()
        {
            base.Awake();

            _input = new Stack<KeyCode>();
            LoadGame(NumberOfLetter);
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
            _currentTurn = 1;
            var targetWords = _wordService.GetRandomWords(16);
            boardManager.BuildBoards(numberOfLetter);
            boardManager.SetTargets(targetWords);
        }

        private void EndTurn()
        {
            // Check word
            InputManager.BlockInput();
            var inputWord = _input.ToKeyWord();
#if UNITY_EDITOR
            Debug.Log("Turn ended! Checking word: " + inputWord);
#endif
            _input.Clear();
            DOTween.Kill(transform);
            var sequence = DOTween.Sequence(transform);
            foreach (var board in boardManager.BoardEntities)
            {
                sequence.Join(board.CheckWord(inputWord));
            }

            _currentTurn += 1;
            sequence.OnComplete(() =>
            {
                var win = winLoseManager.CheckWin(BoardEntities);
                if (win) return;

                if (_currentTurn > MaxTurn)
                {
                    winLoseManager.DoLose();
                    return;
                }
                
                InputManager.UnBlockInput();
            });
        }
    }
}