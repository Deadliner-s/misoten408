using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;   // ゲージの値を持っているオブジェクト

    private Image image;    // 変更する画像
    private Player player;  // ゲージの値を保持しているスクリプト

    private float maxParameter;     // ゲージの最大値
    private float parameter;        // 現在のゲージの値

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        image = gameObject.GetComponent<Image>();   // ゲームオブジェクトのImageを取得

        playerObject = GameObject.FindGameObjectWithTag("Player");  // Playerタグがついているオブジェクトを取得
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Player>();   // PlayerタグがついているオブジェクトからPlayerスクリプトを取得
            image.fillAmount = player.maxBoost / 100;   // FillAmountにゲージの最大値を代入
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject != null)
        {
            image.fillAmount = player.currentBoost / 100;   // 現在のゲージの値を取得し、FillAmountに代入
        }
    }
}
