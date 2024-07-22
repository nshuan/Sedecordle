using System;
using System.Collections.Generic;
using Core.PopUp;
using Runtime.InGame.Board;
using Runtime.InGame.WinLose;
using UnityEngine;

namespace Runtime.InGame
{
    public class WinLoseManager : MonoBehaviour
    {
        public static event Action OnWin;
        public static event Action OnLose;
        
        public bool CheckWin(List<BoardEntity> boardEntities)
        {
            if (boardEntities is null) return false;

            var isWin = true;
            foreach (var board in boardEntities)
            {
                if (board.IsBoardComplete) continue;
                isWin = false;
                break;
            }
            
            if (isWin) DoWin();
            
            return isWin;
        }

        public void DoWin()
        {
            OnWin?.Invoke();
            PopUp.Show(PopUp.Get<WinPopUp>());
        }

        public void DoLose()
        {
            OnLose?.Invoke();
            PopUp.Show(PopUp.Get<LosePopUp>());
        }
    }
}