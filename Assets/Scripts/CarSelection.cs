using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    public List<GameObject> carModels;
    public float rotationSpeed = 100f;
    public float autoRotationSpeed = 20f;
    public float inertiaDamping = 0.95f; // Коэффициент затухания инерции (ближе к 1 - меньше затухание)
    public float autoRotationThreshold = 0.1f;  // Порог скорости для возврата к автопрокрутке
    public float userRotationForceMultiplier = 0.1f; // Множитель силы вращения для увеличения эффекта

    private int currentIndex = 0;
    private bool isUserRotating = false;
    private float currentRotationSpeed = 0f; // Скорость вращения, когда пользователь отпускает мышь
    private Vector3 lastMousePosition; // Последняя позиция мыши

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
            // Обновляем последнюю позицию мыши  
            lastMousePosition = Input.mousePosition; 
        }
        if (Input.GetMouseButton(0)){
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            // Вычисляем скорость движения мыши и добавляем силу вращения
            float mouseSpeed = mouseDelta.x * userRotationForceMultiplier;
            currentRotationSpeed = mouseSpeed;

            // Применяем вращение по горизонтали к модели
            carModels[currentIndex].transform.Rotate(Vector3.up, -currentRotationSpeed * rotationSpeed * Time.deltaTime, Space.World);
            lastMousePosition = Input.mousePosition;
        }
        else if (isUserRotating)
        {
            // При отпускании кнопки мыши модель продолжает вращаться с затуханием
            carModels[currentIndex].transform.Rotate(Vector3.up, -currentRotationSpeed * rotationSpeed * Time.deltaTime, Space.World);
            currentRotationSpeed *= inertiaDamping;

            // Если скорость вращения ниже порога, возвращаемся к автопрокрутке
            if (Mathf.Abs(currentRotationSpeed) < autoRotationThreshold)
            {
                currentRotationSpeed = 0f;
                isUserRotating = false;
            }
        }
        else
        {
            // Автопрокрутка, если пользователь не вращает модель
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
