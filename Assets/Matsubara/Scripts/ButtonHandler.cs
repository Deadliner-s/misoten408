using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    SceneTransitionManager sceneManager;

    [SerializeField] private GameObject firstButton;    //�@�J�n���ɑI����Ԃɂ���{�^��

    private string sceneName = "Area_C";   // �ړ�����V�[����

    [SerializeField] Button[] buttons;
    private int currentIndex = 0;
    private float inputCoolDown = 0.2f;
    private float lastInputTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(SceneTransitionManager.instance != null)
            sceneManager = SceneTransitionManager.instance;
        else
            Debug.LogWarning("SceneManager instance is null!");
        if(firstButton != null)
            // �ŏ��ɑI������{�^����ݒ�
            EventSystem.current.SetSelectedGameObject(firstButton);

        
    }

    private void Update()
    {
        if (Time.time - lastInputTime < inputCoolDown)
        {
            return;

            // ���������̓���
            float verticalInput = Input.GetAxis("Vertical");

            if (verticalInput > 0.5f)
            {
                currentIndex = Mathf.Clamp(currentIndex - 1, 0, buttons.Length - 1);
                SelectButton(currentIndex);
                lastInputTime = Time.time;
            }
            else if (verticalInput < -0.5f)
            {
                currentIndex = Mathf.Clamp(currentIndex + 1, 0, buttons.Length - 1);
                SelectButton(currentIndex);
                lastInputTime = Time.time;
            }

        }


    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (firstButton != null)
            // �ŏ��ɑI������{�^����ݒ�
            EventSystem.current.SetSelectedGameObject(firstButton);

    }
    public void TitleOnButtonCllick()
    {
        if(sceneManager != null)
        {
            sceneManager.LoadSceneAsync(sceneName);
        }

    }

    public void ResultOnButtonCllick()
    {
        if (sceneManager != null)
        {
            sceneManager.LoadSceneAsync("Title");
        }

    }

    // �Q�[�����I������֐�
    public void QuitGame()
    {
        // �G�f�B�^�[�Ŏ��s���̏ꍇ�̏I������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // �r���h��̃A�v���P�[�V�������I��
        Application.Quit();
#endif
    }

    public void SelectButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(button);
    }
    public void SelectButton(int index)
    {
        EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
    }
    public void SetSceneButton(string nextSceneName)
    {
        sceneName = nextSceneName;
    }
}
