using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
public class GameManager : MonoBehaviour
{
    [Header("Properties")]
    public bool MeteorShowers;

    [Header("References")]
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public GameObject meteorPrefab;
    public GameObject gameOverScreen;
    public GameObject LoseVolume;
    public GameObject ExplosionEffect;

    [Header("Debug")]
    private bool readyForEvent = true;
    private bool gameOver;
    private float weight;
    private float finalScore;

    void Start()
    {
        
    }

    void Update()
    {
        scoreText.text = player.transform.position.z.ToString("F0") + " m";
        if(player.transform.position.z > 100f && readyForEvent && MeteorShowers)
        {
            readyForEvent = false;
            StartCoroutine(MeteorShower());
        }
        if(gameOver && Input.anyKey) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        LoseVolume.SetActive(true);
        player.GetComponent<Player>().enabled = false;
        player.GetComponent<BoxCollider>().enabled = false;
        finalScore = player.transform.position.z;
        GameObject oldExplosionEffect = Instantiate(ExplosionEffect, player.transform.position, player.transform.rotation);
        player.SetActive(false);
        Destroy(oldExplosionEffect, 2f);
    }

    IEnumerator MeteorShower()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            Vector3 spawnPos = new Vector3(Random.Range(-35f, 35f), 100f, player.transform.position.z + 230f);
            GameObject oldMeteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
        }
    }
}
