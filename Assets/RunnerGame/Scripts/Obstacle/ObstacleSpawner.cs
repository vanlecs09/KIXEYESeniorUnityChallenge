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
        yield return new WaitForSeconds(0.5f);
        UGame.EventManager.TriggerEvent(EventNames.SPAWN_OBSTACLE);
        yield return StartCoroutine(SpawnEnemy());
    }
}