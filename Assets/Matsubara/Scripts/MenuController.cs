using UnityEngine;

public class MenuController : MonoBehaviour
{
    // ���j���[��ʂ̃p�l���iUI�j
    public GameObject menuPanel;
    
    // ���j���[�̃N���[���̃C���X�^���X
    private GameObject instance;

    // �Q�[�����|�[�Y�����ǂ������Ǘ�����t���O
    private bool isPaused = false;

    void Start()
    {
        // ���j���[����
        if (!instance)
        {
            instance = Instantiate(menuPanel);
            instance.SetActive(false);
            
        }

    }

    void Update()
    {
        // Escape�L�[�������ꂽ�烁�j���[�̕\��/��\����؂�ւ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // ���j���[���\������Ă���ꍇ�̓Q�[���ɖ߂�
                
            }
            else
            {
                PauseGame(); // ���j���[���\������Ă��Ȃ��ꍇ�̓��j���[���J��
            }
        }

    }

    // �Q�[�����|�[�Y���ă��j���[��\������
    void PauseGame()
    {
        if(instance)    instance.SetActive(true);   // ���j���[�\��

        Time.timeScale = 0f; // ���Ԃ��~�߂�i�Q�[�����|�[�Y�j
        isPaused = true;
    }

    // ���j���[����ăQ�[���ɖ߂�
    void ResumeGame()
    {
        if(instance)    instance.SetActive(false);  // ���j���[��\��
            
        Time.timeScale = 1f; // ���Ԃ��ĊJ����i�Q�[�����ĊJ�j
        isPaused = false;
    }
}
