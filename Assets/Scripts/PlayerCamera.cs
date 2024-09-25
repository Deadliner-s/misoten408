using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;  // カメラが追従するターゲット
    public float distance = 5.0f;  // ターゲットからの距離
    public float mouseSensitivity = 2.0f;  // マウス感度
    public float controllerSensitivity = 100.0f;  // コントローラー感度
    public Vector2 pitchLimits = new Vector2(-40, 85);  // 上下の角度制限

    private float pitch = 0.0f;  // 上下の角度
    private float yaw = 0.0f;  // 左右の角度

    void Start()
    {
        // 初期の向きを設定
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        //// マウスまたはコントローラーでカメラの回転を制御
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //float controllerX = Input.GetAxis("RightStickHorizontal") * controllerSensitivity * Time.deltaTime;
        //float controllerY = Input.GetAxis("RightStickVertical") * controllerSensitivity * Time.deltaTime;

        //yaw += mouseX + controllerX;
        //pitch -= mouseY + controllerY;

        //// 上下の角度を制限
        //pitch = Mathf.Clamp(pitch, pitchLimits.x, pitchLimits.y);

        // カメラの位置と回転をターゲットに基づいて更新
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
