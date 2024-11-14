using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private float scaleAmount = 1.1f;  // Насколько увеличится кнопка
    [SerializeField] private float animationDuration = 0.2f; // Длительность анимации

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = button.transform.localScale; // Сохраняем оригинальный размер кнопки
    }

    // Метод для анимации при наведении курсора
    public void OnPointerEnter()
    {
        button.transform.DOScale(originalScale * scaleAmount, animationDuration).SetEase(Ease.OutBack);
    }

    // Метод для возврата к оригинальному размеру при убирании курсора
    public void OnPointerExit()
    {
        button.transform.DOScale(originalScale, animationDuration).SetEase(Ease.InBack);
    }
}
