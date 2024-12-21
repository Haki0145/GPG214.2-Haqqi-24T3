using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour
{
    public GameObject winPanel; 
    public TextMeshProUGUI headerText; 
    public TextMeshProUGUI playerTimeText; 
    public TextMeshProUGUI fastestTimeText; 

    private float fastestTime = float.MaxValue;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float playerTime = FindObjectOfType<Timer>().ElapsedTime;

           
            if (playerTime < fastestTime)
            {
                fastestTime = playerTime;
            }

            // Display the UI Panel
            winPanel.SetActive(true);
            headerText.text = "You Win!";
            playerTimeText.text = "Your Time: " + playerTime.ToString("F1") + " seconds";
            fastestTimeText.text = "Fastest Time: " + fastestTime.ToString("F1") + " seconds";

            // Pause the game
            Time.timeScale = 0f;
        }
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        winPanel.SetActive(false);
    }
}
