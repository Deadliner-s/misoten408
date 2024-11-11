using UnityEngine;
using UnityEngine.UI;

public class SwitchImage : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject mapChip;
   private bool displayFlag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (mapChip != null)
            mapChip.transform.localScale = new Vector3(135.0f, 2.0f, 135.0f);

        displayFlag = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            switch (displayFlag)
            {
                case false:
                    displayFlag = true;

                    if(mapChip != null)
                        mapChip.transform.localScale = new Vector3(35.0f,2.0f,35.0f);
                    break;

                case true:
                    displayFlag = false;

                    if (mapChip != null)
                        mapChip.transform.localScale = new Vector3(135.0f, 2.0f, 135.0f);
                    break;

            }
        }

        canvas.SetActive(displayFlag);
    }
}
