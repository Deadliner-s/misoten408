using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;  // �J�������Ǐ]����^�[�Q�b�g
    public float distance = 5.0f;  // �^�[�Q�b�g����̋���
    public float mouseSensitivity = 2.0f;  // �}�E�X���x
    public float controllerSensitivity = 100.0f;  // �R���g���[���[���x
    public Vector2 pitchLimits = new Vector2(-40, 85);  // �㉺�̊p�x����

    private float pitch = 0.0f;  // �㉺�̊p�x
    private float yaw = 0.0f;  // ���E�̊p�x

    void Start()
    {
        // �����̌�����ݒ�
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        //// �}�E�X�܂��̓R���g���[���[�ŃJ�����̉�]�𐧌�
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //float controllerX = Input.GetAxis("RightStickHorizontal") * controllerSensitivity * Time.deltaTime;
        //float controllerY = Input.GetAxis("RightStickVertical") * controllerSensitivity * Time.deltaTime;

        //yaw += mouseX + controllerX;
        //pitch -= mouseY + controllerY;

        //// �㉺�̊p�x�𐧌�
        //pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

        // �J�����̈ʒu�Ɖ�]���^�[�Q�b�g�Ɋ�Â��čX�V
        Vector3 targetPosition = target.position - new Vector3(0, 0, distance);
        transform.position = targetPosition;

        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position - transform.forward * distance;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        yaw += moveInput.x * controllerSensitivity * Time.deltaTime;
        pitch -= moveInput.y * controllerSensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);
    }




}
