using System;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // アニメーションの状態
    public enum AnimState
    {
        Idle,
        Walk,
        Boost,
    }

    public enum PlayerDirection
    {
        None,
        Right,
        Left,
    }
    public AnimState state;
    public PlayerDirection direction;

    private float hor;
    private float ver;

    private GameObject player;
    private Player playerCS;
    private float LateRotatY;
    private float Ynum;
    private float RotatY;
    [SerializeField] private float distans;

    // アニメーションのコンポーネント
    private Animator anim;

    private void Awake()
    {
        // 親オブジェクト取得
        player = transform.parent.gameObject;
        // Playerコンポーネントを取得
        playerCS = player.GetComponent<Player>();

        // アニメーターコンポーネントを取得
        anim = GetComponent<Animator>();
        // アニメーションの状態を初期化
        state = AnimState.Idle;
        direction = PlayerDirection.None;
        anim.SetBool("Idle", true);
        anim.SetBool("Walk", false);
        anim.SetBool("Boost", false);

        LateRotatY= player.transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        // 現在のアニメーションの状態を取得
        AnimState nowState = state;
        // 移動量を取得
        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2( Mathf.Abs(ver), Mathf.Abs(hor));
        float movel = move.magnitude;

        // 移動量が0以上なら
        if (movel > 0)
        {
            if (playerCS.isBoosting)
                state = AnimState.Boost;
            else
                state = AnimState.Walk;
        }
        else
        {
            state = AnimState.Idle;
        }

        // アニメーションの状態が変更されたら
        if (nowState != state)
        {
            // アニメーションの状態を変更
            switch (state)
            {
                case AnimState.Idle:
                    anim.SetBool("Idle", true);
                    anim.SetBool("Walk", false);
                    anim.SetBool("Boost", false);
                    SoundManager.instance.StopBGM("bike");
                    break;
                case AnimState.Walk:
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", true);
                    anim.SetBool("Boost", false);
                    if (nowState != AnimState.Boost)
                    {
                        SoundManager.instance.PlayBGM("bike");
                    }
                    break;
                case AnimState.Boost:
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", true);
                    anim.SetBool("Boost", true);
                    if (nowState != AnimState.Walk)
                    {
                        SoundManager.instance.PlayBGM("bike");
                    }
                    break;
            }
        }

        // プレイヤーの向きを取得
        RotatY = player.transform.rotation.eulerAngles.y;
        // プレイヤーの向きが変わったら
        distans = Mathf.Abs(RotatY) - Mathf.Abs(LateRotatY);
        switch (direction)
        {
            case PlayerDirection.None:
                if (RotatY != LateRotatY)
                {
                    if (distans > 2.0f || -2.0 > distans)
                    {
                        if (RotatY > LateRotatY)
                        {
                            direction = PlayerDirection.Right;
                            Ynum += 2f * Time.deltaTime;
                        }
                        else
                        {
                            direction = PlayerDirection.Left;
                            Ynum -= 2f * Time.deltaTime;
                        }
                    }
                }
                break;
            case PlayerDirection.Right:
                if (RotatY == LateRotatY || distans < 2.0f)
                {   // 傾けを戻す
                    if (Ynum > 0.0f) Ynum -= 2f * Time.deltaTime;
                    if (Ynum <= 0.0f) direction = PlayerDirection.None;
                }
                else
                {   // 傾ける
                    Ynum += 1f * Time.deltaTime;
                    if (Ynum > 1.0f) Ynum = 1.0f;
                }
                break;
            case PlayerDirection.Left:
                if (RotatY == LateRotatY || distans > -2.0f)
                {   // 傾けを戻す
                    if (Ynum < 0) Ynum += 2f * Time.deltaTime;
                    if (Ynum >= 0)　direction = PlayerDirection.None;
                }
                else
                {   // 傾ける
                    Ynum -= 1f * Time.deltaTime;
                    if (Ynum < -1.0f) Ynum = -1.0f;
                }
                break;
        }
        anim.SetFloat("Blend", Ynum);

        // プレイヤーの向きを保存
        LateRotatY = RotatY;

    }

}
