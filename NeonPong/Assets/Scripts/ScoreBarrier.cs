/*
 * Author: Matthew Minnet
 * Date: 11/06/2023
 * Desc: Updates score of the player on the opposite side
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBarrier : Barrier
{
    public PaddleMover oppositePlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ball != null && ball.gameObject == collision.gameObject) // if ball has collided
        {
            UIManager.Instance.UpdateScores(oppositePlayer); // update score of opposite player
        }
    }
}