using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTransitionTrigger : MonoBehaviour
{
    public AreaTransitionData transitionData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(transitionData.targetScene);
            // 必要であれば、遷移先でのプレイヤーのスポーン位置も設定
            // other.transform.position = transitionData.spawnPosition;
        }
    }
}
