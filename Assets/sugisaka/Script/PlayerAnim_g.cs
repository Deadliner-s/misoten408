using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerAnim_g : MonoBehaviour
{

    // �A�j���[�V�����̏��
    public enum AnimState
    {
        Idle,
        Walk,
    }

    public AnimState state;

    private GameObject player;
    private Player playerCS;

    // �A�j���[�V�����̃R���|�[�l���g
    private Animator anim;

    [SerializeField] float movel;

    private void Awake()
    {
        // �e�I�u�W�F�N�g�擾
        player = transform.parent.gameObject;
        // Player�R���|�[�l���g���擾
        playerCS = player.GetComponent<Player>();

        // �A�j���[�^�[�R���|�[�l���g���擾
        anim = GetComponent<Animator>();
        // �A�j���[�V�����̏�Ԃ�������
        state = AnimState.Idle;
        anim.SetBool("walk", false);
    }

    private void Update()
    {
        // ���݂̃A�j���[�V�����̏�Ԃ��擾
        AnimState nowState = state;
        // �ړ��ʂ��擾
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(Mathf.Abs(ver), Mathf.Abs(hor));
        movel = move.magnitude;

        // �ړ��ʂ�0�ȏ�Ȃ�
        if (movel > 0)
        {
            state = AnimState.Walk;
        }
        else
        {
            state = AnimState.Idle;
        }

        // ���݂̃A�j���[�V�����̏�Ԃ��ύX���ꂽ�ꍇ
        if (nowState != state)
        {
            // �A�j���[�V�����̏�Ԃɉ����ăA�j���[�V������؂�ւ���
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
