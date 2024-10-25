using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;

    private Image image;
    private Player player;

    [SerializeField] private float maxParameter;
    [SerializeField] private float parameter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        image = gameObject.GetComponent<Image>();


        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
            maxParameter = player.maxBoost;
            image.fillAmount = maxParameter / 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject != null)
        {
            parameter = player.currentBoost;

            image.fillAmount = parameter / 100;
        }
    }
}
