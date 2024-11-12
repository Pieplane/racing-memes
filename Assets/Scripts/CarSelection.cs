using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    public List<GameObject> carModels;
    public float rotationSpeed = 100f;
    public float autoRotationSpeed = 20f;
    public float inertiaDamping = 0.95f; // ����������� ��������� ������� (����� � 1 - ������ ���������)
    public float autoRotationThreshold = 0.1f;  // ����� �������� ��� �������� � �������������
    public float userRotationForceMultiplier = 0.1f; // ��������� ���� �������� ��� ���������� �������

    private int currentIndex = 0;
    private bool isUserRotating = false;
    private float currentRotationSpeed = 0f; // �������� ��������, ����� ������������ ��������� ����
    private Vector3 lastMousePosition; // ��������� ������� ����

    private void Start()
    {
        ShowCar(currentIndex);
        lastMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isUserRotating = true;
            currentRotationSpeed = 0f;
            // ��������� ��������� ������� ����  
            lastMousePosition = Input.mousePosition; 
        }
        if (Input.GetMouseButton(0)){
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            // ��������� �������� �������� ���� � ��������� ���� ��������
            float mouseSpeed = mouseDelta.x * userRotationForceMultiplier;
            currentRotationSpeed = mouseSpeed;

            // ��������� �������� �� ����������� � ������
            carModels[currentIndex].transform.Rotate(Vector3.up, -currentRotationSpeed * rotationSpeed * Time.deltaTime, Space.World);
            lastMousePosition = Input.mousePosition;
        }
        else if (isUserRotating)
        {
            // ��� ���������� ������ ���� ������ ���������� ��������� � ����������
            carModels[currentIndex].transform.Rotate(Vector3.up, -currentRotationSpeed * rotationSpeed * Time.deltaTime, Space.World);
            currentRotationSpeed *= inertiaDamping;

            // ���� �������� �������� ���� ������, ������������ � �������������
            if (Mathf.Abs(currentRotationSpeed) < autoRotationThreshold)
            {
                currentRotationSpeed = 0f;
                isUserRotating = false;
            }
        }
        else
        {
            // �������������, ���� ������������ �� ������� ������
            carModels[currentIndex].transform.Rotate(Vector3.up, autoRotationSpeed * Time.deltaTime, Space.World);
        }
    }
    public void NextCar()
    {
        carModels[currentIndex].SetActive(false);

        currentIndex = (currentIndex + 1) % carModels.Count;
        ShowCar(currentIndex);
    }
    public void PreviousCar()
    {
        carModels[currentIndex].SetActive(false);

        currentIndex = (currentIndex - 1 + carModels.Count) % carModels.Count;
        ShowCar(currentIndex);
    }
    private void ShowCar(int index)
    {
        carModels[index].SetActive(true);
    }
}
