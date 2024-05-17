using System.Collections.Generic;
using UnityEngine;

public class ShipPool : PoolObject
{
    public int CounShip => _pool.Count;
    [SerializeField] private GameObject[] _spaceShipsView;

    protected override void InitializePool()
    {
        foreach(GameObject spaceShip in _spaceShipsView)
        {
            GameObject shipPool = Instantiate(spaceShip, transform);
            shipPool.name = spaceShip.name;
            shipPool.SetActive(false);
            _pool.Add(shipPool);
        }
    }

    public GameObject GetSpaceShipFromPool(int index)
    {
        if(!_pool[index].activeInHierarchy)
        {
            _pool[index].transform.position = transform.position;
            _pool[index].transform.rotation = Quaternion.identity;
            _pool[index].SetActive(true);
            return _pool[index];
        }

        return null;
    }

    public string GetNameShipFromPool(string name)
    {
       return _pool.Find(ship=>ship.name == name).name;
    }

    public GameObject GetSpaceShipFromPool(string nameShip, out int indexOnPool)
    {
        GameObject spaceShip = _pool.Find(ships => ships.name == nameShip);
        if (spaceShip)
        {
            int index = _pool.IndexOf(spaceShip);
            _pool[index].transform.position = transform.position;
            _pool[index].transform.rotation = Quaternion.identity;
            _pool[index].SetActive(true);
            indexOnPool = index;
            return _pool[index];
        }
        indexOnPool = 0;
        return null;
    }
}
