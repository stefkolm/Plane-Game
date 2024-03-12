using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float gravitationalConstant = 10f;
    private bool hitedTrigger;
    private bool inHoleZone;
    private Rigidbody rb;
    private Transform player;
    private PlayerCamera playerCamera;
    public Transform NewCameraPos;

    private void Start()
    {
        playerCamera = GameObject.Find("Camera").GetComponent<PlayerCamera>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = player.GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            hitedTrigger = true;
            inHoleZone = false;
            player.gameObject.SetActive(false);
            playerCamera.target = null;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 75f && !hitedTrigger)
        {
            inHoleZone = true;
            player.GetComponent<Player>().enabled = false;
        }

        if(hitedTrigger)
        {
            Transform cameraPos = playerCamera.GetComponent<Transform>();
            Vector3 newPosition = Vector3.Lerp(cameraPos.position, NewCameraPos.position, Time.deltaTime * 5f);
            cameraPos.position = newPosition;
            cameraPos.LookAt(transform.position);
        }

        if (inHoleZone) 
        {
            Vector3 directionToCenter = transform.position - player.transform.position;

            rb.AddForce(directionToCenter * gravitationalConstant * Time.deltaTime);
            if (Vector3.Distance(transform.position, player.transform.position) > 80f)
            {
                rb.AddForce(Vector3.left * gravitationalConstant * Time.deltaTime);
            }
            Quaternion targetRotation = Quaternion.LookRotation(-directionToCenter, Vector3.up);
            player.GetChild(0).transform.rotation = Quaternion.Slerp(player.GetChild(0).transform.rotation, targetRotation, Time.deltaTime * 1f);
        }
        
    }
}
