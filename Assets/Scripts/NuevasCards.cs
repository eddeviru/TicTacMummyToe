using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NuevasCards : MonoBehaviour
{
    public TextMeshProUGUI paraMensaje;
    public TextMeshProUGUI nuevosGold;
    private Returno tr;
    public Image iconoCard;

    private void Start()
    {
        tr = GetComponent<Returno>();
        tr.FadeScreen(0, 0);
        gameObject.SetActive(false);
    }

    public void SetTextos(string mensaje, string nuevosGolden, Sprite iCard)
    {
        paraMensaje.text = mensaje;
        nuevosGold.text = nuevosGolden;
        iconoCard.sprite = iCard;

    }
}
