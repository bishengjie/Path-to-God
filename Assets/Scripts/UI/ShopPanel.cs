using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

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
            go.GetComponentInChildren<Image>().sprite = _vars.skinSpriteList[i];
            go.transform.localPosition = new Vector3((i + 1) * 160, 0, 0);
        }
    }
}
