using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeaderBoard 
    {
    public List<string> leaderboardName = new List<string>(5);
    public List<int> leaderboardScore = new List<int>(5);
    public int score;
}
