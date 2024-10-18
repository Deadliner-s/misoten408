using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 input;                                  // 入力

    // プレイヤーの状態
    public enum PlayerState
    {
        Driving,                                            // 自転車に乗っている状態
        Walking,                                            // 歩いている状態
    }

    [Header("移動速度")]
    public float driveSpeed = 5f;                           // 自転車の移動速度
    public float boostSpeed = 10f;                          // ブースト中の移動速度
    public float walkSpeed = 2f;                            // 歩行中の移動速度

    [Header("旋回速度")]
    public float driveTurnSpeed = 150f;                     // 自転車の旋回速度
    public float walkTurnSpeed = 10f;                       // 歩行中の旋回速度

    [Header("ジャンプの高さ")]
    public float jumpHeight = 2f;                           // ジャンプの高さ

    [Header("Boostの最大容量(秒)")]
    public float maxBoost = 100f;                           // Boostの最大容量

    [Header("Boostの回復速度(1秒間にnずつ)")]
    public float boostRecoverySpeed = 4f;                   // Boostの回復速度

    [Header("プレイヤーの状態")]
    public PlayerState playerState = PlayerState.Driving;   // 現在のプレイヤーの状態

    private Transform cameraTransform;                      // カメラのTransform

    private bool isGrounded = true;                         // 地面にいるかどうか
    private Vector3 inputDirection;                         // 入力方向

    private float currentBoost;                             // 現在のBoost量
    private bool isBoosting = false;                        // ブースト中かどうか

    private GameObject EventArea;                           // EventAreaのGameObject
    private bool isEventArea = false;                       // EventAreaに入っているかどうか
    private bool isRideArea = false;                        // RideAreaに入っているかどうか
    private bool isTalkArea = false;                        // TalkAreaに入っているかどうか
    private bool isTalking = false;                         // 会話中かどうか

    private GameObject road;                                // RoadのGameObject
    private Vector3 firstPos;                               // 初期位置

    void Start()
    {
        cameraTransform = Camera.main.transform;            // カメラのTransformを取得
        currentBoost = maxBoost;                            // Boostを最大容量に設定
        playerState = PlayerState.Driving;                  // プレイヤーの状態をDrivingに設定
        road = GameObject.FindGameObjectWithTag("Ground");  // RoadのGameObjectを取得
        firstPos = transform.position;                      // 初期位置を取得
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
        inputDirection = forward * input.y + right * input.x;

        // プレイヤーの状態に応じて移動処理を行う
        if (playerState == PlayerState.Driving)
        {
            // 入力がある場合（ジャンプ中も移動できるようにする）
            if (inputDirection.sqrMagnitude > 0.01f)
            {
                // プレイヤーの前方向を入力方向に合わせて回転
                Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, driveTurnSpeed * Time.deltaTime);

                // ブースト中の移動速度を使用
                float currentSpeed = isBoosting ? boostSpeed : driveSpeed;
                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

                // ブースト中の場合はBoostを消費
                if (isBoosting)
                {
                    currentBoost = Mathf.Max(0, currentBoost - Time.deltaTime);
                }
            }
        }
        else if (playerState == PlayerState.Walking)
        {
            // Debug用
            // ESCでisTalkingをfalseにする
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isTalking = false;
            }

            // 会話中は移動不可
            if (isTalking)
                return;

            // 移動方向を計算
            Vector3 moveDirection = (forward * input.y + right * input.x).normalized;

            if (moveDirection.sqrMagnitude > 0.01f)
            {
                // プレイヤーの向きを移動方向に向ける
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, walkTurnSpeed * Time.deltaTime);

                // プレイヤーを移動
                transform.position += moveDirection * walkSpeed * Time.deltaTime;
            }

            // EventAreaの中にいる場合は、EventAreaの中でのみ移動可能
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, EventArea.transform.position.x - EventArea.transform.localScale.x / 3f, EventArea.transform.position.x + EventArea.transform.localScale.x / 3f);
            pos.z = Mathf.Clamp(pos.z, EventArea.transform.position.z - EventArea.transform.localScale.z / 3f, EventArea.transform.position.z + EventArea.transform.localScale.z / 3f);
            transform.position = pos;

            // Boostを回復(1秒間にboostRecoverySpeedずつ回復)
            currentBoost = Mathf.Min(maxBoost, currentBoost + boostRecoverySpeed * Time.deltaTime);
            if (currentBoost >= maxBoost)
            {
                currentBoost = maxBoost;
            }
        }

        // マップ外に落ちてしまった時の処理(roadより下に落ちたら初期位置に戻す)
        if (transform.position.y < road.transform.position.y - 1.0f)
        {
            transform.position = firstPos;
        }


    }

    // 地面に接触した時の処理
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 地面に接触したらジャンプ可能
            isGrounded = true;
        }
    }

    // Areaに入ったときの処理
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EventArea"))
        {
            // EventAreaに入ったら、EventAreaのGameObjectを取得(移動制限のため)
            EventArea = other.gameObject;
            isEventArea = true;
        }
        if (other.CompareTag("TalkArea"))
        {
            // 歩いている状態でTalkAreaに入ったら、会話可能
            if (playerState == PlayerState.Walking)
            {
                isTalkArea = true;
            }
        }
        if (other.CompareTag("RideArea"))
        {
            isRideArea = true;
        }
    }

    // Areaから出たときの処理
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EventArea"))
        {
            isEventArea = false;
        }
        if (other.CompareTag("TalkArea"))
        {
            isTalkArea = false;
        }
        if (other.CompareTag("RideArea"))
        {
            isRideArea = false;
        }
    }

    // 移動の入力
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    // ジャンプの入力
    public void OnJump(InputAction.CallbackContext context)
    {
        // ブースト中はジャンプできない、waking中はジャンプできない
        if (context.performed && isGrounded && !isBoosting && playerState == PlayerState.Driving)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    // Boostの入力
    public void OnBoost(InputAction.CallbackContext context)
    {
        // ボタンが押されたとき & ジャンプ中でないとき & Boostが残っているとき & walking中でないとき
        if (context.performed && isGrounded && currentBoost > 0.0f && playerState == PlayerState.Driving)
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
        // Tag EventAreaに入っていたら、自転車から降りる
        if (isEventArea)
        {
            playerState = PlayerState.Walking;
        }
        // Tag TalkAreaに入っていたら、会話開始
        if (isTalkArea)
        {
            isTalking = true;
        }
        // Tag RideAreaに入っていたら、自転車に乗る
        if (isRideArea)
        {
            playerState = PlayerState.Driving;
        }
    }
}
