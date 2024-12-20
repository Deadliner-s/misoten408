using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerAnim_g : MonoBehaviour
{

    // アニメーションの状態
    public enum AnimState
    {
        Idle,
        Walk,
    }

    public AnimState state;

    private GameObject player;
    private Player playerCS;

    // アニメーションのコンポーネント
    private Animator anim;

    [SerializeField] float movel;

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
        anim.SetBool("walk", false);
    }

    private void Update()
    {
        // 現在のアニメーションの状態を取得
        AnimState nowState = state;
        // 移動量を取得
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(Mathf.Abs(ver), Mathf.Abs(hor));
        movel = move.magnitude;

        // 移動量が0以上なら
        if (movel > 0)
        {
            state = AnimState.Walk;
        }
        else
        {
            state = AnimState.Idle;
        }

        // 現在のアニメーションの状態が変更された場合
        if (nowState != state)
        {
            // アニメーションの状態に応じてアニメーションを切り替える
            switch (state)
            {
                case AnimState.Idle:
                    anim.SetBool("walk", false);
                    break;
                case AnimState.Walk:
                    anim.SetBool("walk", true);
                    break;
            }
        }
    }
}
