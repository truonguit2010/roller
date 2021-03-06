﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obstacle spawner.
/// 
/// - Support object pool.
/// </summary>
[DisallowMultipleComponent]
public class ObstacleSpawner : MonoBehaviour {
    [SerializeField]
    private Transform reuseTrigger;

    [SerializeField]
    private GameObject[] obstaclePatterns;
    [SerializeField]
    private float spawnTime = 5f;
    [SerializeField]
    private float spawnDelay = 3f;

    [SerializeField]
    private Character character;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Camera camera;
    private Vector3 startPoint;

    private List<PoolingObstaclePattern> pools = new List<PoolingObstaclePattern>();
    private List<MoveObstaclePattern> moveObstaclePatterns = new List<MoveObstaclePattern>();

    private void Awake()
    {
        this.startPoint = camera.ScreenToWorldPoint(this.transform.position);
        startPoint = new Vector3(startPoint.x, startPoint.y, 0);
    }

    void GetOrSpawn()
    {
        int enemyIndex = Random.Range(0, obstaclePatterns.Length);
        GameObject prefab = obstaclePatterns[enemyIndex];

        PoolingObstaclePattern poolingObstacle = pools.Find(x => (x.prefab == prefab && x.isFree));
        if (poolingObstacle == null) {
            GameObject go = Instantiate<GameObject>(prefab);
            poolingObstacle = go.GetComponent<PoolingObstaclePattern>();
            poolingObstacle.prefab = prefab;
            poolingObstacle.trigger = reuseTrigger;
            poolingObstacle.forceFree();
            poolingObstacle.useMe();

            pools.Add(poolingObstacle);

            MoveObstaclePattern moveObstaclePattern = go.GetComponent<MoveObstaclePattern>();
            moveObstaclePattern.startMove();

            moveObstaclePatterns.Add(moveObstaclePattern);
        } else {
            poolingObstacle.useMe();

            poolingObstacle.GetComponent<MoveObstaclePattern>().startMove();
        }
        poolingObstacle.transform.position = startPoint;

        PatternScoreTrigger patternScoreTrigger = poolingObstacle.GetComponent<PatternScoreTrigger>();
        patternScoreTrigger.setOrReset(character.transform, gameController);
    }

    public void startGame() {
        for (int i = 0; i < pools.Count; i++) {
            pools[i].forceFree();
        }
        InvokeRepeating("GetOrSpawn", spawnDelay, spawnTime);
    }

    public void stopGame() {
        CancelInvoke("GetOrSpawn");
    }

    public void endGame() {
        CancelInvoke("GetOrSpawn");

        for (int i = 0; i < moveObstaclePatterns.Count; i++)
        {
            moveObstaclePatterns[i].endMove();
        }
    }

    public void pauseGame() {
        CancelInvoke("GetOrSpawn");
        for (int i = 0; i < moveObstaclePatterns.Count; i++)
        {
            moveObstaclePatterns[i].endMove();
        }
    }

    public void resumeGame() {
        InvokeRepeating("GetOrSpawn", spawnDelay, spawnTime);
        for (int i = 0; i < moveObstaclePatterns.Count; i++)
        {
            moveObstaclePatterns[i].startMove();
        }
    }
}
