using System;
using System.Linq;
using Core.PopUp;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.InGame.WinLose
{
    public class WinPopUp : MonoBehaviour, IPopUp
    {
        [SerializeField] private Text reviewWords;

        private void OnEnable()
        {
            ShowTargetWords();
        }

        private void ShowTargetWords()
        {
            var result = string.Join(", ", GameManager.Instance.Targets);

            reviewWords.text = result;
        }
        
    }
}