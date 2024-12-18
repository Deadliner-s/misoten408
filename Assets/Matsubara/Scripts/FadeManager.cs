using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static bool isFadeInstance = false;  // フェード用パネル

    [SerializeField]
    private bool isFadeIn = false;   // フェードインフラグ
    [SerializeField]
    private bool isFadeOut = false; // フェードアウトフラグ

    [SerializeField]
    private float alpha = 0.0f; // 透過率
    [SerializeField]
    private float fadeSpeed = 10.0f; // フェードに掛かる時間

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!isFadeInstance)
        {
            DontDestroyOnLoad(this);
            isFadeInstance = true;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(isFadeIn)
        {
            // フェードイン
            alpha -= Time.deltaTime / fadeSpeed;
            if(alpha <= 0.0f)   
            {
                isFadeIn = false;
                alpha = 0.0f;
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if(isFadeOut)
        {
            // フェードアウト
            alpha += Time.deltaTime / fadeSpeed;
            if(alpha >= 1.0f)
            {
                isFadeOut = false;
                alpha = 1.0f;
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }

    public void FadeIn()
    {
        isFadeIn = true;
        isFadeOut = false;
        
    }

    public void FadeOut()
    {
        isFadeIn = false;
        isFadeOut = true;
    }
}
