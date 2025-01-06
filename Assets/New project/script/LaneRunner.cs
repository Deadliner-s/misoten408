using UnityEngine;

public class LaneRunner : MonoBehaviour
{
    // レーン情報
    private int currentLane = 1; // 0: 左, 1: 中央 (初期), 2: 右
    public float laneDistance = 2.0f; // レーン間の距離

    // キャラクターの移動速度
    public float lateralSpeed = 10.0f; // 左右移動速度
    public float forwardSpeed = 5.0f; // 前進速度
    private float originalForwardSpeed; // 元の前進速度を保存

    // ゴール関連
    private bool hasReachedGoal = false; // ゴールに到達したかどうか
    public float decelerationRate = 0.1f; // 減速の割合

    // ジャンプ関連
    public float jumpHeight = 2.0f;
    public float jumpDuration = 0.5f;
    private bool isGrounded = true;
    private float groundY; // 地面のY座標
    private float jumpTime; // ジャンプ経過時間
    private bool isJumping = false;
    private bool isMoving = false; // 移動中の判定

    // アニメーション関連
    public LaneAni laneAni;

    // 目標位置
    private Vector3 targetPosition;

    // リザルトのCanvas
    [SerializeField] private GameObject ResurtCanvas;

    void Start()
    {
        // 初期の目標位置を設定
        targetPosition = transform.position;

        // 地面のY座標を保存
        groundY = transform.position.y;

        // 元の前進速度を保存
        originalForwardSpeed = forwardSpeed;

        // プレイヤーの子オブジェクトのアニメーションを取得
        laneAni = GetComponentInChildren<LaneAni>();
    }

    void Update()
    {
        if (!hasReachedGoal)
        {
            // 入力処理 (ジャンプ中または移動中は左右移動を無効化)
            if (!isJumping && !isMoving)
            {
                if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
                {
                    currentLane--;
                    isMoving = true;
                    laneAni.LaneChangeAni(0);
                }
                else if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
                {
                    currentLane++;
                    isMoving = true;
                    laneAni.LaneChangeAni(1);
                }
            }

            // 入力処理 (ジャンプ中および移動中でない場合のみジャンプ可能)
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isMoving)
            {
                StartJump();
                laneAni.LaneChangeAni(2);
                SoundManager.instance.PlaySE("jump");
            }

            // ジャンプ処理
            if (isJumping)
            {
                HandleJump();
                //hasReachedGoal = true;
            }

            // レーン移動処理
            if (isMoving)
            {
                HandleLaneMovement();
            }

            // 前進処理
            MoveForward();
        }
        else
        {
            // ゴール到達時の減速処理
            HandleDeceleration();

            // リザルト表示
            ResurtCanvas.GetComponent<RunGameResult>().UpdateResultDisplay();
        }
    }

    void StartJump()
    {
        isGrounded = false;
        isJumping = true;
        jumpTime = 0f;
    }

    void HandleJump()
    {
        jumpTime += Time.deltaTime;
        float progress = jumpTime / jumpDuration;

        if (progress >= 1f)
        {
            // ジャンプ終了
            isJumping = false;
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            isGrounded = true;
        }
        else
        {
            // 放物線を描くジャンプの高さ計算
            float height = 4 * jumpHeight * progress * (1 - progress); // 簡易的な放物線
            transform.position = new Vector3(transform.position.x, groundY + height, transform.position.z);
        }
    }

    void HandleLaneMovement()
    {
        // 目標位置を計算
        targetPosition = new Vector3(currentLane * laneDistance - laneDistance, transform.position.y, transform.position.z);

        // 現在の位置を目標位置に近づける
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, lateralSpeed * Time.deltaTime);

        // 移動終了判定
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            isMoving = false;
        }
    }

    void MoveForward()
    {
        // 常に前進
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
    }

    void HandleDeceleration()
    {
        // 前進速度を徐々に減少
        forwardSpeed = Mathf.Max(0, forwardSpeed - decelerationRate * Time.deltaTime);

        // 停止後に前進処理を無効化
        if (forwardSpeed <= 0)
        {
            forwardSpeed = 0;
        }

        // ゴール到達後の前進処理
        transform.position += Vector3.forward * forwardSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ゴールオブジェクトに接触した場合
        if (other.CompareTag("Goal"))
        {
            hasReachedGoal = true;
        }
    }
}
