using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    SceneTransitionManager sceneManager;

    [SerializeField] private GameObject firstButton;


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
    
    public void TitleOnButtonCllick()
    {
        if(sceneManager != null)
        {
            sceneManager.LoadSceneAsync("Area_C");
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
}
