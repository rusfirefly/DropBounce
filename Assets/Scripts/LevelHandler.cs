using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelHandler : MonoBehaviour
{
    public static event Action NewGame;
    [SerializeField] private HudHandler _hudHandler;
    [SerializeField] private LeaderboardYG _leaderBoard;

    private void Awake()
    {
        YandexGame.LoadProgress();
    }

    private void Start()
    {
        SavesYG data = YandexGame.savesData;
        if(data != null)
            _hudHandler.SetCurrentScore(data.Score);

        YandexGame.StickyAdActivity(true);
    }

    public void MusicOff()
    {

    }

    public void RateGame()
    {
        Debug.Log("окно оценки игры");
        YandexGame.ReviewShow(true);
    }

    public void ShowLiaderBord()
    {

    }

    public void DisableABS()
    {

    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(0);
        //NewGame?.Invoke();
        //_hudHandler.SetVisibleGameOver(false);
    }

    public void ResetSave()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }

    public void UpdateLB()
    {
        _leaderBoard.UpdateLB();
    }
}
