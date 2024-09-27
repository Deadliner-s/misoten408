using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // 入力
    private float horizontalInput;
    private float verticalInput;

    [Header("移動速度")]
    public float moveSpeed = 5f;        // 自転車の移動速度
    public float boostSpeed = 10f;      // ブースト中の移動速度

    [Header("旋回速度")]
    public float turnSpeed = 150f;      // 自転車の旋回速度

    [Header("ジャンプの高さ")]
    public float jumpHeight = 2f;       // ジャンプの高さ

    [Header("カメラのTransform")]
    public Transform cameraTransform;   // カメラのTransform

    [Header("Boostの最大容量(秒)")]
    public float maxBoost = 100f;       // Boostの最大容量


    private bool isGrounded = true;     // 地面にいるかどうか
    private Vector3 inputDirection;     // 入力方向

    private float currentBoost;         // 現在のBoost量

    private Rigidbody rb;               // Rigidbodyの参照

    private bool isBoosting = false;    // ブースト中かどうか

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbodyを取得
        currentBoost = maxBoost;        // Boostを最大容量に設定
    }

    void Update()
    {
        // カメラに対する入力方向を計算
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // カメラのY方向を無視
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // 入力に基づく方向を決定
        inputDirection = forward * verticalInput + right * horizontalInput;

        // 入力がある場合（ジャンプ中も移動できるようにする）
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            // プレイヤーの前方向を入力方向に合わせて回転
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // ブースト中の移動速度を使用
            float currentSpeed = isBoosting ? boostSpeed : moveSpeed;
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

            // ブースト中の場合はBoostを消費
            if (isBoosting)
            {
                currentBoost = Mathf.Max(0, currentBoost - Time.deltaTime);
            }
        }
    }

    // 地面に接触した時の処理
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // 移動の入力
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }

    // ジャンプの入力
    public void OnJump(InputAction.CallbackContext context)
    {
        // ブースト中はジャンプできない
        if (context.performed && isGrounded && !isBoosting)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    // Boostの入力
    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && currentBoost > 0.0f) // ボタンが押されたとき & ジャンプ中でないとき & Boostが残っているとき
        {
            // ブースト開始
            isBoosting = true;
        }
        else if (context.canceled) // ボタンが離されたとき
        {
            // ブースト終了
            isBoosting = false;
        }
    }

    // 決定ボタンの入力
    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
    }
}
