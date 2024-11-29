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

    public float hor;
    public float ver;

    private Player player;

    // アニメーションのコンポーネント
    private Animator anim;

    private void Awake()
    {
        // 親オブジェクトのコンポーネントを取得
        player = transform.parent.GetComponent<Player>();

        // アニメーターコンポーネントを取得
        anim = GetComponent<Animator>();
        // アニメーションの状態を初期化
        state = AnimState.Idle;
        direction = PlayerDirection.None;
        anim.SetBool("Idle", true);
        anim.SetBool("Walk", false);
        anim.SetBool("Boost", false);
        anim.SetBool("Right", false);
        anim.SetBool("Left", false);
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
        anim.SetFloat("Blend", movel);

        // 移動量が0以上なら
        if (movel > 0)
        {
            if (player.isBoosting)
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
                    break;
                case AnimState.Walk:
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", true);
                    anim.SetBool("Boost", false);
                    break;
                case AnimState.Boost:
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", true);
                    anim.SetBool("Boost", true);
                    break;
            }
        }
    }

}
