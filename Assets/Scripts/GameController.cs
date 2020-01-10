using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int whoseTurn; // 0 = X, 1 = O
    public int turnCount; // num of turns
    public GameObject[] turnIcons; //display whose turn it is
    public Sprite[] playerIcons;  // 0 = X icon, 1 = O icon
    public Button[] tictactoeSpaces; // playable space for game
    public int[] board;// place piece for each space
    public GameObject[] winningLines;
    public Text winningText;
    public GameObject winningPannel;
    public Button xButton, oButton;
    public int xScore, oScore;
    public Text xScoreText, oScoreText;
    public Boolean[] isAI;


    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void GameSetup()
    {
        whoseTurn = PlayerPrefs.GetInt("whoStarts", 0);
        setCurrent(whoseTurn);
        turnCount = 0;
        xButton.interactable = true;
        oButton.interactable = true;
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = 0;
        }
    }

    public void TicTacToeButton(int spaceNumber)
    {
        xButton.interactable = false;
        oButton.interactable = false;
        tictactoeSpaces[spaceNumber].interactable = false;

        switch (whoseTurn)
        {
            case 0:
                tictactoeSpaces[spaceNumber].image.sprite = playerIcons[0];
                board[spaceNumber] = -1;
                if (checkWinner())
                    return;
                setCurrent(1);
                break;
            case 1:
                tictactoeSpaces[spaceNumber].image.sprite = playerIcons[1];
                board[spaceNumber] = 1;
                if (checkWinner())
                    return;
                setCurrent(0);
                break;
            default:
                Console.WriteLine("Default case");
                break;

        }

        turnCount++;

        if (turnCount == 9)
        {
            tie();
            return;
        }

        if (isAI[whoseTurn])
            TicTacToeButton(AI.Easy(board));

    }

    void tie()
    {
        if (!winningPannel.activeSelf)
        {
            winningPannel.SetActive(true);
            winningText.text = "It's A Tie!";
        }
    }

    Boolean checkWinner()
    {
        int s1 = board[0] + board[1] + board[2];
        int s2 = board[3] + board[4] + board[5];
        int s3 = board[6] + board[7] + board[8];
        int s4 = board[0] + board[3] + board[6];
        int s5 = board[1] + board[4] + board[7];
        int s6 = board[2] + board[5] + board[8];
        int s7 = board[0] + board[4] + board[8];
        int s8 = board[2] + board[4] + board[6];
        int[] solutions = { s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == -3 || solutions[i] == 3)
            {
                displayWinner(i);
                return true;
            }
        }
        return false;
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

    public void setCurrent(int player)
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

    public void setFirst(int player)
    {
        setCurrent(player);
        if (player == 0)
        {
            PlayerPrefs.SetInt("whoStarts", 0);
        }
        else
        {
            PlayerPrefs.SetInt("whoStarts", 1);
        }

    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
