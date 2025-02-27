using DG.Tweening;
using Runtime.Const;
using Runtime.DarkMode;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.Board
{
    public class BoardGroupEntity : MonoBehaviour, IAffectedByDarkMode
    {
        [SerializeField] private BoardEntity boardEntity;
        [SerializeField] private Transform content;
        [SerializeField] private Text title;
        
        public BoardEntity[] BoardEntities { get; private set; }

        public const int MaxBoard = 2;

        public BoardEntity[] BuildBoards(int numberOfBoard, int numberOfLetter)
        { 
            if (numberOfBoard > MaxBoard) numberOfBoard = MaxBoard;
            
            foreach (Transform board in content)
            {
               Destroy(board.gameObject);
            }

            BoardEntities = new BoardEntity[numberOfBoard];
            for (var i = 0; i < numberOfBoard; i++)
            {
               var board = Instantiate(boardEntity, content);
               board.BuildBoard(numberOfLetter);
               BoardEntities[i] = board;
            }

            return BoardEntities;
        }

        public void SetTitle(string value)
        {
            title.text = value;
        }

        public Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return title.DOColor(ColorConst.Default.activeTextColor, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}