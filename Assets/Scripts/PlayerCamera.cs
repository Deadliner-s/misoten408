using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private Vector2 input;              // 入力
    private Rigidbody playerRigidbody;  // プレイヤーのRigidbody

    [Header("カメラの回転速度")]
    public float lookSpeed = 2f;        // カメラの回転速度

    [Header("カメラの角度制限")]
    public float maxPitch = 50f;        // ピッチの最大値（上向き）
    public float minPitch = -25f;       // ピッチの最小値（下向き）

    [Header("カメラの距離設定")]
    public float baseDistance = 5f;     // 基本の距離
    public float speedFactor = 0.5f;    // 速度に基づく距離の増加量
    public float maxDistance = 10f;     // 最大の距離
    public float smoothTime = 0.3f;     // 距離の変化の滑らかさ

    private float pitch = 0;            // カメラの上下の角度
    private float yaw = 0;              // カメラの左右の角度
    private float currentDistance;      // 現在のカメラの距離
    private float distanceVelocity;     // 距離の変化速度

    void Start()
    {
        yaw = this.transform.eulerAngles.y;                      // プレイヤーの向きを初期値に設定
        playerRigidbody = GetComponent<Rigidbody>();             // Rigidbodyコンポーネントを取得
        currentDistance = baseDistance;                          // 初期のカメラの距離を設定
    }

    void LateUpdate()
    {
        // カメラの角度を更新
        yaw += input.x * lookSpeed;
        pitch -= input.y * lookSpeed;

        // ピッチ角度の制限
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // プレイヤーの速度を取得
        float playerSpeed = playerRigidbody.linearVelocity.magnitude;

        // 目標のカメラの距離を計算
        float targetDistance = baseDistance + playerSpeed * speedFactor;

        // targetDistanceの値を制限
        targetDistance = Mathf.Clamp(targetDistance, baseDistance, baseDistance + maxDistance);

        // カメラの距離を滑らかに更新
        currentDistance = Mathf.SmoothDamp(currentDistance, targetDistance, ref distanceVelocity, smoothTime);

        // カメラをプレイヤーの後ろに回転させる
        Camera.main.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        Camera.main.transform.position = this.transform.position - Camera.main.transform.forward * currentDistance + Vector3.up * 2f; // 距離や高さは調整
    }

    // InputSystemのOnLookメソッド
    public void OnLook(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
}
