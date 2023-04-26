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
        EventCenter.AddListener<int>(EventDefine.UpdateScoreText,UpdateScoreText);
        EventCenter.AddListener<int>(EventDefine.UpdateDiamondText,UpdateDiamondText);
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
        EventCenter.RemoveListener<int>(EventDefine.UpdateScoreText,UpdateScoreText);
        EventCenter.RemoveListener<int>(EventDefine.UpdateDiamondText,UpdateDiamondText);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    // 更新成绩显示
    private void UpdateScoreText(int score)
    {
        _score.text = score.ToString();
    }  
    // 更新钻石数量显示
    private void UpdateDiamondText(int diamond)
    {
        _diamondCount.text = diamond.ToString();
    }
    
    // 暂停按钮点击
    private void OnPauseButtonClick()
    {
        EventCenter.Broadcast(EventDefine.PlayClickAudio);
        _buttonPlay.gameObject.SetActive(true);
        _buttonPause.gameObject.SetActive(false);
        // 游戏暂停
        Time.timeScale = 0;
        GameManager.Instance.IsPause = true;
    }
    // 开始按钮点击
    private void OnPlayButtonClick()
    {
        EventCenter.Broadcast(EventDefine.PlayClickAudio);
        _buttonPlay.gameObject.SetActive(false);
        _buttonPause.gameObject.SetActive(true);
        // 游戏继续
        Time.timeScale = 1;
        GameManager.Instance.IsPause = false;
    }

}
