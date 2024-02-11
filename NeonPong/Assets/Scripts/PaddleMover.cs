/*
 * Author: Matthew Minnett
 * Date: 11/06/2023
 * Desc: Abstract class for paddle movement scripts to derive from
 */

using UnityEngine;

// Any class that moves the paddle must extend this class. Do NOT use a MonoBehaviour class to move
// the paddle.
public abstract class PaddleMover : MonoBehaviour
{
    // The player's score.
    public int Score { get; set; }

    public enum PlayerSide
    { NOT_SELECTED, LEFT, RIGHT }

    public PlayerSide playerSide;

    // Move the paddle using the input method that the platform supports.
    protected abstract void MovePaddle();

    // Inform sub-classes that a collision has occurred.
    protected abstract void Collision(Ball ball);

    #region Unity messages

    // Move the paddle.
    private void Update()
    {
        MovePaddle();
    }

    // Handle ball - paddle collisions.
    private void OnCollisionEnter2D(Collision2D c)
    {
        // If we collided with a call, the call collision on the ball we collided with.
        if (c.gameObject == UIManager.Instance.ball.gameObject) // if c is ball in play
        {
            Ball ball;

            if (c.gameObject.TryGetComponent(out ball)) // ensure c has ball script
            {
                Collision(ball);
            }
        }
    }

    #endregion Unity messages
}