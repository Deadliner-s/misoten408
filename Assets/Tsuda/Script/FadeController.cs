using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image targetImage; // フェードさせるImageコンポーネント
    [SerializeField] private float fadeDuration = 1.0f; // フェードにかかる時間

    private Coroutine fadeCoroutine; // 現在実行中のフェードコルーチン

    // フェードアウトを開始する（アルファ値を1から0に）
    public void StartFadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(1f, 0f));
    }

    // フェードインを開始する（アルファ値を0から1に）
    public void StartFadeIn()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(0f, 1f));
    }

    // フェード処理を実行するコルーチン
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        // 初期カラーを取得
        Color color = targetImage.color;
        color.a = startAlpha;
        targetImage.color = color;

        // フェード処理
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            targetImage.color = color;
            yield return null;
        }

        // 最終カラーを設定
        color.a = endAlpha;
        targetImage.color = color;
        fadeCoroutine = null;
    }
}
