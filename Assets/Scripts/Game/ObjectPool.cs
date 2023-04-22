using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    public int _initSpawnCount = 5;
    private List<GameObject> _normalPlatformList = new();
    private List<GameObject> _commonPlatformList = new();
    private List<GameObject> _grassPlatformList = new();
    private List<GameObject> _winterPlatformList = new();
    private List<GameObject> _spikePlatformLeftList = new();
    private List<GameObject> _spikePlatformRightList = new();
    private ManagerVars _vars;

    private void Awake()
    {
        Instance = this;
        _vars = ManagerVars.GetManagerVars();
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < _initSpawnCount; i++)
        {
            InstantiateObject(_vars.normalPlatformPrefab, ref _normalPlatformList);
        }

        for (int i = 0; i < _initSpawnCount; i++)
        {
            for (int j = 0; j < _vars.commonPlatformGroup.Count; j++)
            {
                InstantiateObject(_vars.commonPlatformGroup[j], ref _commonPlatformList);
            }
        }

        for (int i = 0; i < _initSpawnCount; i++)
        {
            for (int j = 0; j < _vars.grassPlatformGroup.Count; j++)
            {
                InstantiateObject(_vars.grassPlatformGroup[j], ref _grassPlatformList);
            }
        }

        for (int i = 0; i < _initSpawnCount; i++)
        {
            for (int j = 0; j < _vars.winterPlatformGroup.Count; j++)
            {
                InstantiateObject(_vars.winterPlatformGroup[j], ref _winterPlatformList);
            }
        }

        for (int i = 0; i < _initSpawnCount; i++)
        {
            InstantiateObject(_vars.spikePlatformLeft, ref _spikePlatformLeftList);
        }

        for (int i = 0; i < _initSpawnCount; i++)
        {
            InstantiateObject(_vars.spikePlatformRight, ref _spikePlatformRightList);
        }
    }

    private GameObject InstantiateObject(GameObject prefab, ref List<GameObject> addList)
    {
        GameObject go = Instantiate(prefab, transform);
        go.SetActive(false);
        _commonPlatformList.Add(go);
        return go;
    }

    // 获得单个平台
    public GameObject GetNormalPlatform()
    {
        for (int i = 0; i < _normalPlatformList.Count; i++)
        {
            if (_normalPlatformList[i].activeInHierarchy == false)
            {
                return _normalPlatformList[i];
            }
        }

        return InstantiateObject(_vars.normalPlatformPrefab, ref _normalPlatformList);

    } 
    // 获得通用组合平台
    public GameObject GetCommonPlatform()
    {
        for (int i = 0; i < _commonPlatformList.Count; i++)
        {
            if (_commonPlatformList[i].activeInHierarchy == false)
            {
                return _commonPlatformList[i];
            }
        }

        int ran = Random.Range(0, _vars.commonPlatformGroup.Count);
        return InstantiateObject(_vars.commonPlatformGroup[ran], ref _commonPlatformList);

    }
    // 获得草地组合平台
    public GameObject GetGrassPlatform()
    {
        for (int i = 0; i < _grassPlatformList.Count; i++)
        {
            if (_grassPlatformList[i].activeInHierarchy == false)
            {
                return _grassPlatformList[i];
            }
        }

        int ran = Random.Range(0, _vars.grassPlatformGroup.Count);
        return InstantiateObject(_vars.grassPlatformGroup[ran], ref _grassPlatformList);

    }
    // 获得冬季组合平台
    public GameObject GetWinterPlatform()
    {
        for (int i = 0; i < _winterPlatformList.Count; i++)
        {
            if (_winterPlatformList[i].activeInHierarchy == false)
            {
                return _winterPlatformList[i];
            }
        }

        int ran = Random.Range(0, _vars.winterPlatformGroup.Count);
        return InstantiateObject(_vars.winterPlatformGroup[ran], ref _winterPlatformList);

    }
    
    // 获得左边钉子组合平台
    public GameObject GetLeftSpikePlatform()
    {
        for (int i = 0; i < _spikePlatformLeftList.Count; i++)
        {
            if (_spikePlatformLeftList[i].activeInHierarchy == false)
            {
                return _spikePlatformLeftList[i];
            }
        }

        return InstantiateObject(_vars.spikePlatformLeft, ref _spikePlatformLeftList);

    } 
    // 获得右边钉子组合平台
    public GameObject GetRightSpikePlatform()
    {
        for (int i = 0; i < _spikePlatformRightList.Count; i++)
        {
            if (_spikePlatformRightList[i].activeInHierarchy == false)
            {
                return _spikePlatformRightList[i];
            }
        }

        return InstantiateObject(_vars.spikePlatformRight, ref _spikePlatformRightList);

    } 
}
