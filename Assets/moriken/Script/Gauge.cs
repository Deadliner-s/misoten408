using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    Image image;
    [SerializeField] float maxPrameter;
    [SerializeField] float prameter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = prameter / maxPrameter;
    }
}
