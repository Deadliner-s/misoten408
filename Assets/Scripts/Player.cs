using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    private Vector2 input;                                  // 入力

    // プレイヤーの状態
    public enum PlayerState
    {
        Driving,                                            // 自転車に乗っている状態
        Walking,                                            // 歩いている状態
        Debug,                                              // デバッグ中
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

    [Header("自転車のモデル")]
    public GameObject bicycleModel;                         // 自転車のモデル
    private GameObject bicycle;                             // 自転車のGameObject

    [Header("プレイヤーの状態")]
    public PlayerState playerState = PlayerState.Driving;   // 現在のプレイヤーの状態

    private Transform cameraTransform;                      // カメラのTransform

    private bool isGrounded = true;                         // 地面にいるかどうか
    private Vector3 inputDirection;                         // 入力方向

    public float currentBoost;                              // 現在のBoost量
    [HideInInspector] public bool isBoosting = false;       // ブースト中かどうか

    private GameObject EventArea;                           // EventAreaのGameObject
    private GameObject TalkingArea;                         // TalkAreaのGameObject
    private GameObject RideArea;                            // RideAreaのGameObject
    private GameObject CircleEffect;                        // CircleEffectのGameObject
    private bool isEventArea = false;                       // EventAreaに入っているかどうか
    private bool isRideArea = false;                        // RideAreaに入っているかどうか
    private bool isTalkArea = false;                        // TalkAreaに入っているかどうか

    private GameObject road;                                // RoadのGameObject
    private Vector3 firstPos;                               // 初期位置

    [Header("デバッグ用(F1押すとクリエみたいになる、自転車乗ってる時だけ)")]
    public bool isDebug = false;                            // デバッグモードかどうか

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
                targetRotation = Quaternion.Euler(this.transform.eulerAngles.x, targetRotation.eulerAngles.y, this.transform.eulerAngles.z); // X軸の回転をゼロに設定
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, driveTurnSpeed * Time.deltaTime);

                // ブースト中の移動速度を使用
                float currentSpeed = isBoosting ? boostSpeed : driveSpeed;
                transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

                // ブースト中の場合はBoostを消費
                if (isBoosting)
                {
                    currentBoost = Mathf.Max(0, currentBoost - Time.deltaTime);

                    if (currentBoost <= 0.0f)
                    {
                        isBoosting = false;
                    }
                }
            }
        }
        else if (playerState == PlayerState.Walking)
        {
            // 会話中は移動不可
            if (NPCDialogueManager.instance.isTalking)
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
            if (isEventArea)
            {
                Vector3 pos = transform.position;
                pos.x = Mathf.Clamp(pos.x, EventArea.transform.position.x - EventArea.transform.localScale.x / 3f, EventArea.transform.position.x + EventArea.transform.localScale.x / 3f);
                pos.z = Mathf.Clamp(pos.z, EventArea.transform.position.z - EventArea.transform.localScale.z / 3f, EventArea.transform.position.z + EventArea.transform.localScale.z / 3f);
                transform.position = pos;
            }
            else
            {
                Debug.Log("isEventAreaがnullです。");
            }

            // Boostを回復(1秒間にboostRecoverySpeedずつ回復)
            currentBoost = Mathf.Min(maxBoost, currentBoost + boostRecoverySpeed * Time.deltaTime);
            if (currentBoost >= maxBoost)
            {
                currentBoost = maxBoost;
            }
        }
        // Debug用
        else if (playerState == PlayerState.Debug)
        {
            Vector3 moveDirection = (forward * input.y + right * input.x).normalized;

            if (moveDirection.sqrMagnitude > 0.01f)
            {
                // プレイヤーの向きを移動方向に向ける
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 100 * Time.deltaTime);

                // プレイヤーを移動
                transform.position += moveDirection * 100 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += Vector3.down * 50 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position += Vector3.up * 50 * Time.deltaTime;
            }
        }

        // ひっくり返らないようにする傾きを制限するX(-45度～45度),Z(0度～0度)
        Vector3 angles = transform.eulerAngles;
        if (angles.x > 180.0f)
        {
            angles.x -= 360.0f;
        }
        angles.x = Mathf.Clamp(angles.x, -45.0f, 45.0f);
        if (angles.z > 180.0f)
        {
            angles.z -= 360.0f;
        }
        angles.z = Mathf.Clamp(angles.z, 0.0f, 0.0f);
        transform.eulerAngles = angles;

        // マップ外に落ちてしまった時の処理(roadより下に落ちたら初期位置に戻す)
        if (road)
        {
            if (transform.position.y < road.transform.position.y - 1.0f)
            {
                transform.position = firstPos;
            }
        }
        else
        {
            Debug.Log("roadがnullです。");
        }

        // F1を押すとDebugモードに切り替え
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isDebug = !isDebug;
            playerState = isDebug ? PlayerState.Debug : PlayerState.Driving;
            // Gravityを無効にする
            this.GetComponent<Rigidbody>().useGravity = !isDebug;
            // Dragを無効にする
            this.GetComponent<Rigidbody>().linearDamping = isDebug ? 10 : 0;
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
            // RideAreaのタグが付いているオブジェクトを取得
            GameObject[] rideAreas = GameObject.FindGameObjectsWithTag("RideArea");
            // Playerから一番近いRideAreaを取得
            float minDistance = float.MaxValue;
            foreach (GameObject rideArea in rideAreas)
            {
                float distance = Vector3.Distance(transform.position, rideArea.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    RideArea = rideArea;
                }
            }

            // EventAreaに入ったら、EventAreaのGameObjectを取得(移動制限のため)
            EventArea = other.gameObject;
            isEventArea = true;
        }
        if (other.CompareTag("TalkArea"))
        {
            // 歩いている状態でTalkAreaに入ったら、会話可能
            if (playerState == PlayerState.Walking)
            {
                TalkingArea = other.gameObject;
                isTalkArea = true;
            }
        }
        if (other.CompareTag("RideArea"))
        {
            if (playerState == PlayerState.Walking)
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
        if (context.performed)
        {
            // Tag EventAreaに入っていたら、自転車から降りる
            if (isEventArea)
            {
                playerState = PlayerState.Walking;

                // 自転車をスポーンさせる
                if (bicycle == null)
                    SpawnBicycle();

                // EventAreaの子オブジェクトCircleを取得
                CircleEffect = EventArea.transform.Find("Circle").gameObject;
                // エフェクトを止める
                CircleEffect.GetComponent<VisualEffect>().Stop();
            }

            // Tag TalkAreaに入っていたら、会話開始
            if (isTalkArea)
            {
                if (NPCDialogueManager.instance.isTalking != true)
                {
                    NPCDialogueManager.instance.isTalking = true;

                    // TalkAreaのEventNameを取得して会話を開始
                    NPCDialogueManager.instance.StartEvent(TalkingArea.GetComponent<TalkArea>().eventName.ToString(), 
                        NPCDialogueManager.instance.runtimeEventSetting.DataList.FirstOrDefault(data => data.eventName == TalkingArea.GetComponent<TalkArea>().eventName).cnt,
                        NPCDialogueManager.instance.runtimeEventSetting.DataList.FirstOrDefault(data => data.eventName == TalkingArea.GetComponent<TalkArea>().eventName).tex,
                        NPCDialogueManager.instance.runtimeEventSetting.DataList.FirstOrDefault(data => data.eventName == TalkingArea.GetComponent<TalkArea>().eventName).checkPoint);
                    // 話しかけた回数を増やす
                    NPCDialogueManager.instance.runtimeEventSetting.DataList.FirstOrDefault(data => data.eventName == TalkingArea.GetComponent<TalkArea>().eventName).cnt++;
                }
                else
                    NPCDialogueManager.instance.NextDialogue();
            }

            // Tag RideAreaに入っていたら、自転車に乗る
            if (isRideArea && playerState == PlayerState.Walking)
            {
                // エフェクトを再生
                CircleEffect.GetComponent<VisualEffect>().Play();

                playerState = PlayerState.Driving;
                DestroyBicycle();
            }
        }
    }

    // 自転車から降りる(Walking)になった時自転車をスポーンさせる
    public void SpawnBicycle()
    {
        // RideAreaの位置(少しずらした位置)に自転車をスポーン
        bicycle = Instantiate(bicycleModel, RideArea.transform.position + new Vector3(0.2f, -0.3f, -0.40f), RideArea.transform.rotation);
    }

    // 自転車に乗る(Driving)になった時自転車を削除する
    public void DestroyBicycle()
    {
        // 自転車を削除
        Destroy(bicycle);
    }
}
