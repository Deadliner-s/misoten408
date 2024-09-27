using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // ����
    private float horizontalInput;
    private float verticalInput;

    [Header("�ړ����x")]
    public float moveSpeed = 5f;        // ���]�Ԃ̈ړ����x

    [Header("���񑬓x")]
    public float turnSpeed = 150f;      // ���]�Ԃ̐��񑬓x

    [Header("�W�����v�̍���")]
    public float jumpHeight = 2f;       // �W�����v�̍���

    [Header("�J������Transform")]
    public Transform cameraTransform;   // �J������Transform

    private bool isGrounded = true;     // �n�ʂɂ��邩�ǂ���
    private Vector3 inputDirection;     // ���͕���

    private Rigidbody rb;               // Rigidbody�̎Q��

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody���擾
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

            // �v���C���[��O�i������i�n�ʂɂ��邩�ǂ����Ɋ֌W�Ȃ��ړ��ł���j
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    // �W�����v���͏���
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            isGrounded = false;
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

    // InputSystem��OnMove���\�b�h
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        Debug.Log("Boost");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
    }
}
