using System;
using System.Collections.Generic;
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
        private const int BoardAmount = 16;
        
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
            while (boardToInit > 0)
            {
                var boardGroup = Instantiate(boardGroupEntity, scroller.content);
                var newBoards = boardGroup.BuildBoards(Math.Min(boardToInit, BoardGroupEntity.MaxBoard), numberOfLetter);
                var boardIndexFrom = BoardAmount - boardToInit + 1;
                var boardIndexTo = Math.Min(boardIndexFrom + BoardGroupEntity.MaxBoard - 1, BoardAmount);
                boardGroup.SetTitle("Boards #" + boardIndexFrom + "-" + boardIndexTo);
                foreach (var board in newBoards)
                {
                    BoardEntities.Add(board);
                }

                boardToInit -= BoardGroupEntity.MaxBoard;
            }
        }

        public void SetTargets(string[] targets)
        {
            for (var i = 0; i < BoardEntities.Count; i++)
            {
                if (i >= targets.Length)
                {
                    BoardEntities[i].SetTarget("ABCDEF");
                    continue;
                }
                
                BoardEntities[i].SetTarget(targets[i]);
            }
        }
    }
}