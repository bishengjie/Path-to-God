using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public Text score, bestScore, addDiamond;
    public Button reStart, rank, home;
    public Image imageNew;

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
        if (GameManager.Instance.GetGameScore()>GameManager.Instance.GetBestScore())
        {
            imageNew.gameObject.SetActive(true);
            bestScore.text = "最高分" + GameManager.Instance.GetGameScore();
        }
        else
        {
            imageNew.gameObject.SetActive(false);
            bestScore.text = "最高分" + GameManager.Instance.GetBestScore();
        }

        GameManager.Instance.SaveScore(GameManager.Instance.GetGameScore());
        score.text = GameManager.Instance.GetGameScore().ToString();
        addDiamond.text = "+" + GameManager.Instance.GetGameDiamond().ToString();
        // 总的钻石数量
        GameManager.Instance.UpdateAllDiamond(GameManager.Instance.GetGameDiamond());
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
