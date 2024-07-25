using System.Collections.Generic;

namespace Runtime.InGame.WordService
{
    public interface IWordService
    {
        List<string> GetRandomWords(int length, int amount);
    }
}