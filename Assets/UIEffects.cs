using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEffects : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private Color attentionColor = Color.red;
    private Color originalColor;

    private void Start()
    {
        if (targetText != null)
            originalColor = targetText.color;
    }

    /// <summary>
    /// Attention Effect - Shake and color flash. Use for lack of resources.
    /// </summary>
    public void Attention(float shakeDuration = 0.5f, float shakeStrength = 10f, int vibrato = 10, float colorFlashDuration = 0.2f, Transform target = null)
    {
        // Shake
        target = target ?? transform;
        target.transform.DOShakePosition(shakeDuration, shakeStrength, vibrato, 90, false, true);

        // Color Flash
        if (targetText != null)
        {
            targetText.DOColor(attentionColor, colorFlashDuration)
                      .OnComplete(() => targetText.DOColor(originalColor, colorFlashDuration));
        }
    }

    /// <summary>
    /// Pulse Effect - Soft scale up and down. Use for interactive hints or important buttons.
    /// </summary>
    public void Pulse(float scaleAmount = 1.1f, float duration = 0.5f)
    {
        transform.DOScale(Vector3.one * scaleAmount, duration)
                 .SetEase(Ease.InOutSine)
                 .SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// Fade Effect - Fade in and out. Use for tips or notifications.
    /// </summary>
    public void Fade(float fadeDuration = 1f, float targetAlpha = 0f)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.DOFade(targetAlpha, fadeDuration).SetLoops(2, LoopType.Yoyo);
    }

    /// <summary>
    /// Bounce Effect - Quick scale up and down. Use for button clicks.
    /// </summary>
    public void Bounce(float scaleAmount = 1.2f, float duration = 0.2f)
    {
        transform.DOScale(Vector3.one * scaleAmount, duration)
                 .SetEase(Ease.OutBack)
                 .OnComplete(() => transform.DOScale(Vector3.one, duration * 0.5f));
    }

    /// <summary>
    /// Rotate Effect - Smooth rotation loop. Use for icons or rewards.
    /// </summary>
    public void Rotate(float rotationAmount = 360f, float duration = 2f)
    {
        transform.DORotate(new Vector3(0, 0, rotationAmount), duration, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Restart);
    }
}
