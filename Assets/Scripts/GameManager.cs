using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] public TextMeshProUGUI scoreText;
    public int asteroidCount = 0;
    private int level = 0;

    public int score = 0;
    public int highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    private void Update()
    {
        // if there are no asteroid left, spawn more!
        if (asteroidCount == 0) {
            // Increase the level.
            level++;

            // Spawn the correct number for this level.
            // 1=>4, 2=>6, 3=>8, 4=>10,...
            int numAsteroids = 0 + (2 * level);
            for (int i = 0; i < numAsteroids; i++) {
                SpawnAsteroid();
            }
        }

        // Display score
        scoreText.text = score.ToString();
    }

    private void SpawnAsteroid()
    {
        // Position from the edge.
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;

        // Select edge to spawn from.
        int edge = Random.Range(0, 4);
        if (edge == 0) {
            viewportSpawnPosition = new Vector2(offset, 0);
        } else if (edge == 1) {
            viewportSpawnPosition = new Vector2(offset, 1);
        } else if (edge == 2) {
            viewportSpawnPosition = new Vector2(0, offset);
        } else if (edge == 3) {
            viewportSpawnPosition = new Vector2(1, offset);
        }

        // Create asteroid.
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        Asteroid asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        asteroid.gameManager = this;
    }

    public void GameOver()
    {
        //StartCoroutine(Restart());

        // Set score
        PlayerPrefs.SetInt("CurrentScore", score);

        if(score > highScore) {
            PlayerPrefs.SetInt("HighScore", score);
        }

        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator Restart()
    {
        Debug.Log("Game Over");

        // Wait a bit before restarting.
        yield return new WaitForSeconds(2f);

        // Restart scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        yield return null;
    }
}
