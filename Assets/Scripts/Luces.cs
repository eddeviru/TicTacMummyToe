using UnityEngine;

public class Luces : MonoBehaviour
{
    public Light Antorcha;
    public float tiempo;
    private float cambioLimite;
    public bool HaveLight;
    public GameObject laLu;
    private float luzBaseSize;

    private void Start()
    {
        luzBaseSize = 80f;

        if (HaveLight)
        {
            laLu.SetActive(true);
        }
    }

    private void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo > cambioLimite)
        {
            cambioLimite = Random.Range(1f, 1.7f);
            luzBaseSize = Random.Range(60f, 150f);
            if (laLu != null)
            {
                float sizeF = Random.Range(0.6f, 0.9f);
                iTween.ScaleTo(laLu, iTween.Hash("y", sizeF, "time", cambioLimite, "islocal", true));
            }
            tiempo = 0;
        }
        Antorcha.intensity = luzBaseSize + Mathf.Sin(Time.time * 8f) * luzBaseSize / 8f;
    }
}
