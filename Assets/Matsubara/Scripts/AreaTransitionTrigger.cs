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
            // �K�v�ł���΁A�J�ڐ�ł̃v���C���[�̃X�|�[���ʒu���ݒ�
            // other.transform.position = transitionData.spawnPosition;
        }
    }
}
