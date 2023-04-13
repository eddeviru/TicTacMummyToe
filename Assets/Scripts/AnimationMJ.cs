using UnityEngine;

public class AnimationMJ : MonoBehaviour
{
    private Animator AnimC;
    private string currentState;
    public string[] idleAm;
    public string putFAm;
    public string rageAm;
    public string[] dieAm;
    public string[] dieLo;
    public string[] celebAm;
    public string[] dormir;

    private void Start()
    {
        AnimC = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        AnimC.Play(newState);

        currentState = newState;
    }
}
