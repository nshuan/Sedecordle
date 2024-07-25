using System;

namespace Runtime.Vocabulary
{
    [Serializable]
    public class WordData
    {
        public string Word { get; set; }
        public string Link { get; set; }
        public string Topic { get; set; }
        public string SubTopic { get; set; }
    }
}