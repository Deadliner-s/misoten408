using UnityEngine;

public class IconRotate : MonoBehaviour
{
    private GameObject player;
    private Transform playerTra;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerTra = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerTra.rotation.y);
        gameObject.transform.eulerAngles = new Vector3(270.0f, 0.0f, playerTra.localEulerAngles.y - 180.0f);
            
    }
}
