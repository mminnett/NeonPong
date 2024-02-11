/*
 * Author: Matthew Minnet
 * Date: 11/06/2023
 * Desc: Tracks scoring and manages UI elements of game
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Variables
    public GameObject gameOverImage;

    // score sprites
    public NumericSprite leftScore;
    public NumericSprite rightScore;

    public const int MAX_SCORE = 9;

    public List<GameObject> UIElements = new List<GameObject>(); // to be hidden on game over
    public List<GameObject> introText = new List<GameObject>(); // to be hidden on game start

    // ball in play
    public Ball ball;
    #endregion

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    private void Start()
    {
        gameOverImage.SetActive(false); // ensure game over image set to false
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) // if escape is pressed (or back button)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // quit in editor
#else
            Application.Quit(); // quit in application
#endif
        }

        // If not served and a button was pressed, serve
        if (Input.acceleration.sqrMagnitude >= 5f || Input.GetKeyUp(KeyCode.Space))
        {
            ball.ServeBall(); // sets served to true

            foreach (GameObject go in introText)
            {
                go.SetActive(false); // disable text
            }
        }
    }

    /// <summary>
    /// Update score of passed in player paddle
    /// </summary>
    /// <param name="player"></param>
    public void UpdateScores(PaddleMover player)
    {
        if (player.playerSide == PaddleMover.PlayerSide.LEFT) // if left player
        {
            leftScore.Number++; // increment score

            if (ball != null && leftScore.Number < MAX_SCORE) 
            {
                ball.ResetBall(1); // reset ball if score is less than max
            }
            else if (leftScore.Number >= MAX_SCORE)
            {
                leftScore.Number = MAX_SCORE;
                GameOver(); // game over if score is equal to the max score
            }
        }
        else if (player.playerSide == PaddleMover.PlayerSide.RIGHT) // if right player
        {
            rightScore.Number++; // increment right score

            if (ball != null && rightScore.Number < MAX_SCORE)
            {
                ball.ResetBall(-1); // reset ball if score < max
            }
            else if (rightScore.Number >= MAX_SCORE)
            {
                rightScore.Number = MAX_SCORE;
                GameOver(); // game over if score is equal to the max score
            }
        }
    }

    /// <summary>
    /// Sends the ball in direction passed in
    /// </summary>
    /// <param name="normal"></param>
    public void ReflectBall(Vector3 normal)
    {
        if (ball != null)
        {
            ball.Reflect(normal);
        }
    }

    /// <summary>
    /// Sets UI elements to inactive and enables game over image
    /// </summary>
    private void GameOver()
    {
        foreach (var element in UIElements)
        {
            element.SetActive(false);
        }

        ball.gameObject.SetActive(false);

        gameOverImage.SetActive(true);
    }
}