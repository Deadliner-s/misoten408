using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image targetImage; // �t�F�[�h������Image�R���|�[�l���g
    [SerializeField] private float fadeDuration = 1.0f; // �t�F�[�h�ɂ����鎞��

    private Coroutine fadeCoroutine; // ���ݎ��s���̃t�F�[�h�R���[�`��

    // �t�F�[�h�A�E�g���J�n����i�A���t�@�l��1����0�Ɂj
    public void StartFadeOut()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(1f, 0f));
    }

    // �t�F�[�h�C�����J�n����i�A���t�@�l��0����1�Ɂj
    public void StartFadeIn()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(Fade(0f, 1f));
    }

    // �t�F�[�h���������s����R���[�`��
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        // �����J���[���擾
        Color color = targetImage.color;
        color.a = startAlpha;
        targetImage.color = color;

        // �t�F�[�h����
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            targetImage.color = color;
            yield return null;
        }

        // �ŏI�J���[��ݒ�
        color.a = endAlpha;
        targetImage.color = color;
        fadeCoroutine = null;
    }
}
