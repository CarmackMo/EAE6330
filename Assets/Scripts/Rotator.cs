using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    private float rotationSpeed = 100.0f;




    private void OnMouseDrag()
    {
        float mousX = Input.GetAxis("Mouse X");
        float mousY = Input.GetAxis("Mouse Y");
        transform.Rotate(mousY * rotationSpeed, -mousX * rotationSpeed, 0, Space.World);


        Debug.LogWarning("Get Mouse Button");
    }



    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {

        }
        


    }
}
