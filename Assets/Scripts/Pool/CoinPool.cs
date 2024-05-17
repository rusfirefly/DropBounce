using UnityEngine;

public class CoinPool : PoolObject
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private int _count;

    protected override void InitializePool()
    {
        _objectPrefab = _coinPrefab.gameObject;

        for (int i = 0; i < _count; i++)
        {
            GameObject gameObject = Instantiate(_coinPrefab.gameObject, transform);
            gameObject.SetActive(false);
            _pool.Add(gameObject);
        }
    }
}
