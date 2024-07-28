using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runtime.InGame
{
    public class KeyWord : IEnumerable<KeyCode>
    {
        private readonly string _value;
        private readonly List<KeyCode> _word;

        public KeyCode this[int index]
        {
            get => _word[index];
            set => _word[index] = value;
        }
        
        public KeyWord(string str)
        {
            _value = str.ToUpper();
            _word = new List<KeyCode>();
            foreach (var c in str)
            {
                _word.Add(c.ToKeyCode());
            }
        }

        public KeyWord(KeyCode[] keys)
        {
            _value = "";
            _word = new List<KeyCode>();
            foreach (var key in keys)
            {
                _value += key.ToString();
                _word.Add(key);
            }
        }

        public int Count => _word.Count;
        public int Length => Count;
        
        public override string ToString()
        {
            return _value;
        }
        
        public IEnumerator<KeyCode> GetEnumerator()
        {
            return _word.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _word.GetEnumerator();
        }
    }

    public static class WordExtension
    {
        public static KeyCode ToKeyCode(this char c)
        {
            // Handle uppercase letters
            if (c >= 'A' && c <= 'Z')
            {
                return (KeyCode)((int)KeyCode.A + (c - 'A'));
            }
        
            // Handle lowercase letters
            if (c >= 'a' && c <= 'z')
            {
                return (KeyCode)((int)KeyCode.A + (c - 'a'));
            }

            // If the character is not a letter, return KeyCode.None
            return KeyCode.None;
        }
        
        public static KeyWord ToKeyWord(this string str)
        {
            return new KeyWord(str);
        }

        public static KeyWord ToKeyWord(this Stack<KeyCode> keys)
        {
            var array = keys.ToArray();
            Array.Reverse(array);
            return new KeyWord(array);
        }
    }
}