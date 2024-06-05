using UnityEngine;
using YG;

public class SaveHandler : MonoBehaviour
{
    public bool IsADS => YandexGame.savesData.IsADS;

    public bool IsSoundOn => YandexGame.savesData.IsSound;

    public void NewSaveData(SavesYG saveData)
    {
        YandexGame.savesData = saveData;
        Save();
    }

    public void Initialize()
    {
        YandexGame.LoadProgress();        
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

    public void SaveBuyNoADS()
    {
        YandexGame.savesData.IsADS = true;
        Save();
    }

    public void SaveSoundOn(bool isSoundOn) 
    {
        YandexGame.savesData.IsSound = isSoundOn;
        Save();
    
    }

    
    private void Save() => YandexGame.SaveProgress();


}
