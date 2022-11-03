using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static System.Action OnNewRoundStarted;
    public static System.Action OnScoreReset;
    public static System.Action<int> OnPlayerScoredEvent;
    public static System.Action<int> OnComputerScoredEvent;

    [SerializeField]
    Rigidbody2D playerPaddle;

    [SerializeField]
    Rigidbody2D computerPaddle;

    [SerializeField]
    Rigidbody2D ball;

    public static int computerScore = 0;
    public static int playerScore = 0;

    void Awake()
    {
       Ball.PlayerScoresHandler += OnPlayerScores;
       Ball.ComputerScoresHandler += OnComputerScores;
    }

    void ResetScores() 
    {
        computerScore = 0;
        playerScore = 0;
        OnScoreReset?.Invoke();
    }
    
    void OnPlayerScores()
    {
        playerScore++;
        OnPlayerScoredEvent?.Invoke(playerScore);
        
        if(playerScore == 10)
        {
            Debug.Log("Player wins!");
            ResetGameField(true);
        }
        else
        {
            ResetGameField();
        }
    }
    
    void OnComputerScores()
    {
        computerScore++;
        OnComputerScoredEvent?.Invoke(computerScore);
        if(computerScore == 10)
        {
            Debug.Log("Computer wins!");
            ResetGameField(true);
        }
        else
        {
            ResetGameField();
        }
    }

    void ResetGameField(bool resetScores =  false)
    {
        ball.velocity = Vector2.zero;
        playerPaddle.velocity = Vector2.zero;
        computerPaddle.velocity = Vector2.zero;
        ball.transform.position = new Vector2(0,0);
        playerPaddle.position = new Vector2(-8,0);
        computerPaddle.position = new Vector2(8,0);
        StartCoroutine(NewRound(resetScores));
         
    }

    private IEnumerator NewRound(bool resetScores =  false)
    {
        if(resetScores)
        {
            yield return new WaitForSeconds(4.0f);
            ResetScores();
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
        }
        OnNewRoundStarted.Invoke();
    }
}
