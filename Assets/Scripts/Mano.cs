using UnityEngine;

public class Mano : MonoBehaviour
{
    private float velGiro;
    private float timerSalto;
    private float timerJump;
    private float timerLimit;

    private void Start()
    {
        velGiro = Random.Range(30, 75);
        timerJump = Random.Range(0.6f, 0.8f);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * velGiro * Time.deltaTime);
        timerSalto += Time.deltaTime;
        if (timerSalto > 0.8f && timerSalto < 0.9f)
        {
            iTween.MoveTo(gameObject, iTween.Hash("y", 5, "time", timerJump, "islocal", true));
        }
        if (timerSalto > 0.9f && timerSalto < 1f)
        {
            iTween.MoveTo(gameObject, iTween.Hash("y", 6, "time", timerJump, "islocal", true));
        }
        if (timerSalto > 1f)
        {
            timerSalto = 0;
        }
    }
}
