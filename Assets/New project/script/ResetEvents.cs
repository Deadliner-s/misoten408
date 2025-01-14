using UnityEngine;

public class ResetEvents : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Scene‚ªØ‚è‘Ö‚í‚é“x‚ÉŒÄ‚Î‚ê‚é
    private void OnDisable()
    {
        if (NPCDialogueManager.instance != null)
        {
            NPCDialogueManager.instance.ResetEvents();
        }

        if (RunGameManager.instance != null)
        {
            RunGameManager.instance.ResetScore();
        }
    }
}
