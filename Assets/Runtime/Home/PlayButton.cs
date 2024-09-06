using System.Collections;
using Core.PopUp;
using DG.Tweening;
using EasyButtons;
using Runtime.Home.SelectHardness;
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
            var popup = PopUp.Get<HardnessSelector>();
            StartCoroutine(IEPlay(popup));
        }

        private IEnumerator IEPlay(HardnessSelector suspender)
        {
            PopUp.Show(suspender);
            
            yield return new WaitUntil(() => suspender.Saved);

            PopUp.Hide(suspender);
            Loading.Show(2f);
            Navigator.LoadScene("InGame");
        }
    }
}