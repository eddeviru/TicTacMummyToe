using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Returno : MonoBehaviour
{
    public bool Accion;
    public void FadeScreen(float alphaF, float alphaT)
    {
        Image[] img = GetComponentsInChildren<Image>();
        TextMeshProUGUI[] txt = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (Image im in img)
            im.CrossFadeAlpha(alphaF, alphaT, false);

        foreach (TextMeshProUGUI txts in txt)
            txts.CrossFadeAlpha(alphaF, alphaT, false);
    }

    public void destroir()
    {
        gameObject.SetActive(false);
    }

    public void Retorno()
    {
        FadeScreen(0, 0.8f);
        Invoke("destroir", 0.8f);
    }

    public void Comenzar()
    {
        FadeScreen(1, 0.8f);
        Accion = true;
    }
}