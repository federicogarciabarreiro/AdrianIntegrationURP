using System.Collections;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float slowdownFactor = 0.25f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = slowdownFactor;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1f;
        }
    }
}
