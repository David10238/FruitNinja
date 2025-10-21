using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image fadeImage;
    
    private int score;

    private Blade blade;
    private Spawner spawner;

    private void Start()
    {
        blade = FindFirstObjectByType<Blade>();
        spawner = FindFirstObjectByType<Spawner>();

        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        IncreaseScore(0);

        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        ClearScene();
    }

    private void ClearScene()
    {
        foreach (var fruit in FindObjectsOfType<Fruit>())
        {
            Destroy(fruit.gameObject);
        }

        foreach (var bomb in FindObjectsOfType<Bomb>())
        {
            Destroy(bomb.gameObject);
        }
    }

    public void IncreaseScore(int amount = 1)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
    }

    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();

        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
}
