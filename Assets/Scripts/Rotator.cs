using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    private float rotationSpeed = 100.0f;



    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            float mousX = Input.GetAxis("Mouse X");
            float mousY = Input.GetAxis("Mouse Y");
            transform.Rotate(mousY * rotationSpeed, -mousX * rotationSpeed, 0, Space.World);
        }
        else if (Input.GetMouseButtonUp(1)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500))
            {
                Debug.Log("Mouse click hit!");
            }
        }


    }
}
