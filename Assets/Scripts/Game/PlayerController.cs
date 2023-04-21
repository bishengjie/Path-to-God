using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 是否向左移动，反之向右
    private bool isMoveLeft;
    private Vector3 nextPlatformLeft, nextPlatformRight;
    private ManagerVars _vars;

    private void Awake()
    {
        _vars = ManagerVars.GetManagerVars();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
        }
    }

    private void Jump()
    {
        if (isMoveLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
            
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag=="Platform")
        {
            Vector3 currentPlatformPos = col.gameObject.transform.position;
            nextPlatformLeft = new Vector3(currentPlatformPos.x - _vars.nextXPos, currentPlatformPos.y + _vars.nextYPos, 0);
            nextPlatformRight = new Vector3(currentPlatformPos.x + _vars.nextXPos, currentPlatformPos.y + _vars.nextYPos, 0);
        }
    }
}
