using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // �A�j���[�V�����̏��
    public enum AnimState
    {
        Idle,
        Walk,
    }
    public AnimState state;

    // �A�j���[�V�����̃R���|�[�l���g
    private Animator anim;

    private void Awake()
    {
        // �A�j���[�^�[�R���|�[�l���g���擾
        anim = GetComponent<Animator>();
        // �A�j���[�V�����̏�Ԃ�������
        state = AnimState.Idle;
        anim.SetBool("Idle", true);
        anim.SetBool("Walk", false);
    }

    private void Update()
    {
        // ���݂̃A�j���[�V�����̏�Ԃ��擾
        AnimState nowState = state;
        // �ړ��ʂ��擾
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2( Mathf.Abs(ver), Mathf.Abs(hor));
        float movel = move.magnitude;


        // �ړ��ʂ�0�ȏ�Ȃ�
        if (movel > 0)
        {
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
                    break;
                case AnimState.Walk:
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walk", true);
                    break;
            }
        }
    }

}
