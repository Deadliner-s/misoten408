using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 移動速度
    public float moveSpeed = 5f;

    // 回転速度
    public float rotationSpeed = 10f;

    // カメラの参照
    public Camera mainCamera;

    void Start()
    {
        // メインカメラを自動取得（指定がない場合）
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // 入力を取得
        float moveX = Input.GetAxis("Horizontal"); // A, Dキーで左右移動
        float moveZ = Input.GetAxis("Vertical");   // W, Sキーで前後移動

        // 入力方向をカメラ基準で変換
        Vector3 cameraForward = mainCamera.transform.forward; // カメラの前方向
        Vector3 cameraRight = mainCamera.transform.right;     // カメラの右方向

        // Y軸方向の影響を除外
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // 正規化して方向ベクトルを取得
        cameraForward.Normalize();
        cameraRight.Normalize();

        // 移動方向を計算
        Vector3 moveDirection = cameraForward * moveZ + cameraRight * moveX;

        // 移動処理
        if (moveDirection.magnitude > 0.1f) // 入力がある場合のみ処理
        {
            // 移動
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // プレイヤーの向きを移動方向に合わせる
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
