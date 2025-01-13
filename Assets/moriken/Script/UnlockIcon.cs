using UnityEngine;
using UnityEngine.UI;

public class UnlockIcon : MonoBehaviour
{
    private bool unlockFlag;
    [Header("‰ğ•ú‚µ‚½‚¢‚à‚Ì‚Ì•Ï”–¼")][SerializeField] string flagName;

    private SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Unlock();

        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Unlock();

        if (unlockFlag)
        {
            sprite.enabled = true;
        }
    }

    private  void Unlock()
    {
        // •Ï”‚É“ü‚Á‚Ä‚¢‚é–¼‘O‚É‚æ‚Á‚ÄØ‚è‘Ö‚¦
        switch (flagName)
        {
            // 
            case "A_Home":
                unlockFlag = RunGameManager.instance.A_Home;
                break;
            case "A_intermediate":
                unlockFlag = RunGameManager.instance.A_intermediate;
                break;
            case "A_Advanced":
                unlockFlag = RunGameManager.instance.A_Advanced;
                break;
            case "B_Home":
                unlockFlag = RunGameManager.instance.B_Home;
                break;
            case "B_intermediate":
                unlockFlag = RunGameManager.instance.B_intermediate;
                break;
            case "B_Advanced":
                unlockFlag = RunGameManager.instance.B_Advanced;
                break;
            case "C_intermediate":
                unlockFlag = RunGameManager.instance.C_intermediate;
                break;
            case "C_Advanced":
                unlockFlag = RunGameManager.instance.C_Advanced;
                break;

            default:
                unlockFlag = true;
                break;
        }
    }
}
