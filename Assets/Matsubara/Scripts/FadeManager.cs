using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static bool isFadeInstance = false;  // �t�F�[�h�p�p�l��

    [SerializeField]
    private bool isFadeIn = false;   // �t�F�[�h�C���t���O
    [SerializeField]
    private bool isFadeOut = false; // �t�F�[�h�A�E�g�t���O

    [SerializeField]
    private float alpha = 0.0f; // ���ߗ�
    [SerializeField]
    private float fadeSpeed = 10.0f; // �t�F�[�h�Ɋ|���鎞��

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
            // �t�F�[�h�C��
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
            // �t�F�[�h�A�E�g
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
