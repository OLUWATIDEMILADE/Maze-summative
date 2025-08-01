using UnityEngine;
using System;

public class AnalogClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    void Start()
    {
        UpdateClock();
        InvokeRepeating(nameof(UpdateClock), 0, 1f);
    }

    void UpdateClock()
    {
        DateTime time = DateTime.Now;

        float seconds = time.Second + time.Millisecond / 1000f;
        float minutes = time.Minute + seconds / 60f;
        float hours = (time.Hour % 12) + minutes / 60f;

        float secondAngle = -seconds * 6f; // 360° / 60 seconds
        float minuteAngle = -minutes * 6f; // 360° / 60 minutes
        float hourAngle = -hours * 30f;  // 360° / 12 hours

        // Rotate on Y-axis
        iTween.RotateTo(secondHand.gameObject, iTween.Hash("rotation", new Vector3(0, secondAngle, 0), "time", 0.5f, "islocal", true));
        iTween.RotateTo(minuteHand.gameObject, iTween.Hash("rotation", new Vector3(0, minuteAngle, 0), "time", 0.5f, "islocal", true));
        iTween.RotateTo(hourHand.gameObject, iTween.Hash("rotation", new Vector3(0, hourAngle, 0), "time", 0.5f, "islocal", true));
    }
}
