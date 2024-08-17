using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Runtime.InGame.Board
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private ScrollRect scroller;
        [SerializeField] private BoardGroupEntity boardGroupEntity;
        
        public List<BoardEntity> BoardEntities { get; private set; }
        public static int BoardAmount => GameManager.NumberOfWords;
        public static Vector2Int BoardContainerSize = new Vector2Int(2, 8);
        
        public void BuildBoards(int numberOfLetter)
        {
            if (scroller == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Missing scroller!");
#endif
                return;
            }
            
            foreach (Transform boardGroup in scroller.content.transform)
            {
                Destroy(boardGroup.gameObject);
            }
            
            BoardEntities = new List<BoardEntity>();
            var boardToInit = BoardAmount;
            var boardIndex = 0;
            while (boardToInit > 0)
            {
                var boardGroup = Instantiate(boardGroupEntity, scroller.content);
                var newBoards = boardGroup.BuildBoards(Math.Min(boardToInit, BoardGroupEntity.MaxBoard), numberOfLetter);
                var boardIndexFrom = BoardAmount - boardToInit + 1;
                var boardIndexTo = Math.Min(boardIndexFrom + BoardGroupEntity.MaxBoard - 1, BoardAmount);
                boardGroup.SetTitle("Boards #" + boardIndexFrom + "-" + boardIndexTo);
                foreach (var board in newBoards)
                {
                    board.BoardId = boardIndex;
                    boardIndex += 1;
                    BoardEntities.Add(board);
                }

                boardToInit -= BoardGroupEntity.MaxBoard;
            }
        }

        public void SetTargets(List<string> targets)
        {
            for (var i = 0; i < BoardEntities.Count; i++)
            {
                if (i >= targets.Count)
                {
                    BoardEntities[i].SetTarget("ABCDEF");
                    continue;
                }
                
                BoardEntities[i].SetTarget(targets[i]);
            }
        }

        public void ScrollToBoard(int boardIndex)
        {
            var currentNormalizeX = scroller.verticalNormalizedPosition;
            
            var groupIndex = boardIndex / 2;
            var groupCount = (BoardEntities.Count + 1) / 2;
            var groupNormalizeX = (float)(groupCount - groupIndex) / groupCount;
            var duration = Mathf.Abs(groupNormalizeX - currentNormalizeX) * 0.6f;
            scroller.DOVerticalNormalizedPos(groupNormalizeX, duration).SetEase(Ease.Linear);
        }
    }
}