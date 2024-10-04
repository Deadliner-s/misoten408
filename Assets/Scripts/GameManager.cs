using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager�̃C���X�^���X
    public static GameManager instance;

    private void Awake()
    {
        // �C���X�^���X�����݂��Ȃ��ꍇ
        if (instance == null)
        {
            instance = this;                // ���̃C���X�^���X����
            DontDestroyOnLoad(gameObject);  // �V�[���J�ڎ��ɔj������Ȃ��悤�ɐݒ�
        }
        // �C���X�^���X�����݂���ꍇ
        else
        {
            Destroy(gameObject); // ���̃C���X�^���X��j��
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // FPS�Œ�
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
