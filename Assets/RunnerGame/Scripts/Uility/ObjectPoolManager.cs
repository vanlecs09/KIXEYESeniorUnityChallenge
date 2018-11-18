using System;
using System.Collections.Generic;
using UnityEngine;
public interface IPoolableObject
{
    void New();
    void Respawn();
}

public class ObjectPool<T> where T : IPoolableObject, new()
{
    private Stack<T> _pool;
    private int _currentIndex = 0;
    public ObjectPool(int initialCapacity)
    {
        _pool = new Stack<T>(initialCapacity);
        for (int i = 0; i < initialCapacity; ++i)
        {
            Spawn();
        }

        Reset();
    }

    public int Count
    {
        get { return _pool.Count; }
    }

    public void Reset()
    {
        _currentIndex = 0;
    }

    public T Spawn()
    {
        if (_currentIndex < Count)
        {
            T obj = _pool.Pop();
            _currentIndex++;
            IPoolableObject ip = obj as IPoolableObject;
            ip.Respawn();

            return obj;
        }
        else
        {
            T obj = new T();
            _pool.Push(obj);
            _currentIndex++;

            IPoolableObject ip = obj as IPoolableObject;
            ip.New();
            return obj;
        }
    }
}


public interface IPoolableComponent
{
    void Spawned();
    void Despawned();
}

public struct PoolablePrefabData
{
    public GameObject go;
    public IPoolableComponent[] poolableComponents;
}

public class PrefabPool
{
    Dictionary<GameObject, PoolablePrefabData> _activeList = new Dictionary<GameObject, PoolablePrefabData>();
    Queue<PoolablePrefabData> _inactiveList = new Queue<PoolablePrefabData>();

    public GameObject Spawn(GameObject prefab)
    {
        PoolablePrefabData data;
        if (_inactiveList.Count > 0)
        {
            data = _inactiveList.Dequeue();
        }
        else
        {
            GameObject newGo = GameObject.Instantiate(prefab) as GameObject;
            data = new PoolablePrefabData();
            data.go = newGo;
            data.poolableComponents = newGo.GetComponents<IPoolableComponent>();
        }

        data.go.SetActive(true);
        for (int i = 0; i < data.poolableComponents.Length; i++)
        {
            data.poolableComponents[i].Spawned();
        }

        _activeList.Add(data.go, data);
        return data.go;
    }



    public bool Despawned(GameObject objToDespawn)
    {
        if (!_activeList.ContainsKey(objToDespawn))
        {
            Debug.LogError("this Object is not managed by this object pool");
            return false;
        }

        PoolablePrefabData data = _activeList[objToDespawn];

        for (int i = 0; i < data.poolableComponents.Length; i++)
        {
            data.poolableComponents[i].Despawned();
        }

        data.go.SetActive(false);
        _activeList.Remove(objToDespawn);
        _inactiveList.Enqueue(data);
        return true;
    }

    public void ShutDown()
    {
        foreach (var item in _activeList.Keys)
        {
            GameObject.Destroy(item);
        }

        foreach (var item in _inactiveList)
        {
            GameObject.Destroy(item.go);
        }

        _inactiveList.Clear();
        _activeList.Clear();
        {

        }
    }
}

public class PrefabPoolSystem
{
    static Dictionary<GameObject, PrefabPool> _prefabToPoolMap = new Dictionary<GameObject, PrefabPool>();
    static Dictionary<GameObject, PrefabPool> _goToPoolMap = new Dictionary<GameObject, PrefabPool>();

    public static GameObject Spawn(GameObject prefab)
    {
        if (!_prefabToPoolMap.ContainsKey(prefab))
        {
            _prefabToPoolMap.Add(prefab, new PrefabPool());
        }

        PrefabPool pool = _prefabToPoolMap[prefab];
        GameObject go = pool.Spawn(prefab);
        _goToPoolMap.Add(go, pool);
        return go;
    }

    public static bool Despawned(GameObject obj)
    {
        if (!_goToPoolMap.ContainsKey(obj))
        {
            return false;
        }

        PrefabPool pool = _goToPoolMap[obj];
        if (pool.Despawned(obj))
        {
            _goToPoolMap.Remove(obj);
            return true;
        }

        return false;
    }

    public static void Prespawn(GameObject prefab, int numToSpawn)
    {
        List<GameObject> spawnedObjects = new List<GameObject>();

        for (int i = 0; i < numToSpawn; i++)
        {
            spawnedObjects.Add(Spawn(prefab));
        }

        for (int i = 0; i < numToSpawn; i++)
        {
            Despawned(spawnedObjects[i]);
        }

        spawnedObjects.Clear();
    }

    public static void Release(GameObject prefab)
    {
        if (_prefabToPoolMap.ContainsKey(prefab))
        {
            _prefabToPoolMap[prefab].ShutDown();
        }
    }

    public static void ShutDown()
    {
        foreach (var item in _prefabToPoolMap.Values)
        {
            item.ShutDown();
        }
    }
}