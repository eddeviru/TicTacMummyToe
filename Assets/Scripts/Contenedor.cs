using UnityEngine;

public class Contenedor : MonoBehaviour
{
    public void caePlt()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", -22f, "speed", 2f, "islocal", true, "easetype", iTween.EaseType.easeInQuint));
    }
}
