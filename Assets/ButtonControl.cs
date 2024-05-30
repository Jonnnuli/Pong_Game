using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** ButtonControl - Player clicks buttons.
 *  Jonna Helaakoski 2023
 */
public class ButtonControl : MonoBehaviour
{
    [SerializeField] private Button playMultiplayer;

    public MovePlayer movePlayer;
    public MoveBall moveBall;

    
    // ClickButton1 - Button1 has been clicked. Multiplayer game selected.
    public void ClickButton1()
    {
        movePlayer.ChooseMultiplayer();
        moveBall.ResetGame();
    }

    
    // ClickButton2 - Button2 has been clicked. AI game selected.
    public void ClickButton2()
    {
        movePlayer.ChooseAI();
        moveBall.ResetGame();
    }
}
