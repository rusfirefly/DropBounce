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

    private void Start()
    {
        _enemyPool.Initialize();
        _coinPool.Initialize();
        _spawn.Initialize();

        float soundVolume = 1;

        SavesYG gameData = YandexGame.savesData;
        if (gameData == null)
        {
            Debug.Log("new data");
            gameData.Score = 0;
            gameData.IsSound = true;
            gameData.IsADS = false;

            _saveHandler.NewSaveData(gameData);
        }
        else
        {
            Debug.Log("load data");
            _saveHandler.Initialize();
            soundVolume = YandexGame.savesData.IsSound ? soundVolume = 1 : soundVolume = 0;
        }

        _sound.Initialize(soundVolume);
        Time.timeScale = 1;
        _ads.Initialize(_saveHandler);
    }
}
