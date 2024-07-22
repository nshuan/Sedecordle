using System;
using System.Collections.Generic;
using System.Linq;
using Core.Singleton;
using Runtime.Const;
using UnityEngine;

namespace Runtime.InGame.WordChecking
{
    public class WordChecker : MonoSingleton<WordChecker>
    {
        private ICheckWordStrategy _checkStrategy = new SedecordleCheckStrategy();

        public List<CharMatch> CheckWord(KeyWord wordToCheck, KeyWord target) =>
            _checkStrategy?.CheckWord(wordToCheck, target);
    }

    public static class CharMatchExtension
    {
        public static Color GetMatchColor(this CharMatch charMatch)
        {
            return charMatch switch
            {
                CharMatch.NotExist => ColorConst.Default.notExistColor,
                CharMatch.NotPlace => ColorConst.Default.notPlaceColor,
                CharMatch.Correct => ColorConst.Default.correctColor,
                _ => ColorConst.Default.notExistColor
            };
        }

        public static bool IsAllCorrect(this List<CharMatch> results)
        {
            return results.All(charMatch => charMatch != CharMatch.NotPlace && charMatch != CharMatch.NotExist);
        }
    }
}