using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
 * MovePlayer
 * Player and AI control.
 * Jonna Helaakoski 2023
 */

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float movingSpeed;
    [SerializeField] private bool isAI;
    [SerializeField] private GameObject ball;
    [SerializeField] private bool isMultiplayer = false;

    private Rigidbody2D rb;
    private Vector2 playerMove;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isAI)
        {
            if(isMultiplayer)
            {
                Player2Control();
            }
            else
            {
                AIControl();
            }
        } 
        else
        {
            Player1Control();
        }
    }
    
    // Player1Control - controls Player1 movement.
    private void Player1Control()
    {
        playerMove = new Vector2(0, Input.GetAxisRaw("Vertical")); //0 means that it doesn't move with x coordinate. Moving with s and w buttons.
    }

    
    // Player2Control - controls Player2 movement.
    private void Player2Control()
    {
        playerMove = new Vector2(0, Input.GetAxisRaw("Vertical2")); // moving with up and down arrow buttons
    }

   
    // AI control - Controls AI position, moves AI.
    private void AIControl()
    {
        if (ball.transform.position.y > transform.position.y + 0.5f)
        {
            playerMove = new Vector2(0, 1);
        }
        else if(ball.transform.position.y < transform.position.y - 0.5f)
        {
            playerMove = new Vector2(0, -1);
        }
        else
        {
            playerMove = new Vector2(0, 0);
        }
    }

    
    // FixedUpdate - Fixes frame update.
    private void FixedUpdate()
    {
        rb.velocity = playerMove * movingSpeed;
    }

    // ChooseMultiplayer - Choose multiplayer game.
    public void ChooseMultiplayer()
    {
        isMultiplayer = true;
    }

    
    // ChooseAI - Choose AI game.
    public void ChooseAI()
    {
        isMultiplayer = false;
    }

    // IsMultiplayer - is game multiplayer, returns true or false.
    public bool IsMultiplayer()
    {
        return isMultiplayer;
    }
}
