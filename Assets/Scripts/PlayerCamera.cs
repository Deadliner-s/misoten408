using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private Vector2 input;              // 入力

    [Header("カメラの回転速度")]
    public float lookSpeed = 2f;        // カメラの回転速度

    [Header("カメラの角度制限")]
    public float maxPitch = 50f;        // ピッチの最大値（上向き）
    public float minPitch = -25f;       // ピッチの最小値（下向き）

    private Transform playerTransform;  // プレイヤーのTransform

    private float pitch = 0;            // カメラの上下の角度
    private float yaw = 0;              // カメラの左右の角度

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;    // プレイヤーのTransformを取得
        yaw = playerTransform.eulerAngles.y;                      // プレイヤーの向きを初期値に設定
    }

    void LateUpdate()
    {
        // カメラの角度を更新
        yaw += input.x * lookSpeed;
        pitch -= input.y * lookSpeed;

        // ピッチ角度の制限
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // カメラをプレイヤーの後ろに回転させる
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        transform.position = playerTransform.position - transform.forward * 5f + Vector3.up * 2f; // 距離や高さは調整
    }

    // InputSystemのOnLookメソッド
    public void OnLook(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}
