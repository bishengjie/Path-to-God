using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    // 游戏是否开始
    public bool IsGameStart { get; set; }
    // 游戏是否结束
    public bool IsGameOver { get; set; }
    public bool IsPause { get; set; }

    // 游戏成绩
    private int _gameScore;
    
    private void Awake()
    {
        Instance = this;
        EventCenter.AddListener(EventDefine.AddScore,AddGameScore);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.AddScore,AddGameScore);
    }

    // 增加游戏成绩
    private void AddGameScore()
    {
        if(IsGameStart==false||IsGameOver||IsPause)return;
        
        _gameScore++;
        EventCenter.Broadcast(EventDefine.UpdateScoreText,_gameScore);
    }
}
