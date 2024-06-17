
using UnityEngine.Playables;

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
        public bool IsSound;
        public bool IsADS;
        public bool IsTutorial;

        public SavesYG()
        {
            Score = 0;
            IsSound = true;
            IsADS = false;
            IsTutorial = true;
        }
    }

    [System.Serializable]
    public class GameData
    {
        public string PlayerName;
        public int BestScore;
    }
}
