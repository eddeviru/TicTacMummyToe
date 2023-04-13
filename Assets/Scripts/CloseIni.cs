using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseIni : MonoBehaviour
{
    private Returno tr;

    private void Start()
    {
        tr = GetComponent<Returno>();
        tr.FadeScreen(0, 0);
        gameObject.SetActive(false);
    }
}
