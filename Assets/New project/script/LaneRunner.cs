using UnityEngine;

public class LaneRunner : MonoBehaviour
{
    // ���[�����
    private int currentLane = 1; // 0: ��, 1: ���� (����), 2: �E
    public float laneDistance = 2.0f; // ���[���Ԃ̋���

    // �L�����N�^�[�̈ړ����x
    public float lateralSpeed = 10.0f; // ���E�ړ����x
    public float forwardSpeed = 5.0f; // �O�i���x
    private float originalForwardSpeed; // ���̑O�i���x��ۑ�

    // �S�[���֘A
    private bool hasReachedGoal = false; // �S�[���ɓ��B�������ǂ���
    public float decelerationRate = 0.1f; // �����̊���

    // �W�����v�֘A
    public float jumpHeight = 2.0f;
    public float jumpDuration = 0.5f;
    private bool isGrounded = true;
    private float groundY; // �n�ʂ�Y���W
    private float jumpTime; // �W�����v�o�ߎ���
    private bool isJumping = false;
    private bool isMoving = false; // �ړ����̔���

    // �A�j���[�V�����֘A
    public LaneAni laneAni;

    // �ڕW�ʒu
    private Vector3 targetPosition;

    // ���U���g��Canvas
    [SerializeField] private GameObject ResurtCanvas;

    void Start()
    {
        // �����̖ڕW�ʒu��ݒ�
        targetPosition = transform.position;

        // �n�ʂ�Y���W��ۑ�
        groundY = transform.position.y;

        // ���̑O�i���x��ۑ�
        originalForwardSpeed = forwardSpeed;

        // �v���C���[�̎q�I�u�W�F�N�g�̃A�j���[�V�������擾
        laneAni = GetComponentInChildren<LaneAni>();
    }

    void Update()
    {
        if (!hasReachedGoal)
        {
            // ���͏��� (�W�����v���܂��͈ړ����͍��E�ړ��𖳌���)
            if (!isJumping && !isMoving)
            {
                if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
                {
                    currentLane--;
                    isMoving = true;
                    laneAni.LaneChangeAni(0);
                }
                else if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
                {
                    currentLane++;
                    isMoving = true;
                    laneAni.LaneChangeAni(1);
                }
            }

            // ���͏��� (�W�����v������шړ����łȂ��ꍇ�̂݃W�����v�\)
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isMoving)
            {
                StartJump();
                laneAni.LaneChangeAni(2);
                SoundManager.instance.PlaySE("jump");
            }

            // �W�����v����
            if (isJumping)
            {
                HandleJump();
                //hasReachedGoal = true;
            }

            // ���[���ړ�����
            if (isMoving)
            {
                HandleLaneMovement();
            }

            // �O�i����
            MoveForward();
        }
        else
        {
            // �S�[�����B���̌�������
            HandleDeceleration();

            // ���U���g�\��
            ResurtCanvas.GetComponent<RunGameResult>().UpdateResultDisplay();
        }
    }

    void StartJump()
    {
        isGrounded = false;
        isJumping = true;
        jumpTime = 0f;
    }

    void HandleJump()
    {
        jumpTime += Time.deltaTime;
        float progress = jumpTime / jumpDuration;

        if (progress >= 1f)
        {
            // �W�����v�I��
            isJumping = false;
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            isGrounded = true;
        }
        else
        {
            // ��������`���W�����v�̍����v�Z
            float height = 4 * jumpHeight * progress * (1 - progress); // �ȈՓI�ȕ�����
            transform.position = new Vector3(transform.position.x, groundY + height, transform.position.z);
        }
    }

    void HandleLaneMovement()
    {
        // �ڕW�ʒu���v�Z
        targetPosition = new Vector3(currentLane * laneDistance - laneDistance, transform.position.y, transform.position.z);

        // ���݂̈ʒu��ڕW�ʒu�ɋ߂Â���
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, lateralSpeed * Time.deltaTime);

        // �ړ��I������
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false;
        }
    }

    void MoveForward()
    {
        // ��ɑO�i
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
    }

    void HandleDeceleration()
    {
        // �O�i���x�����X�Ɍ���
        forwardSpeed = Mathf.Max(0, forwardSpeed - decelerationRate * Time.deltaTime);

        // ��~��ɑO�i�����𖳌���
        if (forwardSpeed <= 0)
        {
            forwardSpeed = 0;
        }

        // �S�[�����B��̑O�i����
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �S�[���I�u�W�F�N�g�ɐڐG�����ꍇ
        if (other.CompareTag("Goal"))
        {
            hasReachedGoal = true;
        }
    }
}
