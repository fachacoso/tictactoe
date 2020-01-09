using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int whoseTurn; // 0 = X, 1 = O
    public int turnCount; // num of turns
    public GameObject[] turnIcons; //display whose turn it is
    public Sprite[] playerIcons;  // 0 = X icon, 1 = O icon
    public Button[] tictactoeSpaces; // playable space for game
    public int[] markedSpace; // place piece for each space
    public GameObject[] winningLines;
    public Text winningText;
    public GameObject winningPannel;
    public Button xButton, oButton;
    public int xScore, oScore;
    public Text xScoreText, oScoreText;
   

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoseTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        xButton.interactable = true;
        oButton.interactable = true;
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpace.Length; i++)
        {
            markedSpace[i] = 0;
        }
        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }

    }

    public void TicTacToeButton(int spaceNumber)
    {
        xButton.interactable = false;
        oButton.interactable = false;
        tictactoeSpaces[spaceNumber].image.sprite = playerIcons[whoseTurn];
        tictactoeSpaces[spaceNumber].interactable = false;

        switch (whoseTurn)
        {
            case 0:
                markedSpace[spaceNumber] = -1;
                checkWinner();
                whoseTurn = 1;
                turnIcons[1].SetActive(true);
                turnIcons[0].SetActive(false);
                break;
            case 1:
                markedSpace[spaceNumber] = 1;
                checkWinner();
                whoseTurn = 0;
                turnIcons[0].SetActive(true);
                turnIcons[1].SetActive(false);
                break;
            default:
                Console.WriteLine("Default case");
                break;

        }
        turnCount++;
        if (turnCount == 9)
            tie();
    }

    void tie()
    {
        if (!winningPannel.activeSelf)
        {
            winningPannel.SetActive(true);
            winningText.text = "It's A Tie!";
        }
    }

    void checkWinner()
    {
        int s1 = markedSpace[0] + markedSpace[1] + markedSpace[2];
        int s2 = markedSpace[3] + markedSpace[4] + markedSpace[5];
        int s3 = markedSpace[6] + markedSpace[7] + markedSpace[8];
        int s4 = markedSpace[0] + markedSpace[3] + markedSpace[6];
        int s5 = markedSpace[1] + markedSpace[4] + markedSpace[7];
        int s6 = markedSpace[2] + markedSpace[5] + markedSpace[8];
        int s7 = markedSpace[0] + markedSpace[4] + markedSpace[8];
        int s8 = markedSpace[2] + markedSpace[4] + markedSpace[6];
        int[] solutions = { s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == -3 || solutions[i] == 3)
            {
                displayWinner(i);
                break;
            }
        }
    }

    void displayWinner(int solutionNumber)
    {
        winningPannel.SetActive(true);
        winningLines[solutionNumber].SetActive(true);
        if (whoseTurn == 0)
        {
            xScore++;
            xScoreText.text = xScore.ToString();
            winningText.text = "Player X Wins!";
        }
        else
        {
            oScore++;
            oScoreText.text = oScore.ToString();
            winningText.text = "Player O Wins!";
        }
    }

    public void rematch()
    {
        GameSetup();
        winningPannel.SetActive(false);
        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
    }

    public void restart()
    {
        rematch();
        xScore = 0;
        xScoreText.text = xScore.ToString();
        oScore = 0;
        oScoreText.text = oScore.ToString();
    }

    public void switchPlayer(int player)
    {
        if (player == 0)
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else
        {
            whoseTurn = 1;
            turnIcons[1].SetActive(true);
            turnIcons[0].SetActive(false);
        }

    }
}
