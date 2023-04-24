using System;
using Unity.VisualScripting;
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
    // 里程碑数
    public int milestoneCount = 10;
    // 掉落时间
    public float fallTime;
    public float minFallTime;
    public float multiple; // 倍数
    // 生成平台数量
    private int spawnPlatformCount;
    private ManagerVars _vars;
    private Vector3 platformSpawnPosition; // 平台的生成位置
    private bool isLeftSpawn; // 是否朝左边生成，反之朝右

    private Sprite selectPlatformSprite; //选择的平台图

    // 组合平台的类型
    private PlatformGroupType _groupType;

    // 钉子平台组合是否生成在左边
    private bool spikeSpawnLeft;

    // 钉子方向平台的位置
    private Vector3 spikeDirectionPlatformPos;

    // 生成钉子平台之后需要在钉子方向生成的平台数量
    private int afterSpawnSpikeCount;
    private bool isSpawnSpike;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.DecidePath, DecidePath);
        _vars = ManagerVars.GetManagerVars();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.DecidePath, DecidePath);

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
        go.transform.position = new Vector3(0, -1.8f, 0);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameStart && GameManager.Instance.IsGameOver == false)
        {
            UpdateFallTime();
        }
    }

    // 更新平台掉落时间
    private void UpdateFallTime()
    {
        if (GameManager.Instance.GetGameScore() > milestoneCount)
        {
            milestoneCount *= 2;
            fallTime *= multiple;
            if (fallTime < minFallTime)
            {
                fallTime = minFallTime;
            }
        }
    }
    // 随机平台主题
    private void RandomPlatformTheme()
    {
        int ran = Random.Range(0, _vars.platformThemeSpriteList.Count);
        selectPlatformSprite = _vars.platformThemeSpriteList[ran];
        if (ran == 2)
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
        if (isSpawnSpike)
        {
            AfterSpawnSpike();
            return;
        }

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
        int ranObstacleDirection = Random.Range(0, 2);
        // 生成单个平台
        if (spawnPlatformCount >= 1)
        {
            SpawnNormalPlatform(ranObstacleDirection);
        }
        // 生成组合平台
        else if (spawnPlatformCount == 0)
        {
            int ran = Random.Range(0, 3);
            // 生成通用组合平台
            if (ran == 0)
            {
                SpawnCommOnPlatformGroup(ranObstacleDirection);
            }
            // 生成主题组合平台
            else if (ran == 1)
            {
                switch (_groupType)
                {
                    case PlatformGroupType.Grass:
                        SpawnGrassPlatformGroup(ranObstacleDirection);
                        break;
                    case PlatformGroupType.Winter:
                        SpawnWinterPlatformGroup(ranObstacleDirection);
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

                isSpawnSpike = true;
                afterSpawnSpikeCount = 4;
                if (spikeSpawnLeft) // 钉子在左边
                {
                    spikeDirectionPlatformPos = new Vector3(platformSpawnPosition.x - 1.65f,
                        platformSpawnPosition.y + _vars.nextYPos, 0);
                }
                else
                {
                    spikeDirectionPlatformPos = new Vector3(platformSpawnPosition.x + 1.65f,
                        platformSpawnPosition.y + _vars.nextYPos, 0);
                }
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
    private void SpawnNormalPlatform(int ranObstacleDirection)
    {
        GameObject go = ObjectPool.Instance.GetNormalPlatform();
        go.transform.position = platformSpawnPosition;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, fallTime,ranObstacleDirection);
        go.SetActive(true);
    }

    // 生成通用组合平台
    private void SpawnCommOnPlatformGroup(int ranObstacleDirection)
    {

        GameObject go = ObjectPool.Instance.GetCommonPlatform();
        go.transform.position = platformSpawnPosition;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, fallTime, ranObstacleDirection);
        go.SetActive(true);
    }

    // 生成草地组合平台
    private void SpawnGrassPlatformGroup(int ranObstacleDirection)
    {
        GameObject go = ObjectPool.Instance.GetGrassPlatform();
        go.transform.position = platformSpawnPosition;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, fallTime, ranObstacleDirection);
        go.SetActive(true);
    }

    // 生成冬季组合平台
    private void SpawnWinterPlatformGroup(int ranObstacleDirection)
    {
        GameObject go = ObjectPool.Instance.GetWinterPlatform();
        go.transform.position = platformSpawnPosition;
        go.GetComponent<PlatformScript>().Init(selectPlatformSprite, fallTime, ranObstacleDirection);
        go.SetActive(true);
    }

    // 生成钉子组合平台
    private void SpawnSpikePlatform(int direction)
    {
        GameObject temp = null;
        if (direction == 0)
        {
            spikeSpawnLeft = false;
            temp = ObjectPool.Instance.GetRightSpikePlatform();
        }
        else
        {
            spikeSpawnLeft = true;
            temp = ObjectPool.Instance.GetLeftSpikePlatform();
        }

        temp.transform.position = platformSpawnPosition;
        temp.GetComponent<PlatformScript>().Init(selectPlatformSprite,  fallTime,direction);
        temp.SetActive(true);
    }

    // 生成钉子平台之后需要生成的平台
    // 包括钉子方向，也包括原来的方向
    private void AfterSpawnSpike()
    {
        if (afterSpawnSpikeCount > 0)
        {
            afterSpawnSpikeCount--;
            for (int i = 0; i < 2; i++)
            {
                GameObject temp = ObjectPool.Instance.GetNormalPlatform();
                if (i == 0) // 生成原来方向的平台
                {
                    temp.transform.position = platformSpawnPosition;
                    // 如果钉子在左边,原先路径就是右边
                    if (spikeSpawnLeft)
                    {
                        platformSpawnPosition = new Vector3(platformSpawnPosition.x + _vars.nextXPos,
                            platformSpawnPosition.y + _vars.nextYPos, 0);
                    }
                    else
                    {
                        platformSpawnPosition = new Vector3(platformSpawnPosition.x - _vars.nextXPos,
                            platformSpawnPosition.y + _vars.nextYPos, 0);
                    }
                }
                else //  生成钉子方向的平台
                {
                    temp.transform.position = spikeDirectionPlatformPos;
                    if (spikeSpawnLeft)
                    {
                        spikeDirectionPlatformPos = new Vector3(spikeDirectionPlatformPos.x - _vars.nextXPos,
                            spikeDirectionPlatformPos.y + _vars.nextYPos, 0);
                    }
                    else
                    {
                        spikeDirectionPlatformPos = new Vector3(spikeDirectionPlatformPos.x + _vars.nextXPos,
                            spikeDirectionPlatformPos.y + _vars.nextYPos, 0);
                    }
                }

                temp.GetComponent<PlatformScript>().Init(selectPlatformSprite, fallTime, 1);
                temp.SetActive(true);
            }
        }
        else
        {
            isSpawnSpike = false;
            DecidePath();
        }
    }
}
