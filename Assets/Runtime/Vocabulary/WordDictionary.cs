using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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

            var lowercasedKey = new Dictionary<string, WordData>();
            foreach (var word in dataDictionary)
            {
                lowercasedKey.TryAdd(word.Key.ToLower(), word.Value);
            }
            return lowercasedKey;
        }

        public static void FixDictionary()
        {
            var json = File.ReadAllText(Path);
            var dataDictionary = JsonConvert.DeserializeObject<Dictionary<string, WordData>>(json);
            
            var fixedDict = new Dictionary<string, WordData>();
            
            var pattern = @"[^\x00-\x7F]+"; // Unicode range for Emoticons
            var onlyLetterPattern = @"^[a-zA-Z]+$";
            foreach (var data in dataDictionary)
            {
                var fixedKey = Regex.Replace(data.Key, pattern, "");

                if (Regex.IsMatch(fixedKey, onlyLetterPattern))
                {
                    data.Value.Word = fixedKey;
                    fixedDict[fixedKey] = data.Value;
                }
            }

            var save = JsonConvert.SerializeObject(fixedDict, Formatting.Indented);
            File.WriteAllText("Assets/Resources/Data/FixedMiniDictionary.json", save);
        }
    }
}