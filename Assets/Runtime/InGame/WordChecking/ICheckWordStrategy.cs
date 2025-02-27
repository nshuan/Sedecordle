using System.Collections.Generic;
using System.Linq;

namespace Runtime.InGame.WordChecking
{
    public interface ICheckWordStrategy
    {
        List<CharMatch> CheckWordMatch(KeyWord wordToCheck, KeyWord target);
    }

    public enum CharMatch
    {
        NotExist,
        NotPlace,
        Correct
    }

    public class SedecordleCheckStrategy : ICheckWordStrategy
    {
        public List<CharMatch> CheckWordMatch(KeyWord wordToCheck, KeyWord target)
        {
            var result = new List<CharMatch>();
            
            for (var i = 0; i < wordToCheck.Count; i++)
            {
                var charResult = CharMatch.NotExist;
                if (target[i] == wordToCheck[i]) charResult = CharMatch.Correct;
                else
                {
                    for (var j = 0; j < wordToCheck.Count; j++)
                    {
                        if (j >= target.Count) break;
                        if (j == i) continue;
                        if (target[j] == wordToCheck[i])
                        {
                            charResult = j == i ? CharMatch.Correct : CharMatch.NotPlace;
                            break;
                        }
                    }
                }
                
                result.Add(charResult);
            }

            return result;
        }
    }
}