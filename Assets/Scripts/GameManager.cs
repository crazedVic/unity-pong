using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static System.Action NewRoundHandler;

    [SerializeField]
    TextMeshProUGUI playerScoreLabel;

    [SerializeField]
    TextMeshProUGUI computerScoreLabel;

    [SerializeField]
    Rigidbody2D playerPaddle;

    [SerializeField]
    Rigidbody2D computerPaddle;

    [SerializeField]
    Rigidbody2D ball;

    private int computerScore = 0;
    private int playerScore = 0;

    // publish to github pages
    // https://www.youtube.com/watch?v=CrsG_v1lCiQ&ab_channel=Simmer.io
    void Awake(){
       Ball.PlayerScoresHandler += OnPlayerScores;
       Ball.ComputerScoresHandler += OnComputerScores;
    }

    void OnPlayerScores(){
        playerScore ++;
        playerScoreLabel.text = playerScore.ToString();
        if(playerScore == 10)
            {
            Debug.Log("Player wins!");
            ResetGameField(true);
            }
            else ResetGameField();
    }

    void ResetScores() {
            computerScore = 0;
            playerScore = 0;
            playerScoreLabel.text = playerScore.ToString();
            computerScoreLabel.text = computerScore.ToString();
    }

    void OnComputerScores(){
            computerScore++;
             computerScoreLabel.text = computerScore.ToString();
             if(computerScore == 10)
            {
                Debug.Log("Computer wins!");
                ResetGameField(true);
            }
            else  ResetGameField();
    }

    void ResetGameField(bool resetScores =  false){
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
        if(resetScores){
            yield return new WaitForSeconds(4.0f);
            ResetScores();
        }
        else{
            yield return new WaitForSeconds(1.5f);
        }
            NewRoundHandler.Invoke();
    }
}
