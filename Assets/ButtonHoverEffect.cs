using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private float scaleAmount = 1.1f;  // ��������� ���������� ������
    [SerializeField] private float animationDuration = 0.2f; // ������������ ��������

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = button.transform.localScale; // ��������� ������������ ������ ������
    }

    // ����� ��� �������� ��� ��������� �������
    public void OnPointerEnter()
    {
        button.transform.DOScale(originalScale * scaleAmount, animationDuration).SetEase(Ease.OutBack);
    }

    // ����� ��� �������� � ������������� ������� ��� �������� �������
    public void OnPointerExit()
    {
        button.transform.DOScale(originalScale, animationDuration).SetEase(Ease.InBack);
    }
}
