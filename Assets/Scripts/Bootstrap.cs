
using UnityEngine;

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
    }
}
