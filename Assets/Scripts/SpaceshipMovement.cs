using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceshipMovement : MonoBehaviour
{
    public float speed = 20f;
    public float boostSpeed = 60f;
    private float activeSpeed;
    private float acceleration = 2f;

    bool isBoosting = false;
    bool lightsOn;

    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    public Light light1;
    public Light light2;
    void Start()
    {
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;

        Cursor.lockState = CursorLockMode.Locked;

        isBoosting = false;

    }

    void Update()
    {
        if (Input.GetButton("Jump") & isBoosting == false)
        {
            speed = boostSpeed;
            isBoosting = true;          
        }
        else
        {
            isBoosting = false;
            speed = 70f;    
        }

        if (isBoosting & lightsOn == false)
        {
            light1.intensity = 10;
            light2.intensity = 10;
            Debug.Log("Boosting");
            lightsOn = true;
        }
        else if (isBoosting==false)
        {
            light1.intensity = 1;
            light2.intensity = 1;
            lightsOn = false;
        }

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.x;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, 0f, mouseDistance.x * lookRotateSpeed * Time.deltaTime, Space.Self);

        activeSpeed = Mathf.Lerp(activeSpeed, Input.GetAxis("Vertical") * speed, acceleration * Time.deltaTime);

        transform.position += -transform.up * activeSpeed * Time.deltaTime;
    }
}
