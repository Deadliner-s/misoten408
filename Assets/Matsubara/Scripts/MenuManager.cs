using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [SerializeField] private GameObject menuUI; // ���j���[UI�p�l�����A�^�b�`����
    private GameObject menuUIInstance;

    private void Awake()
    {
        // �V���O���g���̐ݒ�F���̃I�u�W�F�N�g���B��̃C���X�^���X�ɂȂ�悤�ɂ���
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // �����̃C���X�^���X��h�����߂ɔj��
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���ԂŃI�u�W�F�N�g��ێ�����
            
            // menuUI�����݂��A�V�[���؂�ւ������ێ��������ꍇ
            if (menuUI != null && menuUIInstance == null)
            {
                menuUIInstance = Instantiate(menuUI);
                DontDestroyOnLoad(menuUIInstance); // menuUI��MenuManager�̎q�łȂ��ꍇ�ɕێ�
                menuUIInstance.SetActive(false);    // �ŏ��͔�\��
            }
        }

    }

    private void Update()
    {
        // Escape�L�[�������ꂽ�烁�j���[�̕\��/��\����؂�ւ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    // ���j���[�̕\����؂�ւ��郁�\�b�h
    public void ToggleMenu()
    {
        if(!menuUIInstance.activeSelf)
        {
            // �\��
            ShowMenu();

        }
        else
        {
            // ��\��
            HideMenu();

        }

    }

    // ���j���[��\�����郁�\�b�h
    public void ShowMenu()
    {
        if (menuUIInstance != null)
        {
            menuUIInstance.SetActive(true);
            Time.timeScale = 0f;    // �|�[�Y
        }
    }

    // ���j���[���\���ɂ��郁�\�b�h
    public void HideMenu()
    {
        if (menuUIInstance != null)
        {
            menuUIInstance.SetActive(false);
            Time.timeScale = 1f;   // �ĊJ
        }
    }
}
