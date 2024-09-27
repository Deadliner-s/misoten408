using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // ����
    private float horizontalInput;
    private float verticalInput;

    [Header("�ړ����x")]
    public float moveSpeed = 5f;        // ���]�Ԃ̈ړ����x
    public float boostSpeed = 10f;      // �u�[�X�g���̈ړ����x

    [Header("���񑬓x")]
    public float turnSpeed = 150f;      // ���]�Ԃ̐��񑬓x

    [Header("�W�����v�̍���")]
    public float jumpHeight = 2f;       // �W�����v�̍���

    [Header("�J������Transform")]
    public Transform cameraTransform;   // �J������Transform

    [Header("Boost�̍ő�e��(�b)")]
    public float maxBoost = 100f;       // Boost�̍ő�e��


    private bool isGrounded = true;     // �n�ʂɂ��邩�ǂ���
    private Vector3 inputDirection;     // ���͕���

    private float currentBoost;         // ���݂�Boost��

    private Rigidbody rb;               // Rigidbody�̎Q��

    private bool isBoosting = false;    // �u�[�X�g�����ǂ���

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody���擾
        currentBoost = maxBoost;        // Boost���ő�e�ʂɐݒ�
    }

    void Update()
    {
        // �J�����ɑ΂�����͕������v�Z
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // �J������Y�����𖳎�
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // ���͂Ɋ�Â�����������
        inputDirection = forward * verticalInput + right * horizontalInput;

        // ���͂�����ꍇ�i�W�����v�����ړ��ł���悤�ɂ���j
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            // �v���C���[�̑O��������͕����ɍ��킹�ĉ�]
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // �u�[�X�g���̈ړ����x���g�p
            float currentSpeed = isBoosting ? boostSpeed : moveSpeed;
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

            // �u�[�X�g���̏ꍇ��Boost������
            if (isBoosting)
            {
                currentBoost = Mathf.Max(0, currentBoost - Time.deltaTime);
            }
        }
    }

    // �n�ʂɐڐG�������̏���
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // �ړ��̓���
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }

    // �W�����v�̓���
    public void OnJump(InputAction.CallbackContext context)
    {
        // �u�[�X�g���̓W�����v�ł��Ȃ�
        if (context.performed && isGrounded && !isBoosting)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    // Boost�̓���
    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && currentBoost > 0.0f) // �{�^���������ꂽ�Ƃ� & �W�����v���łȂ��Ƃ� & Boost���c���Ă���Ƃ�
        {
            // �u�[�X�g�J�n
            isBoosting = true;
        }
        else if (context.canceled) // �{�^���������ꂽ�Ƃ�
        {
            // �u�[�X�g�I��
            isBoosting = false;
        }
    }

    // ����{�^���̓���
    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
    }
}
