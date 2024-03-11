using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float edgeOffset = 30f;
    float cameraSpeed = 30f;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x >= Screen.width -edgeOffset)
        {
            // Move the camera
            pos += Vector3.right * Time.deltaTime * cameraSpeed;
            pos.x = Mathf.Clamp(pos.x, -69, 66);
            transform.position = pos;
        }
        else if(Input.mousePosition.x <= edgeOffset){
            pos += -Vector3.right * Time.deltaTime * cameraSpeed;
            pos.x = Mathf.Clamp(pos.x, -69, 66);
            transform.position = pos;
        }
    }

}
