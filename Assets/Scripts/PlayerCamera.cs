using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    // ����
    private float horizontalInput;
    private float verticalInput;

    [Header("�v���C���[��Transform")]
    public Transform playerTransform;   // �v���C���[��Transform

    [Header("�J�����̉�]���x")]
    public float lookSpeed = 2f;        // �J�����̉�]���x

    [Header("�J�����̊p�x����")]
    public float maxPitch = 50f;        // �s�b�`�̍ő�l�i������j
    public float minPitch = -25f;       // �s�b�`�̍ŏ��l�i�������j

    private float pitch = 0;            // �J�����̏㉺�̊p�x
    private float yaw = 0;              // �J�����̍��E�̊p�x

    void Start()
    {
        // �v���C���[�̌����������l�ɐݒ�
        yaw = playerTransform.eulerAngles.y;
    }

    void LateUpdate()
    {
        // �J�����̊p�x���X�V
        yaw += horizontalInput * lookSpeed;
        pitch -= verticalInput * lookSpeed;

        // �s�b�`�p�x�̐���
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // �J�������v���C���[�̌��ɉ�]������
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        transform.position = playerTransform.position - transform.forward * 5f + Vector3.up * 2f; // �����⍂���͒���
    }

    // InputSystem��OnLook���\�b�h
    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }
}
