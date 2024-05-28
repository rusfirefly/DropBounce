using UnityEngine;
using YG;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private HudHandler _hudHandler;


    private void Awake()
    {
        YandexGame.LoadProgress();
    }

    private void Start()
    {
        _hudHandler.SetCurrentScore(YandexGame.savesData.Score);    
    }

    public void MusicOff()
    {

    }

    public void RateGame()
    {

    }

    public void ShowLiaderBord()
    {

    }

    public void DisableABS()
    {

    }
}
