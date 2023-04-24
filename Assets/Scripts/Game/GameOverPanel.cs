using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public Text score, bestScore, addDiamond;
    public Button reStart, rank, home;

    private void Awake()
    {
        reStart.onClick.AddListener(OnReStartButtonClick);
        rank.onClick.AddListener(OnRankButtonClick);
        home.onClick.AddListener(OnHomeButtonClick);
        EventCenter.AddListener(EventDefine.ShowGameOverPanel, Show);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // 移除
        EventCenter.RemoveListener(EventDefine.ShowGameOverPanel, Show);
    }

    private void Show()
    {
        score.text = GameManager.Instance.GetGameScore().ToString();
        addDiamond.text = "+" + GameManager.Instance.GetGameDiamond().ToString();
        gameObject.SetActive(true);
    }

    // 再来一局按钮
    private void OnReStartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameDate.IsAgainGame = true;
    }
    // 排行榜按钮点击
    private void OnRankButtonClick()
    {
        
    }
    // 主界面按钮点击
    private void OnHomeButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameDate.IsAgainGame = false;
    }
}
