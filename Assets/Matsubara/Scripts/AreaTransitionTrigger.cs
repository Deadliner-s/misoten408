using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTransitionTrigger : MonoBehaviour
{
    [SerializeField]
     private AreaMultiTransitionData transitionData;
    [SerializeField]
    private string AreaNo;  // éüÇÃÉGÉäÉAÇÃî‘çÜ

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
                if (transitions.SceneName == AreaNo)
                {
                    if (transitions.transform != null)
                        scene.LoadSceneAsyncPlayerSetpos(transitions.targetScene, transitions.transform);
                    else
                        scene.LoadSceneAsync(transitions.targetScene);
                }
            }
        }
    }
}
