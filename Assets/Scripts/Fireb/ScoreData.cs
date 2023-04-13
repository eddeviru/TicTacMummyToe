public class ScoreData 
{
    public int sunPointsTotal;
    public int sunPointsMonth;
    public string NamePlayer;
    public int goldenBugs;

    public ScoreData(int sunPointsTotal, int sunPointsMonth, string NamePlayer, int goldenBugs)
    {
        this.NamePlayer = NamePlayer;
        this.sunPointsTotal = sunPointsTotal;
        this.sunPointsMonth = sunPointsMonth;
        this.goldenBugs = goldenBugs;
    }
}
