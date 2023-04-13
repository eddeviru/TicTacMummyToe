using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComienzoDuelo : MonoBehaviour
{
    public GameObject GanasteCard;
    private void Start()
    {
        GanasteCard.GetComponent<Returno>().FadeScreen(0,0);
        Invoke("destroir", 0.2f);
    }

    private void destroir()
    {
        GanasteCard.SetActive(false);
    }
}
