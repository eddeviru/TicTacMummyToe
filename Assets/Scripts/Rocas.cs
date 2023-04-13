using UnityEngine;

public class Rocas : MonoBehaviour
{
    private float timerRock;
    private Vector3 inScale;
    private float timeScale;

    private void Start()
    {
        inScale = gameObject.transform.localScale;
        timeScale = Random.Range(0.5f, 1.5f);
        SetPosition();
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -26f)
        {
            SetPosition();
        }

        timerRock += Time.deltaTime;
        if (timerRock >= 5f)
        {
            iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(0, 0, 0), "time", timeScale, "easetype", iTween.EaseType.easeInElastic));
            if (gameObject.transform.localScale.x == 0)
            {
                SetPosition();
            }
        }
    }

    public void SetPosition()
    {
        gameObject.transform.position = new Vector3(Random.Range(-13.49f, 11.7f), Random.Range(27f, 38f), Random.Range(-12.16f, 12.51f));
        gameObject.transform.localScale = inScale;
        timerRock = 0;
        Destroir();
    }

    public void Destroir()
    {
        gameObject.SetActive(false);
    }
}
