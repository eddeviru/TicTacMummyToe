using System;
using System.Collections.Generic;

public class PlayerPos: IComparable<PlayerPos>
{
    public string NamePlayer;
    public int sunPointsMonth;
    public int sunPointsTotal;

    public PlayerPos(string playerName, int sunPointMonth, int sunPointTotal)
    {
        this.NamePlayer = playerName;
        this.sunPointsMonth = sunPointMonth;
        this.sunPointsTotal = sunPointTotal;
    }

    public PlayerPos(IDictionary<string, object> dict)
    {
        this.NamePlayer = dict["NamePlayer"].ToString();
        this.sunPointsMonth = Convert.ToInt32(dict["sunPointsMonth"]);
        this.sunPointsTotal = Convert.ToInt32(dict["sunPointsTotal"]);
    }

    public int CompareTo(PlayerPos other)
    {
        if(other != null)
        {
            return 1;
        }

        return sunPointsMonth - other.sunPointsMonth;
    }
}
