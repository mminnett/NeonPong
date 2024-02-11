/*
 * Author: Matthew Minnet
 * Date: 11/06/2023
 * Desc: Paddles move with finger movements.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FingerSwipePaddle : PaddleMover
{
    // Clamp values to keep the paddle inside the game area
    private readonly float BASE = -9.5f + 1.3f;

    private readonly float RANGE = 9.5f - 1.3f;

    public Vector3 normal = Vector2.right;

    public float speed = 25;

    // Update is called once per frame
    protected override void MovePaddle()
    {
        // Convert those screen co-ordinates to world space using some math
        Vector3 pos = transform.position;
        Vector3 newPos = Vector3.zero;

        if (Input.touchCount > 0) // if there are multiple touches on screen
        {
            for (int i = 0; i < Input.touchCount; i++) // loop through touches
            {
                // if touch moved and position is on left side (and this is left paddle)
                if (Input.GetTouch(i).phase == TouchPhase.Moved && Input.GetTouch(i).position.x <= Screen.width / 2 && playerSide == PlayerSide.LEFT)
                {
                    newPos.y = Input.GetTouch(i).deltaPosition.y; // set new y
                }
                // if touch moved and position is on right side (and this is right paddle)
                else if (Input.GetTouch(i).phase == TouchPhase.Moved && Input.GetTouch(i).position.x >= Screen.width / 2 && playerSide == PlayerSide.RIGHT)
                {
                    newPos.y = Input.GetTouch(i).deltaPosition.y; // set new y
                }
            }

            newPos.y = Mathf.Clamp(newPos.y, BASE, RANGE); // keep within bounds

            pos.y = Mathf.Lerp(pos.y, newPos.y, speed * Time.deltaTime); // lerp the y movement

            transform.position = pos; // update transform
        }
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