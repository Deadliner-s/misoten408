using UnityEngine;

public class MenuController : MonoBehaviour
{
    // ���j���[��ʂ̃p�l���iUI�j
    public GameObject menuPanel;

    // �Q�[�����|�[�Y�����ǂ������Ǘ�����t���O
    private bool isPaused = false;

    void Start()
    {
        // ���j���[���\���ɂ��Ă���
        menuPanel.SetActive(false);
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
        menuPanel.SetActive(true); // ���j���[��\��
        Time.timeScale = 0f; // ���Ԃ��~�߂�i�Q�[�����|�[�Y�j
        isPaused = true;
    }

    // ���j���[����ăQ�[���ɖ߂�
    void ResumeGame()
    {
        menuPanel.SetActive(false); // ���j���[���\��
        Time.timeScale = 1f; // ���Ԃ��ĊJ����i�Q�[�����ĊJ�j
        isPaused = false;
    }
}
