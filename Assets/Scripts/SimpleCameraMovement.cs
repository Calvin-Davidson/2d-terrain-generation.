using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraMovement : MonoBehaviour
{

    public float speed;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3();

        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");

        transform.position += dir * (Time.deltaTime * speed);
    }
}
