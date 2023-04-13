using UnityEngine;

public class TapBrck : MonoBehaviour
{
    ForceTap fT;
    AudioSource aS;

    private void Start()
    {
        fT = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ForceTap>();
        aS = GetComponent<AudioSource>();
    }
    private void OnTouchDown()
    {
        fT.SetForce();
        aS.Play();
    }

    private void OnTouchStay()
    {
        //nada
    }


    private void OnTouchUp()
    {
        //nada
    }
}
