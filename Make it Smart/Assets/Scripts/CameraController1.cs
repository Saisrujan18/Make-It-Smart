using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{

    public float panSpeed;

    public Camera mainCamera;

    public float maxX;
    public float maxY;
    public float minX;
    public float minY;

    void Start()
    {
        //mainCamera = GetComponent<Camera>();
        //mainCamera.transform.rotation = Quaternion.Euler(40, 0, 0);

    }

    public void Update()
    {

        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {

            Vector3 rightMovement = Vector3.right * panSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 upMovement = Vector3.up * panSpeed * Time.deltaTime * Input.GetAxis("Vertical");


            Vector3 newPosition = mainCamera.transform.position + rightMovement + upMovement;
            //mainCamera.transform.position += upMovement;

            if (newPosition.x > maxX)
            {

                newPosition.x = maxX;

            }

            if (newPosition.x < minX)
            {

                newPosition.x = minX;

            }

            if (newPosition.y > maxY)
            {

                newPosition.y = maxY;

            }

            if (newPosition.y < minY)
            {

                newPosition.y = minY;

            }

            mainCamera.transform.position = newPosition;
            
        }

    }

}