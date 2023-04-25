using System;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    private Button _buttonStart;
    private Button _buttonShop;
    private Button _buttonRank;
    private Button _buttonSound;
    private ManagerVars _vars;

    private void Awake()
    {
        _vars = ManagerVars.GetManagerVars();
        EventCenter.AddListener(EventDefine.ShowMainPanel,Show);
        EventCenter.AddListener<int>(EventDefine.ChangeSkin,ChangeSkin);
        Init();
        
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMainPanel,Show);
        EventCenter.RemoveListener<int>(EventDefine.ChangeSkin,ChangeSkin);
        
    }

    // 皮肤更换，这里更换UI皮肤图片
    private void ChangeSkin(int skinIndex)
    {
        
        _buttonShop.transform.GetChild(0).GetComponent<Image>().sprite =
            _vars.skinSpriteList[skinIndex];

    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Start()
    {
        if (GameDate.IsAgainGame)
        {
            EventCenter.Broadcast(EventDefine.ShowGamePanel);
            gameObject.SetActive(false);
        }

        ChangeSkin(GameManager.Instance.GetCurrentSelectSkin());
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
        GameManager.Instance.IsGameStart = true;
                  // 广播，播放
        EventCenter.Broadcast(EventDefine.ShowGamePanel);
        gameObject.SetActive(false); // 隐藏自身
    }
    
    // 商店按钮点击
   private void OnShopButtonClick()
    {
        EventCenter.Broadcast(EventDefine.ShowShopPanel);
        gameObject.SetActive(false);
        
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
