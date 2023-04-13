using UnityEngine;
using UnityEngine.UI;

public class CartaGanadora : MonoBehaviour
{
    public Image Cromo;

    public void SetCromo(Sprite Ganador)
    {
        Cromo.sprite = Ganador;
    }
}
