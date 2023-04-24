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
    // 玩家是否开始移动
    public bool PlayerIsMove { get; set; }

    // 游戏成绩
    private int _gameScore;
    
    private void Awake()
    {
        Instance = this;
        EventCenter.AddListener(EventDefine.AddScore,AddGameScore);
        EventCenter.AddListener(EventDefine.PlayerMove,PlayerMove);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.AddScore,AddGameScore);
        EventCenter.RemoveListener(EventDefine.PlayerMove,PlayerMove);
    }

    // 玩家移动会调用到此方法
    private void PlayerMove()
    {
        PlayerIsMove = true;
    }
    // 增加游戏成绩
    private void AddGameScore()
    {
        if(IsGameStart==false||IsGameOver||IsPause)return;
        
        _gameScore++;
        EventCenter.Broadcast(EventDefine.UpdateScoreText,_gameScore);
    }

    // 获取游戏成绩
    public int  GetGameScore()
    {
        return _gameScore;
    }
}
