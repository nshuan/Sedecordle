using System.Collections.Generic;
using Core.Singleton;
using DG.Tweening;
using UnityEngine;

namespace Runtime.ToastNotify
{
    public class Toast : MonoSingleton<Toast>
    {
        [SerializeField] private ToastItem toastItem;

        private Queue<ToastItem> _toastQueue;

        protected override void Awake()
        {
            base.Awake();

            var garbage = GetComponentsInChildren<ToastItem>();
            foreach (var trash in garbage)
            {
                Destroy(trash);
            }
            
            _toastQueue = new Queue<ToastItem>();
        }
        
        public void Show(string message)
        {
            if (!_toastQueue.TryDequeue(out var toast))
            {
                toast = Instantiate(toastItem, transform);
            }
            
            toast.Show(message).OnComplete(() => _toastQueue.Enqueue(toast));
        }

        public void Show(string message, Color color)
        {
            if (!_toastQueue.TryDequeue(out var toast))
            {
                toast = Instantiate(toastItem, transform);
            }
            
            toast.Show(message, color).OnComplete(() => _toastQueue.Enqueue(toast));
        }
        
        public void Show(string message, Color color, Color textColor)
        {
            if (!_toastQueue.TryDequeue(out var toast))
            {
                toast = Instantiate(toastItem, transform);
            }
            
            toast.Show(message, color, textColor).OnComplete(() => _toastQueue.Enqueue(toast));
        }
    }
}