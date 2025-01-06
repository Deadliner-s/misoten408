using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // 入力
    private Vector2 input;

    // 移動速度
    public float moveSpeed = 5f;

    // 回転速度
    public float rotationSpeed = 10f;

    // カメラの参照
    public Camera mainCamera;

    // NPCと会話できるAreaに入ったかどうか
    private bool isEnterArea = false;

    // 難易度選択のAreaに入ったかどうか
    private bool isEnterDifficultyLevelArea = false;

    // InteractできるArea
    private GameObject interactArea;

    // 難易度Areaでボタンを押したか
    private bool isPressedButton = false;

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
        // 会話中または遷移開始時は移動できない
        if (NPCDialogueManager.instance.isTalking || isPressedButton)
        {
            return;
        }

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
        Vector3 moveDirection = cameraForward * input.y + cameraRight * input.x;

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

    // Areaに入ったときの処理
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RunGameNPCArea"))
        {
            isEnterArea = true;
            interactArea = other.gameObject;
        }
        else if (other.CompareTag("DifficultyLevelArea"))
        {
            isEnterDifficultyLevelArea = true;
            interactArea = other.gameObject;
        }
    }

    // Areaから出たときの処理
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RunGameNPCArea"))
        {
            isEnterArea = false;
            interactArea = null;
        }
        else if (other.CompareTag("DifficultyLevelArea"))
        {
            isEnterDifficultyLevelArea = false;
            interactArea = null;
        }
    }

    // 移動の入力
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    // 決定ボタンの入力
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // NPCと会話できるAreaに入った場合
            if (isEnterArea)
            {
                if (NPCDialogueManager.instance.isTalking != true)
                {
                    NPCDialogueManager.instance.isTalking = true;

                    // 条件を満たしている場合、壁を解除
                    interactArea.GetComponent<RunGameNPCArea>().SetUnlockedWall();

                    // 条件を満たしている場合、Coinを獲得＆itemを消費
                    interactArea.GetComponent<RunGameNPCArea>().GetCoinAndUseItem();

                    // RunGameNPCAreaのEventNameを取得して会話を開始
                    NPCDialogueManager.instance.StartEvent(interactArea.GetComponent<RunGameNPCArea>().eventName.ToString(),
                        NPCDialogueManager.instance.runtimeRunEventSetting.DataList.FirstOrDefault(data => data.eventName == interactArea.GetComponent<RunGameNPCArea>().eventName).cnt,
                        null,
                        NPCDialogueManager.instance.runtimeRunEventSetting.DataList.FirstOrDefault(data => data.eventName == interactArea.GetComponent<RunGameNPCArea>().eventName).checkPoint);
                    // 話しかけた回数を増やす
                    NPCDialogueManager.instance.runtimeRunEventSetting.DataList.FirstOrDefault(data => data.eventName == interactArea.GetComponent<RunGameNPCArea>().eventName).cnt++;

                }
                else
                    NPCDialogueManager.instance.NextDialogue();

            }
            // 難易度選択のAreaに入った場合
            else if (isEnterDifficultyLevelArea && !isPressedButton)
            {
                isPressedButton = true;

                // Home毎の難易度からの遷移
                // 難易度のObjectを取得して、その難易度に応じて遷移
                if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_初級)
                {
                    // Cの初級に遷移
                    Debug.Log("Cの初級に遷移");
                    SceneTransitionManager.instance.LoadSceneAsyncPlayerSetpos(
                        "Game1", 
                        interactArea.GetComponent<DifficultyArea>().GetPlayerPos()
                        );
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_中級)
                {
                    // Cの中級に遷移
                    Debug.Log("Cの中級に遷移");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_上級)
                {
                    // Cの上級に遷移
                    Debug.Log("Cの上級に遷移");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_初級)
                {
                    // Aの初級に遷移
                    Debug.Log("Aの初級に遷移");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_中級)
                {
                    // Aの中級に遷移
                    Debug.Log("Aの中級に遷移");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_上級)
                {
                    // Aの上級に遷移
                    Debug.Log("Aの上級に遷移");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_初級)
                {
                    // Bの初級に遷移
                    Debug.Log("Bの初級に遷移");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_中級)
                {
                    // Bの中級に遷移
                    Debug.Log("Bの中級に遷移");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_上級)
                {
                    // Bの上級に遷移
                    Debug.Log("Bの上級に遷移");
                }
            }
        }
    }
}
