using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // 入力
    private float horizontalInput;
    private float verticalInput;

    [Header("移動速度")]
    public float moveSpeed = 5f;        // 自転車の移動速度

    [Header("旋回速度")]
    public float turnSpeed = 150f;      // 自転車の旋回速度

    [Header("ジャンプの高さ")]
    public float jumpHeight = 2f;       // ジャンプの高さ

    [Header("カメラのTransform")]
    public Transform cameraTransform;   // カメラのTransform

    private bool isGrounded = true;     // 地面にいるかどうか
    private Vector3 inputDirection;     // 入力方向

    private Rigidbody rb;               // Rigidbodyの参照

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbodyを取得
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

            // プレイヤーを前進させる（地面にいるかどうかに関係なく移動できる）
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    // ジャンプ入力処理
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            Debug.Log("Jump");
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            isGrounded = false;
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

    // InputSystemのOnMoveメソッド
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        Debug.Log("Boost");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Interact");
    }
}
