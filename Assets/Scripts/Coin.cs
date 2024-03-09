using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float spinSpeed = 100f;
    public float coinHeight = 2.5f;

    private void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            Vector3 targetPosition = hit.point + Vector3.up * coinHeight;
            transform.position = targetPosition;
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
    }
}
