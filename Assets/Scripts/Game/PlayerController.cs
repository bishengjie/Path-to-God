using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 是否向左移动，反之向右
    private bool isMoveLeft;
    // 是否正在跳跃
    private bool isJump;
    private Vector3 nextPlatformLeft, nextPlatformRight;
    private ManagerVars _vars;

    private void Awake()
    {
        _vars = ManagerVars.GetManagerVars();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameStart == false || GameManager.Instance.IsGameOver)
            return;
        if (Input.GetMouseButtonDown(0) && isJump == false)
        {
            EventCenter.Broadcast(EventDefine.DecidePath);
            isJump = true;
            Vector3 mousePos = Input.mousePosition;
            // 点击的是左边屏幕
            if (mousePos.x <= Screen.width / 2)
            {
                isMoveLeft = true;
            }
            else if (mousePos.x > Screen.width / 2)
            {
                isMoveLeft = false;
            }

            Jump();
        }
    }

    private void Jump()
    {
        if (isMoveLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.DOMoveX(nextPlatformLeft.x, 0.2f);
            transform.DOMoveY(nextPlatformLeft.y + 0.8f, 0.15f);
        }
        else
        {
            transform.DOMoveX(nextPlatformRight.x, 0.2f);
            transform.DOMoveY(nextPlatformRight.y + 0.8f, 0.15f);
            transform.localScale = Vector3.one;

        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Platform")
        {
            isJump = false;
            Vector3 currentPlatformPos = col.gameObject.transform.position;
            nextPlatformLeft = new Vector3(currentPlatformPos.x - _vars.nextXPos, currentPlatformPos.y + _vars.nextYPos,
                0);
            nextPlatformRight = new Vector3(currentPlatformPos.x + _vars.nextXPos,
                currentPlatformPos.y + _vars.nextYPos, 0);
        }
    }
}
