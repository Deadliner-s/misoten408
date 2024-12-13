using UnityEngine;
using UnityEngine.UI;


public class OnclickSetTimer : MonoBehaviour
{
    [System.Serializable]
    struct ButtonData
    {
        [SerializeField] private Button button;
        [SerializeField] private float timer;

        public Button Button => button;
        public float Timer => timer;
    }

    [SerializeField]
    private ButtonData[] buttonData;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(buttonData.Length);
        for (int i = 0; i < buttonData.Length; i++)
        {

            Debug.Log(buttonData[i].Timer);
            float timer = buttonData[i].Timer;
            buttonData[i].Button.gameObject.SetActive(true);

            buttonData[i].Button.onClick.AddListener(() =>
            {
                GameManager.instance.SetTimer(timer);
            });

            this.gameObject.SetActive(false);

        }

        // this.gameObject.SetActive(false);
    }


}

