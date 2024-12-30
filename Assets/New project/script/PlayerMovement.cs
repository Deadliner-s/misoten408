using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // �ړ����x
    public float moveSpeed = 5f;

    // ��]���x
    public float rotationSpeed = 10f;

    // �J�����̎Q��
    public Camera mainCamera;

    void Start()
    {
        // ���C���J�����������擾�i�w�肪�Ȃ��ꍇ�j
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // ���͂��擾
        float moveX = Input.GetAxis("Horizontal"); // A, D�L�[�ō��E�ړ�
        float moveZ = Input.GetAxis("Vertical");   // W, S�L�[�őO��ړ�

        // ���͕������J������ŕϊ�
        Vector3 cameraForward = mainCamera.transform.forward; // �J�����̑O����
        Vector3 cameraRight = mainCamera.transform.right;     // �J�����̉E����

        // Y�������̉e�������O
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // ���K�����ĕ����x�N�g�����擾
        cameraForward.Normalize();
        cameraRight.Normalize();

        // �ړ��������v�Z
        Vector3 moveDirection = cameraForward * moveZ + cameraRight * moveX;

        // �ړ�����
        if (moveDirection.magnitude > 0.1f) // ���͂�����ꍇ�̂ݏ���
        {
            // �ړ�
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // �v���C���[�̌������ړ������ɍ��킹��
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
