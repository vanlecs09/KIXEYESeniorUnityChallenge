using UnityEngine;
using System.Collections.Generic;

public class ObstacleSystem
{
    ObstacleSpawner _obstacleSpawner;
    List<GameObject> _listObstacle;
    List<GameObject> _listObstacleNotPass;
    GameObject _obstaclePrefab;
    GameObject _obstacleSpawnerPrefab;
    Transform _playerTrans;
    Vector3 _lastObstaclePosition;
    Vector3 _minPos;

    float maxRandomDistance = 10;
    float minRandomDistance = 5;

    float _circleRadius;
    public Transform PlayerTrans
    {
        get
        {
            return _playerTrans;
        }

        set
        {
            _playerTrans = value;
        }
    }
    public void Init()
    {
        _obstacleSpawnerPrefab = ResourceFactory.Load<GameObject>("Obstacle/pref_ObstacleSpawner");
        _obstacleSpawner = GameObject.Instantiate(_obstacleSpawnerPrefab).GetComponent<ObstacleSpawner>();

        _obstaclePrefab = ResourceFactory.Load<GameObject>("obstacle/pref_Obstacle");
        PrefabPoolSystem.Prespawn(_obstaclePrefab, 20);

        _listObstacle = new List<GameObject>();
        _listObstacleNotPass = new List<GameObject>();
        UGame.EventManager.StartListening(EventNames.SPAWN_OBSTACLE, SpawnObstacle);

        _minPos = GameObject.Find("Floor").GetComponent<BoxCollider2D>().bounds.max;
        _circleRadius = _obstaclePrefab.GetComponent<BoxCollider2D>().bounds.size.y;
    }

    public void Update(float delta)
    {
        for (int i = _listObstacle.Count - 1; i >= 0; i--)
        {

            var obs = _listObstacle[i];
            var isObstacleOutOfScreen = (_playerTrans.position.x - obs.transform.position.x) > 20;
            if (isObstacleOutOfScreen)
            {
                _listObstacle.RemoveAt(i);
                PrefabPoolSystem.Despawned(obs);
            }
        }

        for (int i = _listObstacleNotPass.Count - 1; i >= 0; i--)
        {
            var obs = _listObstacleNotPass[i];
            var isPlayerPassThisObstacle = _playerTrans.position.x > obs.transform.position.x;
            if (isPlayerPassThisObstacle)
            {
                _listObstacleNotPass.RemoveAt(i);
                UGame.EventManager.TriggerEvent(EventNames.PLAYER_PASS_OBSTACLE);
            }
        }

    }

    public void Shutdown()
    {
        GameObject.Destroy(_obstacleSpawner.gameObject);
        PrefabPoolSystem.Release(_obstaclePrefab);
        UGame.EventManager.StopListening(EventNames.SPAWN_OBSTACLE, SpawnObstacle);
    }

    void SpawnObstacle(System.Object obj = null)
    {
        var obstacle = PrefabPoolSystem.Spawn(_obstaclePrefab);
       
        obstacle.transform.position = this.GetNextPosition();
        _lastObstaclePosition = obstacle.transform.position;

        _listObstacle.Add(obstacle);
        _listObstacleNotPass.Add(obstacle);
    }

    Vector3 GetNextPosition()
    {
        var result = Vector3.zero;
        if(_listObstacle.Count == 0)
        {
            _lastObstaclePosition = _playerTrans.position;
        }
        var nextDistanceX = Random.Range(Gamedefine.MIN_DISTANCE_RANDOM, Gamedefine.MAX_DISTANCE_RANDOM);
        var nextDistanceY = Random.Range(0, Gamedefine.MAX_OBSTACLE_TALL);
        result = _lastObstaclePosition + new Vector3(nextDistanceX, 0, 0);
        result = new Vector3(result.x, _minPos.y + _circleRadius * 5 + nextDistanceY, result.z);
        return result;
    }
}