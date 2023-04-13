using UnityEngine;
using TMPro;

public class ControlSun : MonoBehaviour
{
    ///private Logros Ach;
    public int ScoreTotalGB;
    public int ScoreTotalPts;
    public int SceneryGB;
    public int SceneryPts;
    private float oldBugs;
    private int newBugs;
    private float oldPoints;
    private int newPoints;
    public TextMeshProUGUI GoldenBugsNbr;
    public TextMeshProUGUI PointsNbr;
    public float lerpSpeed;

    /*private void Start()
    {
        Ach = GameObject.FindGameObjectWithTag("pasaje").GetComponent<Logros>();
    }*/

    private void Update()
    {
        if (oldBugs != ScoreTotalGB)
        {
            newBugs = ScoreTotalGB;

            oldBugs = Mathf.Lerp(oldBugs, newBugs, Time.deltaTime * lerpSpeed);
            GoldenBugsNbr.text = oldBugs.ToString("F0");
            //Ach.CheckAch(newBugs);
            //Debug.Log("es " + ScoreTotalGB + " pero es " + oldBugs);
        }

        if (oldPoints != ScoreTotalPts)
        {
            newPoints = ScoreTotalPts;

            oldPoints = Mathf.Lerp(oldPoints, newPoints, Time.deltaTime * lerpSpeed);
            PointsNbr.text = oldPoints.ToString("F0");
            //Debug.Log("es " + ScoreTotalPts + " pero es " + oldPoints);
        }
    }
}
