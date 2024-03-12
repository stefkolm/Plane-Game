using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Properties")]
    public float speed = 5f;
    public Vector3 direction = new Vector3(-1, -1, 0);
    public float meteor_deep;

    [Header("References")]
    public AudioSource audioSource;
    public AudioClip explosionSound;
    public GameObject ExplosionParticles;
    public GameObject FlyParticles;

    private bool hitedGround = false;
    private CameraShake cameraShake;

    void Update()
    {
        Camera cam = Camera.main;
        cameraShake = cam.GetComponent<CameraShake>();
        if(!hitedGround)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.transform.CompareTag("Terrain"))
        {
            hitedGround = true;
            ExplosionParticles.SetActive(true);
            cameraShake.shakeDuration = .15f;
            audioSource.PlayOneShot(explosionSound);
            foreach (Transform child in FlyParticles.transform)
            {
                ParticleSystem particleSystem = child.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Stop();
                }
            }
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y - meteor_deep, transform.position.z);
            transform.position = targetPos;
        }
        Destroy(gameObject, 10f);

    }
}
