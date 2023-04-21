using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    private Button _buttonPause;
    private Button _buttonPlay;
    private Text _score;
    private Text _diamondCount;

    private void Awake()
    {
        // 监听
        EventCenter.AddListener(EventDefine.ShowGamePanel,Show);
        Init();
    }
    private void Init()
    {
        // 暂停
        _buttonPause = transform.Find("Pause").GetComponent<Button>();
        _buttonPause.onClick.AddListener(OnPauseButtonClick);
        // 开始
        _buttonPlay = transform.Find("Play").GetComponent<Button>();
        _buttonPlay.onClick.AddListener(OnPlayButtonClick);
        
        _score = transform.Find("Score").GetComponent<Text>();
        _diamondCount = transform.Find("Diamond/DiamondCount").GetComponent<Text>();
        _buttonPlay.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // 移除
        EventCenter.RemoveListener(EventDefine.ShowGamePanel,Show);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    // 暂停按钮点击
    private void OnPauseButtonClick()
    {
        _buttonPlay.gameObject.SetActive(true);
        _buttonPause.gameObject.SetActive(false);
        // 游戏暂停
        
    }
    // 开始按钮点击
    private void OnPlayButtonClick()
    {
        _buttonPlay.gameObject.SetActive(false);
        _buttonPause.gameObject.SetActive(true);
        // 游戏继续

    }

}
