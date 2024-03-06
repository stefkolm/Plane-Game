using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0f, 5f, -10f);

    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (!(target == null))
        {
            Vector3 b = target.position + offset;
            Vector3 position = Vector3.Lerp(transform.position, b, smoothSpeed);
            transform.position = position;

            Quaternion b2 = Quaternion.Euler(5f, target.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, b2, smoothSpeed * Time.deltaTime);
        }
    }
}
