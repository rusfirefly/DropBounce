using UnityEngine;
using YG;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private CoinPool _coinPool;

    [SerializeField] private Spawn _spawn;

    private void Start()
    {
        _enemyPool.Initialize();
        _coinPool.Initialize();
        _spawn.Initialize();

        SavesYG gameData = YandexGame.savesData;
        if (gameData == null)
        {
            Debug.Log("new data");
            YandexGame.savesData.Score = 0;
            YandexGame.SaveProgress();
        }
        else
        {
            Debug.Log("load data");
            YandexGame.LoadProgress();
        }
        Time.timeScale = 1;
    }
}
