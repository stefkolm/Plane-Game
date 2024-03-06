using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void Start()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            float offsetY = transform.GetComponent<Renderer>().bounds.size.y / 2 * 0.95f;
            Vector3 targetPosition = hit.point + Vector3.up * offsetY;
            transform.position = targetPosition;
        }
    }
}
