using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopPanel : MonoBehaviour
{

    private ManagerVars _vars;
    private Transform _parent;

    private void Awake()
    {
        _parent = transform.Find("ScrollRect/Parent");
        _vars = ManagerVars.GetManagerVars();
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < _vars.skinSpriteList.Count; i++)
        {
            _parent.GetComponent<RectTransform>().sizeDelta = new Vector2((_vars.skinSpriteList.Count + 2) * 160, 300);
            GameObject go = Instantiate(_vars.skinChooseItemPrefab, _parent);
            // 获取某个游戏对象及其所有子对象中所有指定组件
            go.GetComponentInChildren<Image>().sprite = _vars.skinSpriteList[i];
            go.transform.localPosition = new Vector3((i + 1) * 160, 0, 0);
        }
    }

    private void Update()
    {
        //                          四舍五入
        int currentIndex = (int)Mathf.Round(_parent.transform.localPosition.x / -160.0f);
        if (Input.GetMouseButtonDown(0))
        {
            _parent.transform.DOLocalMoveX(currentIndex * -160, 0.2f);
            //_parent.transform.localPosition = new Vector3(currentIndex * -160, 0);
        }
        SetItemSize(currentIndex);
        print(currentIndex);
    }

    private void SetItemSize(int index)
    {
        for (int i = 0; i < _parent.childCount; i++)
        {
            if (index == i)
            {
                _parent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
            }
            else
            {
                _parent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            }
        }
    }
}
