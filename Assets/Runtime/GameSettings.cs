using System;
using Core.DataHandler;
using Runtime.Const;

namespace Runtime
{
    [Serializable]
    public class GameSettings
    {
        public int hardness = 5;
        public ColorMode colorMode = ColorMode.Dark;
        
        private const string GameSettingsKey = "game_settings";
        private static GameSettings _load;

        public static GameSettings Load => _load ??= DataHandler.Load<GameSettings>(GameSettingsKey, new GameSettings());

        public static void Save()
        {
            DataHandler.Save<GameSettings>(GameSettingsKey, _load);
        }
    }
}