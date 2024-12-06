using UnityEngine;

public class GameManager : MonoBehaviour
{
    // GameManager�̃C���X�^���X
    public static GameManager instance;

    // ��������
    public float timeLimit = 180f;

    // ����
    public float time = 0f;

    // �Q�[�������ǂ���
    private bool onGame = false;

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
        // �Q�[�����̏ꍇ
        if (onGame)
        {
            // ���Ԃ����炷
            time -= Time.deltaTime;

            // ���Ԃ�0�ȉ��̏ꍇ
            if (time <= 0)
            {
                time = 0;
                SceneTransitionManager.instance.LoadSceneAsync("Result");
                onGame = false;
            }
        }
    }

    // �Q�[���J�n
    public void StartGame()
    {
        onGame = true;
        time = timeLimit;
    }

    // �Q�[���I��
    public void EndGame()
    {
        onGame = false;
    }

    // �Q�[�������ǂ�����Ԃ�
    public bool IsOnGame()
    {
        return onGame;
    }

    public void SetTimer(float t)
    {
        timeLimit = t;
    }

    
}

