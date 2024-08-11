using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumibles : MonoBehaviour
{
    public bool givesLive = false;
    public int points = 100;
    public bool isRespawnable = true;

    public void consume()
    {
        gameObject.SetActive(false);
    }
}
