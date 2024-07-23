using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.LoadingEffect
{
    public interface ILoadingEffect
    {
        Tween DoEffect();
        Tween DoHide(float duration);
    }

    [Serializable]
    public abstract class MonoLoadingEffect : MonoBehaviour, ILoadingEffect
    {
        public abstract Tween DoEffect();
        public abstract Tween DoHide(float duration);
    }
}