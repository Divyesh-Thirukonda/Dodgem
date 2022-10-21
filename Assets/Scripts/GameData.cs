using System;

[System.Serializable]
public class GameData
{
    public float _gold = 0;
    public bool _gtg;
    public bool _goodGraphics;
    public float _magnetRadius;
    public float _timeSpeed;

    public GameData(float gold, bool gtg, bool goodGraphics, float magnetRadius, float timeSpeed)
    {
        _gold = gold;
        _gtg = gtg;
        _goodGraphics = goodGraphics;
        _magnetRadius = magnetRadius;
        _timeSpeed = timeSpeed;
    }
}