using UnityEngine;

public class DifficultyArea : MonoBehaviour
{
    public RunGameManager.DifficultyLevel difficultyLevel;
    [SerializeField] GameObject playerPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPlayerPos()
    {
        return playerPos;
    }
}
