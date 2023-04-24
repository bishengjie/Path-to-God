using System;
using System.Collections;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public SpriteRenderer[] spriteRenderers;
    public GameObject obstacle; // 障碍物
    private bool _startTimer; // 开启计时器
    private float _fallTime; // 掉落时间
    private Rigidbody2D _myBody;

    private void Awake()
    {
        _myBody = GetComponent<Rigidbody2D>();
    }

    public void Init(Sprite sprite,float fallTime, int obstacleDirection)
    {
        _myBody.bodyType = RigidbodyType2D.Static;
        _fallTime = fallTime;
        _startTimer = true;
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = sprite;
        }

        if (obstacleDirection == 0) // 朝右边
        {
            if (obstacle != null)
            {
                obstacle.transform.localPosition = new Vector3(-obstacle.transform.localPosition.x, obstacle.transform.localPosition.y, 0);

            }
        }
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameStart == false || GameManager.Instance.PlayerIsMove == false) return;
        if (_startTimer)
        {
            _fallTime -= Time.deltaTime;
            if (_fallTime < 0) // 倒计时结束
            {
                // 掉落
                _startTimer = false;
                if (_myBody.bodyType != RigidbodyType2D.Dynamic)
                {
                    _myBody.bodyType = RigidbodyType2D.Dynamic;
                    StartCoroutine(DealyHide());
                }
            }
        }
    }

    private IEnumerator DealyHide() //Hide隐藏 
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
