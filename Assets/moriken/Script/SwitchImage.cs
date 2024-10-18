using UnityEngine;
using UnityEngine.UI;

public class SwitchImage : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    bool displayFlag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        canvas.SetActive(false);
        displayFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            switch (displayFlag)
            {
                case false:
                    displayFlag = true;
                    break;

                case true:
                    displayFlag = false;
                    break;

            }
        }

        canvas.SetActive(displayFlag);
    }
}
