using System;
using System.Collections.Generic;
using System.Linq;
using EasyButtons;
using Runtime.InGame.Board;
using UnityEngine;

namespace Runtime.InGame.Keyboard
{
    public class Keyboard : MonoBehaviour
    {
        private List<KeyboardKey> _keys;

        private void Awake()
        {
            _keys = GetComponentsInChildren<KeyboardKey>().ToList();

            GameManager.OnLoadGame += SetupKeyboard;
        }

        [Button]
        private void SetupKeyboard()
        {
            var size = BoardManager.BoardContainerSize;
            foreach (var key in _keys)
            {
                if (key.Letter is KeyCode.Return or KeyCode.Backspace) continue;
                key.SetupKey(size.x, size.y);
            }
        }
    }
}