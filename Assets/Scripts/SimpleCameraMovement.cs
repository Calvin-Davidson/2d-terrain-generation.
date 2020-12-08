using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleCameraMovement : MonoBehaviour
{

    public float speed;
    [SerializeField] private UnityEvent cameraMoveEvent = new UnityEvent();

    void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.y = Input.GetAxis("Vertical");

        if (dir != Vector3.zero)
        {
            transform.position += dir * (Time.deltaTime * speed);
            cameraMoveEvent.Invoke();
        }
    }
}
