using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    [Header("Properties")]
    public float rotationSpeed = 5f;
    public float maxRotation = 20f;
    public float maxAngle = 30f;
    public float moveSpeed = 5f;
    public float flyHeight = 2f;
    public float heightSpeed = 5f;
    private float maxDistance = 10f;
    public LayerMask groundLayer;

    private float lastCoinPickupTime;
    float pitch = 1f;

    [Header("References")]
    public GameManager gameManager;
    public GameObject ChunkPrefab;  
    public AudioSource audioSource;
    public AudioClip coinPickSound;

    void Update()
    {
        HandleMovement();
    }

    private void FixedUpdate()
    {
        handleFlyHeight();
    }

    private void HandleMovement()
    {
        float axis = Input.GetAxis("Horizontal");
        Vector3 vector = transform.forward * moveSpeed * Time.deltaTime;
        transform.position += vector;

        float num = -axis * maxRotation;
        Quaternion b = Quaternion.Euler(0f, -num, -maxAngle * axis);
        transform.rotation = Quaternion.Lerp(transform.rotation, b, rotationSpeed * Time.deltaTime);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0f;
        transform.rotation = Quaternion.Euler(eulerAngles);
    }

    private void handleFlyHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxDistance, groundLayer))
        {
            if (hit.distance < flyHeight)
            {
                float TargetY = flyHeight - hit.distance;
                Vector3 targetPosition = new Vector3(transform.position.x, TargetY + transform.position.y, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, targetPosition, heightSpeed * Time.deltaTime);
            }
            else if(hit.distance > flyHeight)
            {
                float TargetY = flyHeight - hit.distance;
                Vector3 targetPosition = new Vector3(transform.position.x, TargetY + transform.position.y, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, targetPosition, (heightSpeed / 2) * Time.deltaTime);
            }
        }
    }

    private void CollectCoin(Collider other)
    {
        float timeSinceLastCoinPickup = Time.time - lastCoinPickupTime;
        if (timeSinceLastCoinPickup >= 1f)
        {
            pitch = 1f;
        }
        else
        {
            pitch += 0.1f;
        }
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(coinPickSound);
        Destroy(other.gameObject);

        lastCoinPickupTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ChunkTrigger"))
        {
            GameObject parent = other.transform.parent.gameObject;
            Vector3 targetPos = new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z + 829f);
            float targetRotation;
            float targetTriggerPos;
            if (parent.transform.rotation.z == 0f)
            {
                targetRotation = 0f;
                targetTriggerPos = -193.6f;
            }
            else
            {
                targetRotation = 180f;
                targetTriggerPos = 193.6f;
            }
            GameObject NewChunk = Instantiate(ChunkPrefab, targetPos, Quaternion.Euler(-89.98f, 0, targetRotation));
            NewChunk.transform.GetChild(0).transform.localPosition = new Vector3(0f, targetTriggerPos, 19.8f);
            Destroy(parent.gameObject, 5f);
        }

        if(other.CompareTag("Coin"))
        {
            CollectCoin(other);
        }
    }

}
