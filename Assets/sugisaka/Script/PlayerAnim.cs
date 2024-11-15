using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // アニメーションの状態
    public enum AnimState
    {
        Idle,
        Walk,
    }
    public AnimState state;

    // アニメーションのコンポーネント
    private Animator anim;

    private void Awake()
    {
        // アニメーターコンポーネントを取得
        anim = GetComponent<Animator>();
        // アニメーションの状態を初期化
        state = AnimState.Idle;
        anim.SetBool("Idle", true);
        anim.SetBool("Walk", false);
    }

    private void Update()
    {
        // 現在のアニメーションの状態を取得
        AnimState nowState = state;
        // 移動量を取得
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2( Mathf.Abs(ver), Mathf.Abs(hor));
        float movel = move.magnitude;


        // 移動量が0以上なら
        if (movel > 0)
        {
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
                    break;
                case AnimState.Walk:
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", true);
                    break;
            }
        }
    }

}
