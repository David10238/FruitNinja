using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        IncreaseScore(0);
    }

    public void IncreaseScore(int amount = 1)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
    }
}
