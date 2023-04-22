using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PlatformGroupType
{
    Grass,
    Winter
}

public class PlatformSpawner : MonoBehaviour
{
    public Vector3 startSpawnPos; // 默认初始生成位置

    // 生成平台数量
    private int spawnPlatformCount;
    private ManagerVars _vars;
    private Vector3 platformSpawnPosition; // 平台的生成位置
    private bool isLeftSpawn; // 是否朝左边生成，反之朝右
    private Sprite selectPlatformSprite; //选择的平台图
    // 组合平台的类型
    private PlatformGroupType _groupType;
    
    private void Awake()
    {
        EventCenter.AddListener(EventDefine.DecidePath,DecidePath);
        _vars = ManagerVars.GetManagerVars();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.DecidePath,DecidePath);
        
    }

    private void Start()
    {
        RandomPlatformTheme();
        platformSpawnPosition = startSpawnPos; // 第一个平台的生成位置
        
        for (int i = 0; i < 5; i++)
        {
            spawnPlatformCount = 5;
            DecidePath();
        }
        // 生成人物
        GameObject go = Instantiate(_vars.characterPrefab);
        go.transform.position = new Vector3(0,-1.8f,0);
    }
    // 随机平台主题
    private void RandomPlatformTheme()
    {
        int ran = Random.Range(0, _vars.platformThemeSpriteList.Count);
        selectPlatformSprite = _vars.platformThemeSpriteList[ran];
        if (ran==2)
        {
            _groupType = PlatformGroupType.Winter;
        }
        else
        {
            _groupType = PlatformGroupType.Grass;
        }
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
        // 生成单个平台
        if (spawnPlatformCount >= 1)
        {
            SpawnNormalPlatform();
        }
        // 生成组合平台
        else if (spawnPlatformCount==0)
        {
            int ran = Random.Range(0, 3);
            // 生成通用组合平台
            if (ran==0)
            {
                SpawnCommOnPlatformGroup();
            }
            // 生成主题组合平台
            else if (ran==1)
            {
                switch (_groupType)
                {
                    case PlatformGroupType.Grass:
                        SpawnGrassPlatformGroup();
                        break; 
                    case PlatformGroupType.Winter:
                        SpawnWinterPlatformGroup();
                        break;
                }
            }
            // 生成钉子组合平台
            else
            {
                int value = -1;
                if (isLeftSpawn)
                {
                    value = 0; // 生成右边方向的钉子
                }
                else
                {
                    value = 1; // 生成左边方向的钉子
                }
                SpawnSpikePlatform(value);
            }
        }

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
        GameObject go = Instantiate(_vars.normalPlatformPrefab,transform);
        go.transform.position = platformSpawnPosition;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite);
    }
    
    // 生成通用组合平台
    private void SpawnCommOnPlatformGroup()
    {
        int ran = Random.Range(0, _vars.commonPlatformGroup.Count);
        GameObject go = Instantiate(_vars.commonPlatformGroup[ran],transform);
        go.transform.position = platformSpawnPosition;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite);
    }
    
    // 生成草地组合平台
    private void SpawnGrassPlatformGroup()
    {
        int ran = Random.Range(0, _vars.grassPlatformGroup.Count);
        GameObject go = Instantiate(_vars.grassPlatformGroup[ran],transform);
        go.transform.position = platformSpawnPosition;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite);
    } 
    // 生成冬季组合平台
    private void SpawnWinterPlatformGroup()
    {
        int ran = Random.Range(0, _vars.winterPlatformGroup.Count);
        GameObject go = Instantiate(_vars.winterPlatformGroup[ran],transform);
        go.transform.position = platformSpawnPosition;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite);
    }
    // 生成钉子组合平台
    private void SpawnSpikePlatform(int direction)
    {
        GameObject temp = null;
        if (direction == 0)
        {
            temp = Instantiate(_vars.spikePlatformRight, transform);
        }
        else
        {
            temp = Instantiate(_vars.spikePlatformLeft, transform);
        }
        temp.transform.position = platformSpawnPosition;
        temp.GetComponent<PlatformScript>().Init(selectPlatformSprite);
    }
}
