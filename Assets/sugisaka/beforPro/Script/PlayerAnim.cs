using System;
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

    private float hor;
    private float ver;

    private GameObject player;
    private Player playerCS;
    private float LateRotatY;
    private float Ynum;
    private float RotatY;
    [SerializeField] private float distans;

    // �A�j���[�V�����̃R���|�[�l���g
    private Animator anim;

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
        direction = PlayerDirection.None;
        anim.SetBool("Idle", true);
        anim.SetBool("Walk", false);
        anim.SetBool("Boost", false);

        LateRotatY= player.transform.rotation.eulerAngles.y;
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

        // �ړ��ʂ�0�ȏ�Ȃ�
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

        // �v���C���[�̌������擾
        RotatY = player.transform.rotation.eulerAngles.y;
        // �v���C���[�̌������ς������
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
                {   // �X����߂�
                    if (Ynum > 0.0f) Ynum -= 2f * Time.deltaTime;
                    if (Ynum <= 0.0f) direction = PlayerDirection.None;
                }
                else
                {   // �X����
                    Ynum += 1f * Time.deltaTime;
                    if (Ynum > 1.0f) Ynum = 1.0f;
                }
                break;
            case PlayerDirection.Left:
                if (RotatY == LateRotatY || distans > -2.0f)
                {   // �X����߂�
                    if (Ynum < 0) Ynum += 2f * Time.deltaTime;
                    if (Ynum >= 0)�@direction = PlayerDirection.None;
                }
                else
                {   // �X����
                    Ynum -= 1f * Time.deltaTime;
                    if (Ynum < -1.0f) Ynum = -1.0f;
                }
                break;
        }
        anim.SetFloat("Blend", Ynum);

        // �v���C���[�̌�����ۑ�
        LateRotatY = RotatY;

    }

}
