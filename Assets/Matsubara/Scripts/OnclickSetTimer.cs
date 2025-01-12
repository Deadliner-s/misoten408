using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OnclickSetTimer : MonoBehaviour
{
    [System.Serializable]
    struct ButtonData
    {
        [SerializeField] private Button button;
        [SerializeField] private float timer;
        [SerializeField] private GameObject startpos;

        public Button Button => button;
        public float Timer => timer;
        public GameObject Startpos => startpos;
        
    }

    [SerializeField]
    private ButtonData[] buttonData;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(buttonData.Length);
        for (int i = 0; i < buttonData.Length; i++)
        {
            float timer = buttonData[i].Timer;
            GameObject pos = buttonData[i].Startpos;
            buttonData[i].Button.gameObject.SetActive(true);

            buttonData[i].Button.onClick.AddListener(() =>
            {
                GameManager.instance.SetTimer(timer);
                SceneTransitionManager.instance.SetPlayerPos(pos.transform.position, pos.transform.rotation);
            });

            this.gameObject.SetActive(false);
        }
    }
}

