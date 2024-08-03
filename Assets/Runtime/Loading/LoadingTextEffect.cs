using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.LoadingEffect
{
    public class LoadingTextEffect : MonoLoadingEffect
    {
        [SerializeField] private List<Text> characters = new List<Text>();
        
        public override Tween DoEffect()
        {
            DOTween.Kill(transform);
            var sequence = DOTween.Sequence(transform)
                .AppendInterval(0.4f);

            foreach (var character in characters)
            {
                character.color += new Color(0f, 0f, 0f ,1f);
            }
            
            for (var i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                var charSeq = DOTween.Sequence();
                var charOriginalPos = character.transform.position;
                charSeq.AppendInterval((i + 1) * 0.2f)
                    .Append(character.transform.DOLocalMoveY(80f, 0.22f).SetEase(Ease.OutQuad).SetEase(Ease.InBack).SetRelative())
                    .Append(character.transform.DOLocalMoveY(charOriginalPos.y, 0.16f).SetEase(Ease.InQuad).SetEase(Ease.OutBack));
                sequence.Join(charSeq);
            }

            sequence.SetLoops(-1);

            return sequence;
        }

        public override Tween DoHide(float duration)
        {
            var sequence = DOTween.Sequence();
            
            foreach (var character in characters)
            {
                sequence.Join(character.DOFade(0f, duration));
            }

            return sequence;
        }
    }
}