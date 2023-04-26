using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReSetPanel : MonoBehaviour
{
    private Button _buttonYes;
    private Button _buttonNo;
    private Image _imageBg;
    private GameObject _dialog;


    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowReSetPanel, Show);

        _imageBg = transform.Find("bg").GetComponent<Image>();
        _buttonYes = transform.Find("Dialog/Yes").GetComponent<Button>();
        _buttonNo = transform.Find("Dialog/No").GetComponent<Button>();
        _dialog = transform.Find("Dialog").gameObject;

        _imageBg.color = new Color(_imageBg.color.r, _imageBg.color.g, _imageBg.color.b, 0);
        _dialog.transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowReSetPanel, Show);
    }

    private void Show()
    {
        gameObject.SetActive(true);
        _imageBg.DOColor(new Color(_imageBg.color.r, _imageBg.color.g, _imageBg.color.b, 0.3f), 0.3f);
        _dialog.transform.DOScale(Vector3.one, 0.3f);
    }
}
