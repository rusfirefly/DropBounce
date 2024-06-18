using System;
using UnityEngine;
using YG;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private CoinPool _coinPool;

    [SerializeField] private Spawn _spawn;
    [SerializeField] private Sound _sound;
    [SerializeField] private SaveHandler _saveHandler;
    [SerializeField] private ADS _ads;
    [SerializeField] private HudHandler _hudHandler;
    [SerializeField] private Tutorial _tutorial;

    private void Start()
    {
        _enemyPool.Initialize();
        _coinPool.Initialize();
        _spawn.Initialize();

        SavesYG gameData = YandexGame.savesData;

        if (gameData == null)
        {
            /* Debug.Log("new data");
             gameData.Score = 0;
             gameData.IsSound = true;
             gameData.IsADS = false;
             gameData.IsTutorial = true;

             _saveHandler.NewSaveData(gameData);
            */
        }
        else
        {
            Debug.Log("load data");
            _saveHandler.Initialize();
        }

        _sound.Initialize(YandexGame.savesData.IsSound);
        Time.timeScale = 1;

        _ads.Initialize(_saveHandler);

        if(_saveHandler.IsADS == false)
        {
            _hudHandler.SetSoldButton();
        }

        _tutorial.Initialized(_saveHandler);

    }

    private void OnEnable()
    {
        YandexGame.PurchaseSuccessEvent += SuccessPurchased;
    }

    private void OnDisable()
    {
        YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
    }

    private void SuccessPurchased(string id)
    {
        switch(id)
        {
            case "777":
                _hudHandler.ShowSoldWindow();
                _hudHandler.SetSoldButton();
                _ads.RemoveADS();
            break;
        }
    }
}
