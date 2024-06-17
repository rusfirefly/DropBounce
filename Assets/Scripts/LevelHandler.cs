using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LevelHandler : MonoBehaviour
{
    //public static event Action NewGame;
    [SerializeField] private HudHandler _hudHandler;
    [SerializeField] private LeaderboardYG _leaderBoard;
 
    private void Start()
    {
        SavesYG data = YandexGame.savesData;
        if(data != null)
            _hudHandler.SetCurrentScore(data.Score);
    }

    public void RateGame()
    {
        YandexGame.ReviewShow(true);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateLB()
    {
        _leaderBoard.UpdateLB();
    }


}
