using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D");
            GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().ChangeStage(1);
            SceneManager.LoadScene("Stage2");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
            GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().ChangeStage(0);
            SceneManager.LoadScene("Stage1");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S");
            GameObject.Find("GameCheckPointManager").GetComponent<GameCheckPointManager>().ChangeFlag(0, 0, true);
        }
    }
}
