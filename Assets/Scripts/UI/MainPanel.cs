using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    private Button btn_Start;
    private Button btn_Shop;
    private Button btn_Rank;
    private Button btn_Sound;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        btn_Start = transform.Find("btn_Start").GetComponent<Button>();
        btn_Start.onClick.AddListener(OnStartButtonClick);
        btn_Shop = transform.Find("Btns/btn_Shop").GetComponent<Button>();
        btn_Shop.onClick.AddListener(OnShopButtonClick);
        btn_Rank = transform.Find("Btns/btn_Rank").GetComponent<Button>();
        btn_Rank.onClick.AddListener(OnRankButtonClick);
        btn_Sound = transform.Find("Btns/btn_Sound").GetComponent<Button>();
        btn_Sound.onClick.AddListener(OnSoundButtonClick);
    }
    
    
    // 开始按钮点击后调用此方法
    private void OnStartButtonClick()
    {
        
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
