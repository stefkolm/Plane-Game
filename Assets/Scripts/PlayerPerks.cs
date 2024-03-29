using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerks : MonoBehaviour
{
    [Header("Extra Life")]
    public GameObject Heart_1;
    public GameObject Heart_2;
    private int lifeLeft = 2;


    public bool ExtraLife()
    {
        if(lifeLeft > 1)
        {
            Heart_2.GetComponent<Animator>().Play("LoseHeart");
            lifeLeft--;
            return true;
        }
        else
        {
            Heart_1.GetComponent<Animator>().Play("LoseHeart");
            lifeLeft--;
            return false;
        }
    }
}
