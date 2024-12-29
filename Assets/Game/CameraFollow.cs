using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // プレイヤーのTransform
    public Transform player;

    // カメラのオフセット
    public Vector3 offset;

    // カメラの追従速度
    public float followSpeed = 5.0f;

    void LateUpdate()
    {
        if (player != null)
        {
            // 目標位置を計算
            Vector3 targetPosition = player.position + offset;

            // カメラをスムーズに追従させる
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // カメラの回転角度を固定
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles);
        }
    }
}
