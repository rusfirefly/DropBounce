using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelHandler : MonoBehaviour
{
    //public static event Action NewGame;
    [SerializeField] private HudHandler _hudHandler;
    [SerializeField] private LeaderboardYG _leaderBoard;
    [SerializeField] private PurchaseYG _purchaseYG; 

    private void Start()
    {
        SavesYG data = YandexGame.savesData;
        if(data != null)
            _hudHandler.SetCurrentScore(data.Score);

    }

    public void RateGame()
    {
        if (YandexGame.auth == false)
        {
            _hudHandler.ShowAuthWindow();
        }
        else
        {
            YandexGame.ReviewShow(true);
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateLB()
    {
        _leaderBoard.UpdateLB();
    }

    public void BuyRemoveADS()
    {
        if (YandexGame.auth == false)
        {
            _hudHandler.ShowAuthWindow();
        }
        else
        {
            _purchaseYG.BuyPurchase();
        }
    }

}
