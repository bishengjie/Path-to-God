using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset;
    private Vector2 _velocity; // 速度 /速率 /

    private void Update()
    {
        if (_target == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _offset = _target.position - transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            // 平滑              当前的值                         目标值                                                 平滑时间
            float posX = Mathf.SmoothDamp(transform.position.x, _target.position.x - _offset.x, ref _velocity.x, 0.05f);
            float posY = Mathf.SmoothDamp(transform.position.y, _target.position.y - _offset.y, ref _velocity.y, 0.05f);
            if (posY > transform.position.y)
                transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
}
