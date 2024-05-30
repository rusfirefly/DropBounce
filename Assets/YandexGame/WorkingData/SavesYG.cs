﻿
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int Score;

        public SavesYG()
        {

        }
    }

    [System.Serializable]
    public class GameData
    {
        public string PlayerName;
        public int BestScore;
    }
}
