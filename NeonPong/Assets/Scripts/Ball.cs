using UnityEngine;

// Ball controller.
public class Ball : MonoBehaviour
{
    // Start position. This is a cached value. Call Reset() to reset the ball position to this location.
    private Vector3 start;

    // Initial velocity.
    private Vector3 initialVelocity;

    // True if the ball is served.
    private bool served;

    // The speed of the ball. This can be set in the inspector if the designer wants to speed up / slow down
    // the ball. Shouldn't be changed by any other object.
    public Vector3 velocity = new Vector3(16, -16, 0);

    public float speed = 25; // speed of ball

    private Rigidbody2D rb; // rigidbody attached to ball

    // Reset the ball position and clear the 'served' flag.
    public void ResetBall(int sendDir)
    {
        transform.position = start;
        velocity = initialVelocity;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;

        velocity.x *= sendDir;

        served = false;
    }

    // Reflect the ball based on the normal of the object that it hit
    public void Reflect(Vector3 normal)
    {
        velocity = Vector3.Reflect(velocity, normal);
    }

    // Use this for initialization
    private void Start()
    {
        // Cache the start position and velocity
        start = transform.position;
        initialVelocity = velocity;
        rb = GetComponent<Rigidbody2D>(); // get rigidbody
    }

    private void FixedUpdate()
    {
        // If served, update my position.
        if (served)
        {
            rb.velocity = velocity * speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Function to serve the ball (sets served to true
    /// </summary>
    public void ServeBall()
    {
        if (!served)
        {
            served = true;
        }
    }
}