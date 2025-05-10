using UnityEngine;

[CreateAssetMenu(fileName ="TimeSettings", menuName = "TimeSettings")]

public class TimeService
{
    readonly 
    public float timeMultiplier = 2000;
    public float startHour = 12;
    public float sunriseHour = 6;
    public float sunsetHour = 18;

    public TimeService(TimeSettings settings)
    {

    }
}
