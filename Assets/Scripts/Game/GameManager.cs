using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameDate _date;
    private ManagerVars _vars;
    
    
    // 游戏是否开始
    public bool IsGameStart { get; set; }
    // 游戏是否结束
    public bool IsGameOver { get; set; }
    public bool IsPause { get; set; }
    // 玩家是否开始移动
    public bool PlayerIsMove { get; set; }

    // 游戏成绩
    private int _gameScore;
    private int _gameDiamond;
    
    private bool _isFirstGame;
    private bool _isMusicOn;
    private int[] _bestScoreArr;
    private int _selectSkin;
    private bool[] _skinUnlocked;
    private int _diamondCount;
    
    
    private void Awake()
    {
        _vars = ManagerVars.GetManagerVars();
        Instance = this;
        EventCenter.AddListener(EventDefine.AddScore,AddGameScore);
        EventCenter.AddListener(EventDefine.PlayerMove,PlayerMove);
        EventCenter.AddListener(EventDefine.AddDiamond,AddGameDiamond);
        if (GameDate.IsAgainGame)
        {
            IsGameStart = true;
        }
        InitGameData();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.AddScore,AddGameScore);
        EventCenter.RemoveListener(EventDefine.PlayerMove,PlayerMove);
        EventCenter.RemoveListener(EventDefine.AddDiamond,AddGameDiamond);
    }

    // 保存成绩
    public void SaveScore(int score)
    {
        List<int> list = _bestScoreArr.ToList();
        // 从大到小排序list
        list.Sort((x, y) => (-x.CompareTo(y)));
        _bestScoreArr = list.ToArray();

        // 50 20 10
        int index = -1;
        for (int i = 0; i < _bestScoreArr.Length; i++)
        {
            if (score > _bestScoreArr[i])
            {
                index = i;
            }
        }
        if (index == -1) return;

        for (int i = _bestScoreArr.Length - 1; i > index; i--)
        {
            _bestScoreArr[i] = _bestScoreArr[i - 1];
        }
        _bestScoreArr[index] = score;

        Save();
    }

    // 获取最高分
    public int GetBestScore()
    {
        return _bestScoreArr.Max();
    }
    
    // 获得最高分数组
    public int[] GetScoreArr()
    {
        List<int> list = _bestScoreArr.ToList();
        // 从大到小排序list
        list.Sort((x, y) => (-x.CompareTo(y)));
        _bestScoreArr = list.ToArray();
        return _bestScoreArr;
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
    // 更新游戏钻石数量
    private void  AddGameDiamond()
    {
        _gameDiamond++;
        EventCenter.Broadcast(EventDefine.UpdateDiamondText,_gameDiamond);
    }
    // 获得吃到的钻石数
    public int  GetGameDiamond()
    {
        return _gameDiamond;
    }

    // 获取当前皮肤是否解锁
    public bool GetSkinUnlocked(int index)
    {
        return _skinUnlocked[index];
    } 
    // 设置当前皮肤解锁
    public void SetSkinUnlocked(int index)
    {
        _skinUnlocked[index] = true;
        Save();
    }

    // 获取所有的钻石数量
    public int GetAllDiamond()
    {
        return _diamondCount;
    }
    
    // 更新所有的钻石数量
    public void UpdateAllDiamond(int value)
    {
        _diamondCount += value;
        Save();
    }

    // 设置当前选择的皮肤下标
    public void SetSelectSkin(int index)
    {
        _selectSkin = index;
        Save();
    }

    // 获得当前选择的皮肤
    public int GetCurrentSelectSkin()
    {
        return _selectSkin;
    }

    // 设置音效是否开启
    public void SetIsMusicOn(bool value)
    {
        _isMusicOn = value;
        Save();
    }
    
    // 获取音效是否开启
    public bool GetIsMusicOn()
    {
        return _isMusicOn ;
    }
    
    // 初始化游戏数据
    private void InitGameData()
    {
        Read();
        if (_date != null)
        {
            _isFirstGame = _date.GetIsFirstGame();
        }
        else
        {
            _isFirstGame = true;
        }
        // 如果第一次开始游戏
        if (_isFirstGame)
        {
            _isFirstGame = false;
            _isMusicOn = true;
            _bestScoreArr = new int[3];
            _selectSkin = 0;
            _skinUnlocked = new bool[_vars.skinSpriteList.Count];
            _skinUnlocked[0] = true;
            _diamondCount = 10;
            _date = new GameDate();
            Save();

        }
        else
        {
            _isMusicOn = _date.GetIsMusicOn();
            _bestScoreArr = _date.GetBestScoreArr();
            _selectSkin = _date.GetSelectSkin();
            _skinUnlocked = _date.GetSkinUnlocked();
            _diamondCount = _date.GetDiamondCount();
        }
    }

    // 储存数据
    private void Save()
    {
        try
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Create(Application.persistentDataPath + "/GameData.data"))
            {
                _date.SetBestScoreArr(_bestScoreArr);
                _date.SetDiamondCount(_diamondCount);
                _date.SetIsFirstGame(_isFirstGame);
                _date.SetIsMusicOn(_isMusicOn);
                _date.SetSelectSkin(_selectSkin);
                _date.SetSkinUnlocked(_skinUnlocked);
                binaryFormatter.Serialize(fileStream, _date);
            }
        }
        catch (Exception e)
        {
            print(e.Message);
        }
    }

    // 读取
    private void Read()
    {
        try
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(Application.persistentDataPath + "/GameData.data",FileMode.Open))
            {
                _date = (GameDate)binaryFormatter.Deserialize(fileStream);
            }
        }
        catch (Exception e)
        {
            print(e.Message);
        }
    }
    
    // 重置数据
    public void ReSetData()
    {
        _isFirstGame = false;
        _isMusicOn = true;
        _bestScoreArr = new int[3];
        _selectSkin = 0;
        _skinUnlocked = new bool[_vars.skinSpriteList.Count];
        _skinUnlocked[0] = true;
        _diamondCount = 10;
        
        Save();
    }
}
