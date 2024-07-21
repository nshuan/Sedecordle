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
        [SerializeField] private Transform boardContainer;
        [SerializeField] private BoardGroupEntity boardGroupEntity;
        
        public List<BoardEntity> BoardEntities { get; private set; }
        private const int BoardAmount = 4;
        
        public void BuildBoards(int numberOfLetter)
        {
            if (boardContainer == null)
            {
                boardContainer = FindObjectOfType<Canvas>().transform;
            }
            
            foreach (Transform boardGroup in boardContainer.transform)
            {
                Destroy(boardGroup.gameObject);
            }
            
            BoardEntities = new List<BoardEntity>();
            var boardToInit = BoardAmount;
            while (boardToInit > 0)
            {
                var boardGroup = Instantiate(boardGroupEntity, boardContainer);
                var newBoards = boardGroup.BuildBoards(Math.Min(boardToInit, 4), numberOfLetter);
                foreach (var board in newBoards)
                {
                    BoardEntities.Add(board);
                }

                boardToInit -= 4;
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