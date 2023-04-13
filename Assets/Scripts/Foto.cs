using UnityEngine;
using UnityEngine.UI;

public class Foto : MonoBehaviour
{
    private Returno tr;
    public Image FotoaMostrar;

    private void Awake()
    {
        tr = GetComponent<Returno>();
        tr.FadeScreen(0, 0);
        gameObject.SetActive(false);
    }
}
