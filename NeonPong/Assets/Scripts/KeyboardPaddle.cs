/*
 * Author: Matthew Minnet
 * Date: 11/06/2023
 * Desc: Controls paddle movement with keyboard
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardPaddle : PaddleMover
{
    // Clamp values to keep the paddle inside the game area
    private readonly float BASE = -9.5f + 1.3f;

    private readonly float RANGE = 9.5f - 1.3f;

    // used to determine what controls paddle should move with
    public enum ControlScheme 
    { NOT_SELECTED, WASD, ARROWS }

    public ControlScheme controlScheme;

    public Vector3 normal = Vector2.right;

    public float speed = 25;

    // Update is called once per frame
    protected override void MovePaddle()
    {
        // Convert those screen co-ordinates to world space using some math
        Vector3 pos = transform.position;

        if (controlScheme == ControlScheme.WASD) // if control scheme 1
        {
            if (Input.GetKey(KeyCode.W))
            {
                pos.y = pos.y + 1 * speed * Time.deltaTime; // move up
            }
            else if (Input.GetKey(KeyCode.S))
            {
                pos.y = pos.y - 1 * speed * Time.deltaTime; // move down
            }
        }
        else if (controlScheme == ControlScheme.ARROWS) // if control scheme 2
        {
            if (Input.GetKey(KeyCode.UpArrow)) 
            {
                pos.y = pos.y + 1 * speed * Time.deltaTime; // move up
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                pos.y = pos.y - 1 * speed * Time.deltaTime; // move down
            }
        }

        pos.y = Mathf.Clamp(pos.y, BASE, RANGE); // keep within range

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