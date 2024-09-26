using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;  // �v���C���[�̈ړ����x
    public float rotationSpeed = 720f;  // �v���C���[�̉�]���x
    public Transform cameraTransform;  // �J������Transform

    private Rigidbody playerRigidbody;

    // ���͎擾 (WASD����L�[)
    float horizontal;// = Input.GetAxis("Horizontal");  // ���E�̈ړ�
    float vertical;// = Input.GetAxis("Vertical");      // �O��̈ړ�

    void Start()
    {
        // Rigidbody�R���|�[�l���g���擾
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �J�����Ɋ�Â����ړ��������v�Z
        Vector3 moveDirection = (cameraTransform.forward * vertical) + (cameraTransform.right * horizontal);
        moveDirection.y = 0;  // �����ړ������ɂ���

        // �v���C���[�����������ɉ�]����
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // �v���C���[�̈ړ�����
        Vector3 movement = moveDirection.normalized * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    public void OnMove(InputAction.CallbackContext context) 
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        horizontal = moveInput.x;
        vertical = moveInput.y;
    }

}
