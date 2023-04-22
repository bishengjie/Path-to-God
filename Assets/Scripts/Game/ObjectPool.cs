using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private int initSpawnCount = 5;
    private List<GameObject> _normalPlatformList = new();
    private List<GameObject> _commonPlatformList = new();
    private List<GameObject> _grassPlatformList = new();
    private List<GameObject> _winterPlatformList = new();
    private List<GameObject> _spikePlatformLeftList = new();
    private List<GameObject> _spikePlatformRightList = new();
    private ManagerVars _vars;

    private void Awake()
    {
        _vars = ManagerVars.GetManagerVars();
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < initSpawnCount; i++)
        {
            InstantiateObject(_vars.normalPlatformPrefab, ref _normalPlatformList);
        }

        for (int i = 0; i < initSpawnCount; i++)
        {
            for (int j = 0; j < _vars.commonPlatformGroup.Count; j++)
            {
                InstantiateObject(_vars.commonPlatformGroup[j], ref _commonPlatformList);
            }
        }

        for (int i = 0; i < initSpawnCount; i++)
        {
            for (int j = 0; j < _vars.grassPlatformGroup.Count; j++)
            {
                InstantiateObject(_vars.grassPlatformGroup[j], ref _grassPlatformList);
            }
        }

        for (int i = 0; i < initSpawnCount; i++)
        {
            for (int j = 0; j < _vars.winterPlatformGroup.Count; j++)
            {
                InstantiateObject(_vars.winterPlatformGroup[j], ref _winterPlatformList);
            }
        }

        for (int i = 0; i < initSpawnCount; i++)
        {
            InstantiateObject(_vars.spikePlatformLeft, ref _spikePlatformLeftList);
        }

        for (int i = 0; i < initSpawnCount; i++)
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
}
