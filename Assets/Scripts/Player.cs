using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Ship parameters")]
    [SerializeField] private float shipAcceleration = 1f;
    [SerializeField] private float shipMaxVelocity = 8f;
    [SerializeField] private float shipRotationSpeed = 200f;
    [SerializeField] private float bulletSpeed = 8f;

    [Header("Object references")]
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private ParticleSystem destroyedParticles;
    [SerializeField] public ParticleSystem thrustEffect;
    [SerializeField] public ParticleSystem explosionEffect;

    private Rigidbody2D shipRigidbody;
    private bool isAlive = true;
    private bool isAccelerating = false;

    // Start is called before the first frame update
    private void Start()
    {
        // Get a reference to the attached RigidBody2D
        shipRigidbody = GetComponent<Rigidbody2D>();
        
        // Reset score
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive) {
            HandleShipAcceleration();
            HandleShipRotation();
            HandleShooting();
        } else {
            HandleDeathEffects();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive && isAccelerating) {
            shipRigidbody.AddForce(shipAcceleration * transform.up);
            shipRigidbody.velocity = Vector2.ClampMagnitude(shipRigidbody.velocity, shipMaxVelocity);
        }
    }

    private void HandleShipAcceleration()
    {
        // Is ship accelerating?
        isAccelerating = Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            thrustEffect.Play(true);
        } 
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            thrustEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private void HandleShipRotation()
    {
        // Ship rotation
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Rotate(shipRotationSpeed * Time.deltaTime * transform.forward);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(-shipRotationSpeed * Time.deltaTime * transform.forward);
        }
    }

    private void HandleShooting()
    {
        // Shooting.
        if (Input.GetKeyDown(KeyCode.Space)) {
            Rigidbody2D bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

            // Inherit velocity only in the direction of ship.
            Vector2 shipVelocity = shipRigidbody.velocity;
            Vector2 shipDirection = transform.up;
            float shipForwardSpeed = Vector2.Dot(shipVelocity, shipDirection);

            // Dont want to inherit the opposite direction, else we will get stationary bullets.
            if (shipForwardSpeed < 0) {
                shipForwardSpeed = 0;
            }

            bullet.velocity = shipDirection * shipForwardSpeed;

            // Add force to proper bullet in direction of ship.
            bullet.AddForce(bulletSpeed * transform.up, ForceMode2D.Impulse);
        }
    }

    private void HandleDeathEffects()
    {
        explosionEffect.Play(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid")) {
            isAlive = false;

            // Get a reference to the GameManager.
            GameManager gameManager = FindAnyObjectByType<GameManager>();

            // Restart game after delay.
            gameManager.GameOver();

            // Spawn particles on destruction.
            Instantiate(destroyedParticles, transform.position, Quaternion.identity);

            // Destroy the player
            Destroy(gameObject);
        }
    }
}
