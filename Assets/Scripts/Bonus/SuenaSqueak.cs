using UnityEngine;

public class SuenaSqueak : MonoBehaviour
{
    private AudioSource bafle;
    public float wRange = 2f;
    public bool canShot;
    private float timerRay;
    private ForceTap fT;
    private float timerSound;

    void Start()
    {
        bafle = GetComponent<AudioSource>();
        fT = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ForceTap>();
    }

    private void Update()
    {
        if (canShot)
        {
            timerSound += Time.deltaTime;
            
            Vector3 direction = Vector3.right;
            Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * wRange));
            Debug.DrawRay(transform.position, transform.TransformDirection(direction * wRange));

            if (timerSound < 3f)
            {
                timerRay += Time.deltaTime;
            }

            if (Physics.Raycast(theRay, out RaycastHit hit, wRange))
            {
                if (hit.collider.tag == "Player" && timerRay > 0.2f)
                {
                    SetSound();
                }
            }
        }
    }

    private void SetSound()
    {
        bafle.Play();
        fT.ShakeCamera(0.2f, 0.3f);
        timerRay = 0;
    }
}