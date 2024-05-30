using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * MoveBall
 * Ball movement and collision. Counts player and AI points that appear in the game.
 * Jonna Helaakoski 2023
 */

public class MoveBall : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private Text playerScore;
    [SerializeField] private Text AIScore;
    [SerializeField] private Text WinnerText;
    [SerializeField] private AudioSource HitSound;

    private Rigidbody2D rb;
    private bool winner = false;
    public MovePlayer moveplayer;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    
    // FixedUpdate - Fixes frame update.
    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + speedIncrease);
    }

    
    // StartBall - Ball starts moving. 
    private void StartBall()
    {
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + speedIncrease); //ball moves towards the player.
        WinnerText.text = "";
    }

    
    // ResetBall - Ball is reset.
    private void ResetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        if(winner == true)
        {
            Invoke("StartBall", 5f);
        }
        else
        {
           Invoke("StartBall", 3f);
        }
        
    }


    // PlayerBounce - Ball bounces from player.
    private void PlayerBounce(Transform myObject)
    {

        Vector2 ballPosition = transform.position; //ball position
        Vector2 playerPosition = myObject.position; //player position

        float xDirection, yDirection;
        
        //set x direction
        if(transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }
        yDirection = (ballPosition.y - playerPosition.y) / myObject.GetComponent<Collider2D>().bounds.size.y; //set y direction
        if (yDirection == 0)
        {
            yDirection = 0.25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + speedIncrease); //rigidbody velocity
    }

    // OnCollisionEnter2D - Checks if collision happends with Player1 or AI and sound is played.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player1" || collision.gameObject.name == "AI")
        {
            HitSound.Play();
            PlayerBounce(collision.transform);
        }
    }

    // OnTriggerEnter2D - Ball collision.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(transform.position.x > 0) //Ball hits AI goal, player gets point
        {
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
            CheckIfWin();
            ResetBall();
        }
        else if (transform.position.x < 0) //Ball hits players goal, AI gets point
        {
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
            CheckIfWin();
            ResetBall();
        }
    }

    // CheckIfWin - Checks if player or AI has enough points to win.
    private void CheckIfWin()
    {
        if (playerScore.text == "5") //If player has enough points to win, game says "Pelaaja voitti!".
        {
            WinnerText.text = "Pelaaja1 voitti!";
            winner = true;
            Invoke("ResetGame", 5f);
        }
        else if (AIScore.text == "5") //If AI has enough points to win, game says "AI voitti!". If multiplayer game, game says "Pelaaja2 voitti!".
        {
            if (moveplayer.IsMultiplayer() == true)
            {
                WinnerText.text = "Pelaaja2 voitti!";
                winner = true;
                Invoke("ResetGame", 5f);
            }
            else
            {
                WinnerText.text = "AI voitti!";
                winner = true;
                Invoke("ResetGame", 5f);
            }
        }
    }

    
    // ResetGame - the points count starts from the beginning and winner is false. Ball to starting point.
    public void ResetGame()
    {
        playerScore.text = "0";
        AIScore.text = "0";
        winner = false;
        ResetBall();
    }
}
