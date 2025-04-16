using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, - 10);
    public float smmothSpeed = 0.125f;

    public void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smmothSpeed);
        transform.position = smoothPosition;

        transform.LookAt(transform.position);
    }
}
