using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // ����
    private Vector2 input;

    // �ړ����x
    public float moveSpeed = 5f;

    // ��]���x
    public float rotationSpeed = 10f;

    // �J�����̎Q��
    public Camera mainCamera;

    // NPC�Ɖ�b�ł���Area�ɓ��������ǂ���
    private bool isEnterArea = false;

    // ��Փx�I����Area�ɓ��������ǂ���
    private bool isEnterDifficultyLevelArea = false;

    // Interact�ł���Area
    private GameObject interactArea;

    // ��ՓxArea�Ń{�^������������
    private bool isPressedButton = false;

    void Start()
    {
        // ���C���J�����������擾�i�w�肪�Ȃ��ꍇ�j
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // ��b���܂��͑J�ڊJ�n���͈ړ��ł��Ȃ�
        if (NPCDialogueManager.instance.isTalking || isPressedButton)
        {
            return;
        }

        // ���͕������J������ŕϊ�
        Vector3 cameraForward = mainCamera.transform.forward; // �J�����̑O����
        Vector3 cameraRight = mainCamera.transform.right;     // �J�����̉E����

        // Y�������̉e�������O
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // ���K�����ĕ����x�N�g�����擾
        cameraForward.Normalize();
        cameraRight.Normalize();

        // �ړ��������v�Z
        Vector3 moveDirection = cameraForward * input.y + cameraRight * input.x;

        // �ړ�����
        if (moveDirection.magnitude > 0.1f) // ���͂�����ꍇ�̂ݏ���
        {
            // �ړ�
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // �v���C���[�̌������ړ������ɍ��킹��
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Area�ɓ������Ƃ��̏���
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

    // Area����o���Ƃ��̏���
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

    // �ړ��̓���
    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    // ����{�^���̓���
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // NPC�Ɖ�b�ł���Area�ɓ������ꍇ
            if (isEnterArea)
            {
                if (NPCDialogueManager.instance.isTalking != true)
                {
                    NPCDialogueManager.instance.isTalking = true;

                    // �����𖞂����Ă���ꍇ�A�ǂ�����
                    interactArea.GetComponent<RunGameNPCArea>().SetUnlockedWall();

                    // �����𖞂����Ă���ꍇ�ACoin���l����item������
                    interactArea.GetComponent<RunGameNPCArea>().GetCoinAndUseItem();

                    // RunGameNPCArea��EventName���擾���ĉ�b���J�n
                    NPCDialogueManager.instance.StartEvent(interactArea.GetComponent<RunGameNPCArea>().eventName.ToString(),
                        NPCDialogueManager.instance.runtimeRunEventSetting.DataList.FirstOrDefault(data => data.eventName == interactArea.GetComponent<RunGameNPCArea>().eventName).cnt,
                        null,
                        NPCDialogueManager.instance.runtimeRunEventSetting.DataList.FirstOrDefault(data => data.eventName == interactArea.GetComponent<RunGameNPCArea>().eventName).checkPoint);
                    // �b���������񐔂𑝂₷
                    NPCDialogueManager.instance.runtimeRunEventSetting.DataList.FirstOrDefault(data => data.eventName == interactArea.GetComponent<RunGameNPCArea>().eventName).cnt++;

                }
                else
                    NPCDialogueManager.instance.NextDialogue();

            }
            // ��Փx�I����Area�ɓ������ꍇ
            else if (isEnterDifficultyLevelArea && !isPressedButton)
            {
                isPressedButton = true;

                // Home���̓�Փx����̑J��
                // ��Փx��Object���擾���āA���̓�Փx�ɉ����đJ��
                if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_����)
                {
                    // C�̏����ɑJ��
                    Debug.Log("C�̏����ɑJ��");
                    SceneTransitionManager.instance.LoadSceneAsyncPlayerSetpos(
                        "Game1", 
                        interactArea.GetComponent<DifficultyArea>().GetPlayerPos()
                        );
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_����)
                {
                    // C�̒����ɑJ��
                    Debug.Log("C�̒����ɑJ��");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.C_�㋉)
                {
                    // C�̏㋉�ɑJ��
                    Debug.Log("C�̏㋉�ɑJ��");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_����)
                {
                    // A�̏����ɑJ��
                    Debug.Log("A�̏����ɑJ��");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_����)
                {
                    // A�̒����ɑJ��
                    Debug.Log("A�̒����ɑJ��");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.A_�㋉)
                {
                    // A�̏㋉�ɑJ��
                    Debug.Log("A�̏㋉�ɑJ��");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_����)
                {
                    // B�̏����ɑJ��
                    Debug.Log("B�̏����ɑJ��");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_����)
                {
                    // B�̒����ɑJ��
                    Debug.Log("B�̒����ɑJ��");
                }
                else if (interactArea.GetComponent<DifficultyArea>().difficultyLevel == RunGameManager.DifficultyLevel.B_�㋉)
                {
                    // B�̏㋉�ɑJ��
                    Debug.Log("B�̏㋉�ɑJ��");
                }
            }
        }
    }
}
