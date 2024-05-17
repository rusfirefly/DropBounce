using UnityEngine;

public class EnemyPool : PoolObject
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _count;

    protected override void InitializePool()
    {
        _objectPrefab = _enemyPrefab.gameObject;

        for (int i = 0; i < _count; i++)
        {
            GameObject gameObject = Instantiate(_enemyPrefab.gameObject, transform);
            gameObject.SetActive(false);
            _pool.Add(gameObject);
        }
    }



}
