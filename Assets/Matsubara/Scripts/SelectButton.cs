using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private GameObject button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(button);
    }

    // Update is called once per frame
    void Update()
    {
        EventSystem.current.SetSelectedGameObject(button);

    }
}
