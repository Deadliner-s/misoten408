using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;  // プレイヤーの移動速度
    public float rotationSpeed = 720f;  // プレイヤーの回転速度
    public Transform cameraTransform;  // カメラのTransform

    private Rigidbody playerRigidbody;

    // 入力取得 (WASDや矢印キー)
    float horizontal;// = Input.GetAxis("Horizontal");  // 左右の移動
    float vertical;// = Input.GetAxis("Vertical");      // 前後の移動

    void Start()
    {
        // Rigidbodyコンポーネントを取得
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // カメラに基づいた移動方向を計算
        Vector3 moveDirection = (cameraTransform.forward * vertical) + (cameraTransform.right * horizontal);
        moveDirection.y = 0;  // 水平移動だけにする

        // プレイヤーが動く方向に回転する
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // プレイヤーの移動処理
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
