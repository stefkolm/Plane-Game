using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public GameObject meteorPrefab;
    private bool readyForEvent = true;

    void Start()
    {
        
    }

    void Update()
    {
        scoreText.text = player.transform.position.z.ToString("F0") + " m";
        if(player.transform.position.z > 100f && readyForEvent)
        {
            readyForEvent = false;
            StartCoroutine(MeteorShower());
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator MeteorShower()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            Vector3 spawnPos = new Vector3(Random.Range(-35f, 35f), 100f, player.transform.position.z + 280f);
            GameObject oldMeteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
        }
    }
}
