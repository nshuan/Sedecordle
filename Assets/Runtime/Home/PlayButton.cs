using System;
using System.Collections;
using Core.PopUp;
using DG.Tweening;
using EasyButtons;
using Runtime.Const;
using Runtime.DarkMode;
using Runtime.Home.SelectHardness;
using Runtime.InGame;
using UnityEngine;
using UnityEngine.EventSystems;
using Runtime.LoadingEffect;
using Runtime.Vocabulary;
using UnityEngine.UI;

namespace Runtime.Home
{
    public class PlayButton : MonoBehaviour, IPointerClickHandler, IAffectedByDarkMode
    {
        [SerializeField] private Graphic buttonGraphic;

        public void OnPointerClick(PointerEventData eventData)
        {
            var popup = PopUp.Get<HardnessSelector>();
            StartCoroutine(IEPlay(popup));
        }

        private IEnumerator IEPlay(HardnessSelector suspender)
        {
            PopUp.Show(suspender);
            
            yield return new WaitUntil(() => suspender.Saved);
            
            Loading.Show(2f);
            Navigator.LoadScene("InGame");
        }

        public Tween DoChangeColorMode(ColorConst colorPalette)
        {
            return buttonGraphic.DOColor(colorPalette.iconColor, 0.2f).SetEase(Ease.OutQuint);
        }
    }
}