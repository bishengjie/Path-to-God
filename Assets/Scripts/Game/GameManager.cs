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

    private void Awake()
    {
        Instance = this;
    }
}
