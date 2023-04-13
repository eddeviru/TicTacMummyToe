using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NuevosGolden : MonoBehaviour
{
    public TextMeshProUGUI paraMensaje;
    public TextMeshProUGUI nuevosGold;
    private Returno tr;
    public GameObject[] cartas;
    public GameObject textCards;
    public Image cartaEl;

    public void SetTextos(string mensaje, string nuevosGolden)
    {
        paraMensaje.text = mensaje;
        nuevosGold.text = nuevosGolden;

        for (int i = 0; i < cartas.Length; i++)
        {
            if (cartas[i].activeSelf)
            {
                textCards.SetActive(true);
            }
            if (!cartas[i].activeSelf)
            {
                textCards.SetActive(false);
            }
        }
    }
}
