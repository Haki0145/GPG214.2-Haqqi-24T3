using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    private float elapsedTime;
    public float ElapsedTime => elapsedTime;


    void Update()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60); 
        float seconds = elapsedTime % 60;                

        timerText.text = string.Format("{0:00}:{1:00.0}", minutes, seconds); 
    }
}
