using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTransitionTrigger : MonoBehaviour
{
    [SerializeField]
     private AreaMultiTransitionData transitionData;
    [SerializeField]
    private string AreaNo;  // 次のエリアの番号

    SceneTransitionManager scene;

    private void Start()
    {
        scene = SceneTransitionManager.instance;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(var transitions in transitionData.transitions)
            {
                if (transitions.SceneInitial == AreaNo)
                {
                    scene.LoadSceneAsyncPlayerSetpos(transitions.targetScene, transitions.transform);
                    //GameObject player = GameObject.FindGameObjectWithTag("Player");
                    //player.transform.position = transitions.transform.transform.position;
                    
                }

            }
            // 必要であれば、遷移先でのプレイヤーのスポーン位置も設定
            // other.transform.position = transitionData.spawnPosition;
        }

      
    }


}
