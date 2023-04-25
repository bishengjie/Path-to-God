using UnityEngine;
using Random = UnityEngine.Random;

//             主题 
public class BgTheme : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private ManagerVars _vars;// 变动


    private void Awake()
    {
        _vars = ManagerVars.GetManagerVars();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        int ranValue = Random.Range(0, _vars.bgThemeSpriteList.Count);
        _spriteRenderer.sprite = _vars.bgThemeSpriteList[ranValue];

    }
}
