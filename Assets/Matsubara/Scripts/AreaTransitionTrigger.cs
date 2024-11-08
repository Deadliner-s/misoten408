using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTransitionTrigger : MonoBehaviour
{
    [SerializeField]
     private AreaMultiTransitionData transitionData;
    [SerializeField]
    private string AreaNo;  // ���̃G���A�̔ԍ�

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
            // �K�v�ł���΁A�J�ڐ�ł̃v���C���[�̃X�|�[���ʒu���ݒ�
            // other.transform.position = transitionData.spawnPosition;
        }

      
    }


}
