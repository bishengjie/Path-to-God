using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "CreatManagerVarsContainer")]
public class ManagerVars : ScriptableObject
{
    public static ManagerVars GetManagerVars()
    {
        return Resources.Load<ManagerVars>("ManagerVarsContainer");
    }
    public List<Sprite> bgThemeSpriteList = new();
    public List<Sprite> platformThemeSpriteList = new();
    public GameObject characterPrefab;
    public GameObject normalPlatformPrefab;
    public float nextXPos = 0.554f, nextYPos = 0.645f;
}