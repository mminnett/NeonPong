using UnityEngine;

// Handle barrier collisions.
public class Barrier : MonoBehaviour
{
    // The normal vector of the barrier. This is the facing direction that the ball is likely
    // to hit. For example, the barrier on the bottom of the screen has a normal of (0, 1, 0).
    public Vector3 normal = Vector3.up;

    protected Ball ball;

    private void Start()
    {
        ball = UIManager.Instance.ball; // get reference to the ball
    }

    // Handle 2D collisions.
    private void OnCollisionEnter2D(Collision2D c)
    {
        // If we collided with a ball, reflect it.
        if (ball != null && ball.gameObject == c.gameObject)
        {
            UIManager.Instance.ReflectBall(normal); // call the reflect ball script on UI Manager
        }
    }
}