using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        var second = Random.Range(1, 10);
        yield return new WaitForSeconds(2);
        UGame.EventManager.TriggerEvent(EventNames.SPAWN_OBSTACLE);
        yield return StartCoroutine(SpawnEnemy());
    }
}