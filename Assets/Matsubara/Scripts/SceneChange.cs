using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // ボタンを押した時にシーンを変更するメソッド
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}
