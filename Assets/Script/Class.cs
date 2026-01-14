using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
}

[Serializable]
public class LeaderboardEntry
{
    public string username;
    public int bestTimeSeconds;
}