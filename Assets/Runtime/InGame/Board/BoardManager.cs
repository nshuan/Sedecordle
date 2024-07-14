using UnityEngine;

namespace Runtime.InGame.Board
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private Transform boardContainer;
        [SerializeField] private BoardEntity boardEntity;
        
        public BoardEntity[] BoardEntities { get; private set; }
        private const int BoardAmount = 1;
        
        public void BuildBoards(int numberOfLetter)
        {
            if (boardContainer == null) boardContainer = FindObjectOfType<Canvas>().transform;
            BoardEntities = new BoardEntity[BoardAmount];
            for (var i = 0; i < BoardAmount; i++)
            {
                var board = Instantiate(boardEntity, boardContainer);
                board.BuildBoard(numberOfLetter);
                BoardEntities[i] = board;
            }
        }

        public void SetTargets(string[] targets)
        {
            for (var i = 0; i < BoardEntities.Length; i++)
            {
                if (i >= targets.Length)
                {
                    BoardEntities[i].SetTarget("abcdef");
                    continue;
                }
                
                BoardEntities[i].SetTarget(targets[i]);
            }
        }
    }
}