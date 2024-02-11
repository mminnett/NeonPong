using UnityEngine;

// Mouse paddle interaction
public class MousePaddle : PaddleMover
{
    // Clamp values to keep the paddle inside the game area
    private readonly float BASE = -9.5f;

    private readonly float RANGE = 9.5f * 2;

    public Vector3 normal = Vector2.right;

    // Update is called once per frame
    protected override void MovePaddle()
    {
        // Get the y value with respect to mouse position in screen co-ordinates
        float y = Mathf.Clamp(Input.mousePosition.y / Screen.height, 0f, 1f);

        // Convert those screen co-ordinates to world space using some math
        Vector3 pos = transform.position;
        pos.y = BASE + y * RANGE;

        // Set the paddle's position
        transform.position = pos;
    }

    protected override void Collision(Ball ball)
    {
        // Determine what side of the paddle the ball is on; right is z +ve, left is z -ve
        Vector3 cross = Vector3.Cross(transform.position.normalized, ball.transform.position.normalized);

        if (Mathf.Sign(cross.z) < 0)
        {
            // Otherwise, flip to the normal being on the other side
            normal *= -1;
        }

        // Reflect the ball here
        UIManager.Instance.ReflectBall(-normal);
    }
}