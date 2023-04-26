using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RankPanel : MonoBehaviour
{
    private Button _close;
    private GameObject _scoreList;
    public Text[] score;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowRankPanel, Show);
        _close = transform.Find("Close").GetComponent<Button>();
       _close.onClick.AddListener(OnCloseButtonClick);
        _scoreList = transform.Find("ScoreList").gameObject;
        
        _close.GetComponent<Image>().color = new Color(_close.GetComponent<Image>().color.r, _close.GetComponent<Image>().color.g, _close.GetComponent<Image>().color.b, 0);
        _scoreList.transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);

    }
    
    
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowRankPanel, Show);
    }
    private void Show()
    {
        gameObject.SetActive(true);
        _close.GetComponent<Image>().DOColor(new Color(_close.GetComponent<Image>().
                color.r, _close.GetComponent<Image>().color.g,
            _close.GetComponent<Image>().color.b, 0.3f), 0.3f);
        _scoreList.transform.DOScale(Vector3.one, 0.3f);

    }
    
    private void OnCloseButtonClick()
    {
        _close.GetComponent<Image>().DOColor(new Color(_close.GetComponent<Image>().
                color.r, _close.GetComponent<Image>().color.g,
            _close.GetComponent<Image>().color.b, 0), 0.3f);
        _scoreList.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
