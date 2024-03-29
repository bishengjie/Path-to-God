using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform rayDown,rayLeft,rayRight;

    public LayerMask platformLayer,obstacleLayer;
    // 是否向左移动，反之向右
    private bool isMoveLeft;
    // 是否正在跳跃
    private bool isJump;
    private Vector3 nextPlatformLeft, nextPlatformRight;
    private ManagerVars _vars;
    private Rigidbody2D _myBody;
    private SpriteRenderer _spriteRenderer;
    private bool _isMove;
    private AudioSource _mAudioSouse;

    private void Awake()
    {
        EventCenter.AddListener<bool>(EventDefine.IsMusicOn, IsMusicOn);
        EventCenter.AddListener<int>(EventDefine.ChangeSkin,ChangeSkin);
        _vars = ManagerVars.GetManagerVars();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myBody = GetComponent<Rigidbody2D>();
        _mAudioSouse = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ChangeSkin(GameManager.Instance.GetCurrentSelectSkin());
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.ChangeSkin,ChangeSkin);
        EventCenter.RemoveListener<bool>(EventDefine.IsMusicOn, IsMusicOn);
    }
    
    // 音效是否开启
    private void IsMusicOn(bool value)
    {
        _mAudioSouse.mute = !value;
    }
    
    // 更换皮肤的调用
    private void ChangeSkin(int skinIndex)
    {
        _spriteRenderer.sprite = _vars.characterSkinSpriteList[skinIndex];
    }
    
    private int count;
    private bool IsPointerOverGameObject(Vector2 mousePosition)
    {
        //创建一个点击事件
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //向点击位置发射一条射线，检测是否点击的UI
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults.Count > 0;
    }

    private void Update()
    {
        Debug.DrawRay(rayDown.position, Vector2.down * 1, Color.red);
        Debug.DrawRay(rayLeft.position, Vector2.left * 0.15f, Color.cyan);
        Debug.DrawRay(rayRight.position, Vector2.right * 0.15f, Color.green);
        
        // if (Application.platform == RuntimePlatform.Android ||
        //     Application.platform == RuntimePlatform.IPhonePlayer)
        // {
        //     int fingerId = Input.GetTouch(0).fingerId;
        //     // 判断是否点击在UI上 是人物不在移动
        //     if (EventSystem.current.IsPointerOverGameObject(fingerId)) return;
        // }
        // else
        // {
        //     if (EventSystem.current.IsPointerOverGameObject()) return;
        // }
        
        if (IsPointerOverGameObject(Input.mousePosition)) return;

        if (GameManager.Instance.IsGameStart == false || GameManager.Instance.IsGameOver
                                                      || GameManager.Instance.IsPause)
            return;
        if (Input.GetMouseButtonDown(0) && isJump == false && nextPlatformLeft != Vector3.zero)
        {
            if (_isMove == false)
            {
                EventCenter.Broadcast(EventDefine.PlayerMove);
                _isMove = true;
            }

            _mAudioSouse.PlayOneShot(_vars.jumpClip);
            EventCenter.Broadcast(EventDefine.DecidePath);
            isJump = true;
            Vector3 mousePos = Input.mousePosition;
            // 点击的是左边屏幕
            if (mousePos.x <= Screen.width / 2)
            {
                isMoveLeft = true;
            }
            // 点击的是右边屏幕
            else if (mousePos.x > Screen.width / 2)
            {
                isMoveLeft = false;
            }

            Jump();
        }

        // 游戏结束了
        if (_myBody.velocity.y < 0 && IsRayPlatform() == false && GameManager.Instance.IsGameOver == false)
        {
            _mAudioSouse.PlayOneShot(_vars.fallClip);
            _spriteRenderer.sortingLayerName = "Default";
            GetComponent<BoxCollider2D>().enabled = false;
            GameManager.Instance.IsGameOver = true;
            // 协程
            StartCoroutine(DealyShowGameOverPanel());
        }

        // 正在跳跃并且检测到障碍物且游戏判定over
        if (isJump && IsRayObstacle() && GameManager.Instance.IsGameOver == false)
        {
            _mAudioSouse.PlayOneShot(_vars.hitClip);
            GameObject go = ObjectPool.Instance.GetDeathEffect();
            go.SetActive(true);
            go.transform.position = transform.position;
            GameManager.Instance.IsGameOver = true;
            // 销毁人物
            _spriteRenderer.enabled = false;
            // 协程
            StartCoroutine(DealyShowGameOverPanel());
        }

        if (transform.position.y - Camera.main.transform.position.y < -6 && GameManager.Instance.IsGameOver == false)
        {
            _mAudioSouse.PlayOneShot(_vars.fallClip);
            GameManager.Instance.IsGameOver = true;
            StartCoroutine(DealyShowGameOverPanel());
        }
    }

    IEnumerator DealyShowGameOverPanel()
    {
        yield return new WaitForSeconds(1f);
        // 调用结束面板
        EventCenter.Broadcast(EventDefine.ShowGameOverPanel);
    }

    private GameObject lastHitGo = null;
    // 是否检测到平台s
    private bool IsRayPlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayDown.position, Vector2.down, 1f, platformLayer);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Platform")
            {
                if (lastHitGo != hit.collider.gameObject)
                {
                    if (lastHitGo == null)
                    {
                        lastHitGo = hit.collider.gameObject;
                        return true;
                    }
                    EventCenter.Broadcast(EventDefine.AddScore);
                    lastHitGo = hit.collider.gameObject;
                }

                return true;
            }
        }

        return false;
    }

    // 是否检测到障碍物
    private bool IsRayObstacle()
    {
        RaycastHit2D LeftHit = Physics2D.Raycast(rayLeft.position, Vector2.left, 0.15f, obstacleLayer);
        RaycastHit2D RightHit = Physics2D.Raycast(rayRight.position, Vector2.right, 0.15f, obstacleLayer);
        if (LeftHit.collider != null)
        {
            if (LeftHit.collider.tag == "Obstacle")
            {
                return true;
            }
        }

        if (RightHit.collider != null)
        {
            if (RightHit.collider.tag == "Obstacle")
            {
                return true;
            }
        }

        return false;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Pickup")
        {
            _mAudioSouse.PlayOneShot(_vars.diamondClip);
            EventCenter.Broadcast(EventDefine.AddDiamond);
            // 吃到钻石
            col.gameObject.SetActive(false);
            
        }
    }
}
