using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    public GameObject loadingScreen;    // ���[�h��ʂ�UI�i�v���n�u�Ȃǂ��A�^�b�`�j
    public Slider progressBar;          // ���[�h���̐i����\������X���C�_�[   

    [SerializeField]
    private Vector3 playerStartPos = new Vector3(500.0f, 100.0f, 0.0f);
    private GameCheckPointManager gameCheckPointManager;

    private void Awake()
    {
        // �V���O���g���p�^�[���̎���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // �V�[���ԂŃI�u�W�F�N�g���j������Ȃ��悤�ɂ���

            // GameCheckPointManager�擾
            gameCheckPointManager =  GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �V�[���𑦍��ɑJ�ڂ�����i�����I�Ɂj
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // �V�[����񓯊��Ƀ��[�h���A���[�h��ʂ�\������
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }
    public void LoadSceneAsyncPlayerSetpos(string sceneName, GameObject SpawnPos)
    {
        playerStartPos = SpawnPos.transform.position;
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));

    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        // ���[�h��ʂ�\��
        loadingScreen.SetActive(true);

        // �V�[����񓯊��Ń��[�h
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
  
        // ���[�h���̐i�����X�V
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;  // �i���o�[�̒l���X�V
            yield return null;
        }

        // ���̃V�[�����i�[
        gameCheckPointManager.ChangeStage(sceneName);

        // ���[�h�����������烍�[�h��ʂ��\��
        loadingScreen.SetActive(false);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            player.transform.position = playerStartPos;
        }
    }

}
