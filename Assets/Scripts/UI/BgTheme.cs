using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//             主题 
public class BgTheme : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public Sprite[] sprites;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
}
