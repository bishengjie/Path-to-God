using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    public Vector3 startSpawnPos; // 默认初始生成位置

    // 生成平台数量
    private int spawnPlatformCount;
    private ManagerVars _vars;
    private Vector3 platformSpawnPosition; // 平台的生成位置
    private bool isLeftSpawn; // 是否朝左边生成，反之朝右

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.DecidePath,DecidePath);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.DecidePath,DecidePath);
    }

    private void Start()
    {
        platformSpawnPosition = startSpawnPos; // 第一个平台的生成位置
        _vars = ManagerVars.GetManagerVars();
        for (int i = 0; i < 5; i++)
        {
            spawnPlatformCount = 5;
            DecidePath();
        }
        // 生成人物
        GameObject go = Instantiate(_vars.characterPrefab);
        go.transform.position = new Vector3(0,-1.8f,0);
    }

    // 确定路径
    private void DecidePath()
    {
        if (spawnPlatformCount > 0)
        {
            spawnPlatformCount--;
            SpawnPlatform();
        }
        else
        {
            // 反转生成方向
            isLeftSpawn = !isLeftSpawn; // 转向 左变右 右变左
            spawnPlatformCount = Random.Range(1, 4);
            SpawnPlatform();
        }

    }

    // 生成平台
    private void SpawnPlatform()
    {
        SpawnNormalPlatform();
        if (isLeftSpawn) // 向左生成
        {
            platformSpawnPosition = new Vector3(platformSpawnPosition.x - _vars.nextXPos,
                platformSpawnPosition.y + _vars.nextYPos, 0);
        }
        else // 向右生成
        {
            platformSpawnPosition = new Vector3(platformSpawnPosition.x + _vars.nextXPos,
                platformSpawnPosition.y + _vars.nextYPos, 0);
        }
    }

    // 生成普通平台（单个）
    private void SpawnNormalPlatform()
    {
        GameObject go = Instantiate(_vars.normalPlatformPrefab);
        go.transform.position = platformSpawnPosition;
    }
}
