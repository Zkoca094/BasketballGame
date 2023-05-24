using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smootSpeed = 0.125f;

    void FixedUpdate()
    {
        if (Manager.singleton.gameState != State.Play)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smootSpeed);
        transform.position = new Vector3(smoothPosition.x, transform.position.y, smoothPosition.z);
    }
}
