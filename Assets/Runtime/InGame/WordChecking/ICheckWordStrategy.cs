using System.Collections.Generic;
using System.Linq;

namespace Runtime.InGame.WordChecking
{
    public interface ICheckWordStrategy
    {
        List<CharMatch> CheckWord(KeyWord wordToCheck, KeyWord target);
    }

    public enum CharMatch
    {
        NotExist,
        NotPlace,
        Correct
    }

    public class SedecordleCheckStrategy : ICheckWordStrategy
    {
        public List<CharMatch> CheckWord(KeyWord wordToCheck, KeyWord target)
        {
            var result = new List<CharMatch>();
            
            for (var i = 0; i < wordToCheck.Count; i++)
            {
                var charResult = CharMatch.NotExist;
                for (var j = 0; j < target.Count; j++)
                {
                    if (target[j] == wordToCheck[i])
                    {
                        charResult = j == i ? CharMatch.Correct : CharMatch.NotPlace;
                        break;
                    }
                }
                
                result.Add(charResult);
            }

            return result;
        }
    }
}