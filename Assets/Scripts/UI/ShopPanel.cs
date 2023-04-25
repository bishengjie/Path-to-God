using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopPanel : MonoBehaviour
{

    private ManagerVars _vars;
    private Transform _parent;
    private Text _name;
    private Text _diamond;
    private Button _back;
    private Button _select;
    private Button _buy;
    private int _selectIndex;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowShopPanel,Show);
        _parent = transform.Find("ScrollRect/Parent");
        _name = transform.Find("Name").GetComponent<Text>();
        _diamond = transform.Find("Diamond/diamond").GetComponent<Text>();
        _back = transform.Find("Back").GetComponent<Button>();
        _back.onClick.AddListener(OnBackButtonClick);
        _select = transform.Find("Select").GetComponent<Button>();
        _select.onClick.AddListener(OnSelectButtonClick);
        _buy = transform.Find("Buy").GetComponent<Button>();
        _buy.onClick.AddListener(OnBuyButtonClick);
        _vars = ManagerVars.GetManagerVars();
       
    }

    private void Start()
    {
        Init();
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowShopPanel,Show);
        
    }
    
    // 返回按钮点击
    private void OnBackButtonClick()
    {
        EventCenter.Broadcast(EventDefine.ShowMainPanel);
        gameObject.SetActive(false);
        
    }
    
    // 购买按钮点击
    private void OnBuyButtonClick()
    {
        int price = int.Parse(_buy.GetComponentInChildren<Text>().text);
        if (price > GameManager.Instance.GetAllDiamond())
        {
            Debug.Log("钻石不足，不能购买");
            return;
        }
        GameManager.Instance.UpdateAllDiamond(-price);
        GameManager.Instance.SetSkinUnlocked(_selectIndex);
        _parent.GetChild(_selectIndex).GetChild(0).GetComponent<Image>().color=Color.white;
    }
    
    // 选择按钮点击
    private void OnSelectButtonClick()
    {
        EventCenter.Broadcast(EventDefine.ChangeSkin, _selectIndex);
        GameManager.Instance.SetSelectSkin(_selectIndex);
        EventCenter.Broadcast(EventDefine.ShowMainPanel);
        gameObject.SetActive(false);
    }

    private void Init()
    {
        for (int i = 0; i < _vars.skinSpriteList.Count; i++)
        {
            _parent.GetComponent<RectTransform>().sizeDelta = new Vector2((_vars.skinSpriteList.Count + 2) * 160, 300);
            GameObject go = Instantiate(_vars.skinChooseItemPrefab, _parent);
            // 未解锁
            if (GameManager.Instance.GetSkinUnlocked(i)==false)
            {
                go.GetComponentInChildren<Image>().color = Color.gray;
               
            }
            else // 解锁了
            {
                go.GetComponentInChildren<Image>().color = Color.white;

            }
            // 获取某个游戏对象及其所有子对象中所有指定组件
            go.GetComponentInChildren<Image>().sprite = _vars.skinSpriteList[i];
            go.transform.localPosition = new Vector3((i + 1) * 160, 0, 0);
        }
         // 打开界面直接定位到选中的皮肤
        _parent.transform.localPosition = new Vector3(GameManager.Instance.GetCurrentSelectSkin() * -160, 0);
    }

    private void Update()
    {
        //                          四舍五入
         _selectIndex = (int)Mathf.Round(_parent.transform.localPosition.x / -160.0f);
        if (Input.GetMouseButtonDown(0))
        {
            _parent.transform.DOLocalMoveX(_selectIndex * -160, 0.2f);
            //_parent.transform.localPosition = new Vector3(currentIndex * -160, 0);
        }
        SetItemSize(_selectIndex);
        RefreshUI(_selectIndex);  
    }

    private void SetItemSize(int index)
    {
        for (int i = 0; i < _parent.childCount; i++)
        {
            if (index == i)
            {
                _parent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
            }
            else
            {
                _parent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            }
        }
    }

    private void RefreshUI(int selectIndex) // 刷新 
    {
        _name.text = _vars.skinNameList[selectIndex];
        _diamond.text = GameManager.Instance.GetAllDiamond().ToString();
        // 未解锁
        if (GameManager.Instance.GetSkinUnlocked(selectIndex)==false)
        {
            _select.gameObject.SetActive(false);
            _buy.gameObject.SetActive(true);
            _buy.GetComponentInChildren<Text>().text = _vars.skinPriceList[selectIndex].ToString();
        }
        else
        {
            _select.gameObject.SetActive(true);
            _buy.gameObject.SetActive(false);
        }
    }
}
