using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public float reloadSpeed = 1f;
    public GameObject FireBallPrefab;
    private Animator CanonAnimator;

    void Start()
    {
        StartCoroutine(RepeatedActionCoroutine());
        CanonAnimator = GetComponent<Animator>();
    }

    IEnumerator RepeatedActionCoroutine()
    {
        while (true)
        {
            GameObject OldObject = Instantiate(FireBallPrefab, transform.position, transform.rotation);
            Destroy(OldObject, 10f);

            yield return new WaitForSeconds(reloadSpeed);
        }
    }
}
