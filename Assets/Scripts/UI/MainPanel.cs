using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    private Button _buttonStart;
    private Button _buttonShop;
    private Button _buttonRank;
    private Button _buttonSound;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _buttonStart = transform.Find("btn_Start").GetComponent<Button>();
        _buttonStart.onClick.AddListener(OnStartButtonClick);
        _buttonShop = transform.Find("Btns/btn_Shop").GetComponent<Button>();
        _buttonShop.onClick.AddListener(OnShopButtonClick);
        _buttonRank = transform.Find("Btns/btn_Rank").GetComponent<Button>();
        _buttonRank.onClick.AddListener(OnRankButtonClick);
        _buttonSound = transform.Find("Btns/btn_Sound").GetComponent<Button>();
        _buttonSound.onClick.AddListener(OnSoundButtonClick);
    }
    
    
    // 开始按钮点击后调用此方法
    private void OnStartButtonClick()
    {
                  // 广播，播放
        EventCenter.Broadcast(EventDefine.ShowGamePanel);
        gameObject.SetActive(false); // 隐藏自身
    }
    
    // 商店按钮点击
   private void OnShopButtonClick()
    {
        
    }
    
    /// 排行榜按钮点击
    private void OnRankButtonClick()
    {
       
    }
   
    // 音效按钮点击
    private void OnSoundButtonClick()
    {
        
    }
}