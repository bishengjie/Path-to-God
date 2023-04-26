using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    private Image _imageBg;
    private Text _hint;


    private void Awake()
    {
        _imageBg = GetComponent<Image>();
        _hint = GetComponentInChildren<Text>();
        _imageBg.color = new Color(_imageBg.color.r, _imageBg.color.g, _imageBg.color.b, 0);
        _hint.color = new Color(_hint.color.r, _hint.color.g, _hint.color.b, 0);
        EventCenter.AddListener<string>(EventDefine.Hint, Show);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventDefine.Hint, Show);
    }

    private void Show(string text)
    {
        StopCoroutine("Delay");
        transform.localPosition = new Vector3(0, -70, 0);
        transform.DOLocalMoveY(0, -0.3f).OnComplete(() =>
        {
            StartCoroutine("Delay");
        });
        _imageBg.DOColor(new Color(_imageBg.color.r, _imageBg.color.g, _imageBg.color.b, 0.4f), 0.1f);
        _hint.DOColor(new Color(_hint.color.r, _hint.color.g, _hint.color.b, 1), 0.1f);
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        transform.DOLocalMoveY(70, -0.3f);
        _imageBg.DOColor(new Color(_imageBg.color.r, _imageBg.color.g, _imageBg.color.b, 0), 0.1f);
        _hint.DOColor(new Color(_hint.color.r, _hint.color.g, _hint.color.b, 0), 0.1f);
    }
}
