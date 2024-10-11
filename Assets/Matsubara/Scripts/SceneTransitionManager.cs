using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;
    public GameObject loadingScreen;    // ���[�h��ʂ�UI�i�v���n�u�Ȃǂ��A�^�b�`�j
    public Slider progressBar;          // ���[�h���̐i����\������X���C�_�[

    private void Awake()
    {
        // �V���O���g���p�^�[���̎���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // �V�[���ԂŃI�u�W�F�N�g���j������Ȃ��悤�ɂ���
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

        // ���[�h�����������烍�[�h��ʂ��\��
        loadingScreen.SetActive(false);
    }
}
