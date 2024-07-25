using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Runtime.Vocabulary
{
    public static class WordDictionary
    {
        private const string Path = "Assets/Resources/Data/MiniDictionary.json";
        
        private static Dictionary<string, WordData> _wordsMap;

        public static Dictionary<string, WordData> WordsMap => _wordsMap;
        
        static WordDictionary()
        {
            _wordsMap = new Dictionary<string, WordData>();
            _wordsMap = ParseDictionaryFromJson();
        }

        private static Dictionary<string, WordData> ParseDictionaryFromJson()
        {
            if (!File.Exists(Path)) return new Dictionary<string, WordData>();

            var json = File.ReadAllText(Path);
            var dataDictionary = JsonConvert.DeserializeObject<Dictionary<string, WordData>>(json);

            return dataDictionary;
        }
    }
}