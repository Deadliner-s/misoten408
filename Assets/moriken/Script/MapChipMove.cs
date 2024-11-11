using UnityEngine;

public class MapChipMove : MonoBehaviour
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
    void FixedUpdate()
    {
        gameObject.transform.position  = new Vector3(playerTra.position.x, gameObject.transform.position.y, playerTra.position.z);
    }
}
