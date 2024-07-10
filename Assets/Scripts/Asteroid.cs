using UnityEngine;
using UnityEngine.UIElements;

public class Asteroid : MonoBehaviour
{
    public int size = 3;

    public GameManager gameManager;

    private void Start()
    {
        // Scale based on the size.
        transform.localScale = 0.5f * size * Vector3.one;

        // Add movement, bigger asteroids are slower.
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rigidbody.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);

        // Register creation.
        gameManager.asteroidCount++;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Asteroid are only destroyed with bullets.
        if (collision.CompareTag("Bullet")) {
            // Register the destruction with the game manager.
            gameManager.asteroidCount--;

            // Destroy the bullet.
            Destroy(collision.gameObject);

            // If size > 1, spawn 2 smaller asteroids of size minus 1.
            if (size > 1) {
                for (int i = 0; i < 2; i++) {
                    Asteroid newAsteroid = Instantiate(this, transform.position, Quaternion.identity);
                    newAsteroid.size = size - 1;
                    newAsteroid.gameManager = gameManager;
                }
            }

            // Destroy this asteroid.
            Destroy(gameObject);
        }
    }
}
