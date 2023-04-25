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
    private Text _name;
    private Button _back;
    private Button _select;
    private Button _buy;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowShopPanel,Show);
        _parent = transform.Find("ScrollRect/Parent");
        _name = transform.Find("Name").GetComponent<Text>();
        _back = transform.Find("Back").GetComponent<Button>();
        _back.onClick.AddListener(OnBackButtonClick);
        _select = transform.Find("Select").GetComponent<Button>();
        _buy = transform.Find("Buy").GetComponent<Button>();
        _vars = ManagerVars.GetManagerVars();
       
    }

    private void Start()
    {
        Init();
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowShopPanel,Show);
        
    }
    
    private void OnBackButtonClick()
    {
        EventCenter.Broadcast(EventDefine.ShowMainPanel);
        gameObject.SetActive(false);
        
    }

    private void Init()
    {
        for (int i = 0; i < _vars.skinSpriteList.Count; i++)
        {
            _parent.GetComponent<RectTransform>().sizeDelta = new Vector2((_vars.skinSpriteList.Count + 2) * 160, 300);
            GameObject go = Instantiate(_vars.skinChooseItemPrefab, _parent);
            // 未解锁
            if (GameManager.Instance.GetSkinUnlocked(i)==false)
            {
                go.GetComponentInChildren<Image>().color = Color.gray;
               
            }
            else // 解锁了
            {
                go.GetComponentInChildren<Image>().color = Color.white;

            }
            // 获取某个游戏对象及其所有子对象中所有指定组件
            go.GetComponentInChildren<Image>().sprite = _vars.skinSpriteList[i];
            go.transform.localPosition = new Vector3((i + 1) * 160, 0, 0);
        }
    }

    private void Update()
    {
        //                          四舍五入
        int selectIndex = (int)Mathf.Round(_parent.transform.localPosition.x / -160.0f);
        if (Input.GetMouseButtonDown(0))
        {
            _parent.transform.DOLocalMoveX(selectIndex * -160, 0.2f);
            //_parent.transform.localPosition = new Vector3(currentIndex * -160, 0);
        }
        SetItemSize(selectIndex);
        RefreshUI(selectIndex);  
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

    private void RefreshUI(int selectIndex) // 刷新 
    {
        _name.text = _vars.skinNameList[selectIndex];
        if (GameManager.Instance.GetSkinUnlocked(selectIndex)==false)
        {
            _select.gameObject.SetActive(false);
            _buy.gameObject.SetActive(true);
        }
        else
        {
            _select.gameObject.SetActive(true);
            _buy.gameObject.SetActive(false);
        }
    }
}
