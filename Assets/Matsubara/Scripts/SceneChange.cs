using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // �{�^�������������ɃV�[����ύX���郁�\�b�h
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
