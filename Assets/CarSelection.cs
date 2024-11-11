using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    public List<GameObject> carModels;
    public float rotationSpeed = 100f;
    public float autoRotationSpeed = 20f;

    private int currentIndex = 0;
    private bool isUserRotating = false;

    private void Start()
    {
        ShowCar(currentIndex);
    }
    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            isUserRotating = true;

            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            carModels[currentIndex].transform.Rotate(Vector3.up, -horizontalRotation, Space.World);
        }
        else
        {
            if (isUserRotating)
            {
                Invoke("ResumeAutoRotation", 0.1f);
            }
            else
            {
                carModels[currentIndex].transform.Rotate(Vector3.up, autoRotationSpeed * Time.deltaTime);
            }
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
    private void ResumeAutoRotation()
    {
        isUserRotating = false;
    }
}
