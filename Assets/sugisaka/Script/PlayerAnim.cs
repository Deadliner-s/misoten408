using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // �A�j���[�V�����̏��
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

    // �A�j���[�V�����̃R���|�[�l���g
    private Animator anim;

    private void Awake()
    {
        // �e�I�u�W�F�N�g�̃R���|�[�l���g���擾
        player = transform.parent.GetComponent<Player>();

        // �A�j���[�^�[�R���|�[�l���g���擾
        anim = GetComponent<Animator>();
        // �A�j���[�V�����̏�Ԃ�������
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
        // ���݂̃A�j���[�V�����̏�Ԃ��擾
        AnimState nowState = state;
        // �ړ��ʂ��擾
        ver = Input.GetAxis("Vertical");
        hor = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2( Mathf.Abs(ver), Mathf.Abs(hor));
        float movel = move.magnitude;
        anim.SetFloat("Blend", movel);

        // �ړ��ʂ�0�ȏ�Ȃ�
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

        // �A�j���[�V�����̏�Ԃ��ύX���ꂽ��
        if (nowState != state)
        {
            // �A�j���[�V�����̏�Ԃ�ύX
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
