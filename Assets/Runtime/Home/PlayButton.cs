using DG.Tweening;
using EasyButtons;
using Runtime.InGame;
using UnityEngine;
using UnityEngine.EventSystems;
using Runtime.LoadingEffect;
using Runtime.Vocabulary;

namespace Runtime.Home
{
    public class PlayButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Loading.Show(2f);
            Navigator.LoadScene("InGame");
        }

        [Button]
        private void LoadData()
        {
            Debug.Log(WordDictionary.WordsMap.Count);
        }
    }
}