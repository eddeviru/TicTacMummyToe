using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetDataPlayers : MonoBehaviour
{
    public Image Medalla;
    public Sprite[] medallero;
    public TextMeshProUGUI NamePlayerTxt;
    public TextMeshProUGUI SunPointsMonthTxt;
    public TextMeshProUGUI PositionTxt;
    public GameObject PlayerFlag;

    //Data Player
    public string namePl;
    public int sunPointM;
    public int position;
    public int sunPointT;

    public void setData()
    {
        if (position == 1)
        {
            Medalla.sprite = medallero[0];
        }
        if (position == 2)
        {
            Medalla.sprite = medallero[1];
        }
        if (position == 3)
        {
            Medalla.sprite = medallero[2];
        }
        if(position >= 4)
        {
            Medalla.sprite = medallero[3];
        }

        NamePlayerTxt.text = namePl;
        SunPointsMonthTxt.text = sunPointM.ToString() /*+ " <size=60%>" + sunPointT.ToString()*/;
        PositionTxt.text = position.ToString();
        // data total
    }

}
