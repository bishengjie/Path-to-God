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
    public List<GameObject> commonPlatformGroup = new();
    public List<GameObject> grassPlatformGroup = new();
    public List<GameObject> winterPlatformGroup = new();
    public GameObject spikePlatformLeft;
    public GameObject spikePlatformRight;
    public GameObject deathEffect;
    public GameObject diamondPrefab;
    public float nextXPos = 0.554f, nextYPos = 0.645f;
}