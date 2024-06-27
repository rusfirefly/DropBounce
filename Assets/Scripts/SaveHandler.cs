using UnityEngine;
using YG;

public class SaveHandler : MonoBehaviour
{
    public bool IsADS => YandexGame.savesData.IsADS;

    public bool IsSoundOn => YandexGame.savesData.IsSound;

    public bool IsTutorial=>YandexGame.savesData.IsTutorial;

    public void NewSaveData(SavesYG saveData)
    {
        YandexGame.savesData = saveData;
        Save();
    }

    public void Initialize()
    {
        YandexGame.LoadProgress();
        if(YandexGame.savesData.isFirstSession)
        {
            YandexGame.savesData = new SavesYG();
            Save();
        }
    }

    public void ResetSave()
    {
        YandexGame.ResetSaveProgress();
        Save();
    }

    public void SaveScore(int newScore)
    {
        YandexGame.savesData.Score = newScore;
        Save();
    }

    public void SaveBuyNoADS(bool isAds)
    {
        YandexGame.savesData.IsADS = isAds;
        Save();
    }

    public void SaveSoundOn(bool isSoundOn) 
    {
        YandexGame.savesData.IsSound = isSoundOn;
        Save();
    
    }

    public void SetHideTutorial()
    {
        YandexGame.savesData.IsTutorial = false;
        Save();
    }

    public void Save() => YandexGame.SaveProgress();
    
    

}
