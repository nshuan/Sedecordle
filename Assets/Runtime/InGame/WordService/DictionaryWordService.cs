using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Vocabulary;

namespace Runtime.InGame.WordService
{
    public class DictionaryWordService : IWordService
    {
        private Dictionary<int, List<string>> _wordsMapByLength = new Dictionary<int, List<string>>();
        
        public List<string> GetRandomWords(int length, int amount)
        {
            _wordsMapByLength.TryAdd(length, null);

            if (_wordsMapByLength[length] == null)
            {
                _wordsMapByLength[length] = new List<string>();
                foreach (var word in WordDictionary.WordsMap.Keys.Where(word => word.Length == length))
                {
                    _wordsMapByLength[length].Add(word);
                }
            }
            
            // Get random indexes
            var randomAmount = amount;
            var maxIndex = _wordsMapByLength[length].Count;
            if (amount > maxIndex) randomAmount = maxIndex; 
            var uniqueIndexes = new HashSet<int>();
            var random = new Random();

            while (uniqueIndexes.Count < randomAmount)
            {
                var number = random.Next(0, maxIndex);
                uniqueIndexes.Add(number);
            }

            for (var i = randomAmount; i < amount; i++)
                uniqueIndexes.Add(0);

            return uniqueIndexes.Select(index => _wordsMapByLength[length][index]).ToList();
        }
    }
}