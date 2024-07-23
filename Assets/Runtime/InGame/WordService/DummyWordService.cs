using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Runtime.InGame.WordService
{
    public class DummyWordService : IWordService
    {
        private List<string> _dict = new List<string>()
        {
            "beast",
            "elide",
            "blank",
            "bitch",
            "whale",
            "squid",
            "sprat",
            "tench",
            "trout",
            "fable",
            "paper",
            "novel",
            "flash",
            "woman",
            "worth",
            "scoop",
            "radio",
            "ultra",
            "video",
            "crisp",
            "Hunaa",
            "Hinee"
        };
            
        public List<string> GetRandomWords(int amount)
        {
            var shuffledDict = new List<string>(_dict);
            var count = shuffledDict.Count;

            for (var i = 0; i < count; i++)
            {
                var j = Random.Range(i, count);
                (shuffledDict[i], shuffledDict[j]) = (shuffledDict[j], shuffledDict[i]);
            }

            return shuffledDict.GetRange(0, amount);
        }
    }
}