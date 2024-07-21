using UnityEngine;

namespace Runtime.InGame.Board
{
    public class BoardGroupEntity : MonoBehaviour
    {
        [SerializeField] private BoardEntity boardEntity;
        
        public BoardEntity[] BoardEntities { get; private set; }
        
        public static int MaxBoard = 4;
        
        public BoardEntity[] BuildBoards(int numberOfBoard, int numberOfLetter)
        { 
            if (numberOfBoard > MaxBoard) numberOfBoard = MaxBoard;
            
            foreach (Transform board in transform)
            {
               Destroy(board.gameObject);
            }

            BoardEntities = new BoardEntity[numberOfBoard];
            for (var i = 0; i < numberOfBoard; i++)
            {
               var board = Instantiate(boardEntity, transform);
               board.BuildBoard(numberOfLetter);
               BoardEntities[i] = board;
            }

            return BoardEntities;
        }
    }
}