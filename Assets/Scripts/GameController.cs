﻿using System;
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
        if (PlayerPrefs.GetInt("ai") != 0)
            InvokeRepeating("checkAI", 0, 1);
    }

    void GameSetup()
    {
        whoseTurn = PlayerPrefs.GetInt("whoStarts", 0);
        setCurrent(whoseTurn);
        turnCount = 0;
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
                if (gameOver())
                {
                    gameOverScreen();
                    return;
                }
                setCurrent(1);
                break;
            case 1:
                tictactoeSpaces[spaceNumber].image.sprite = playerIcons[1];
                board[spaceNumber] = 1;
                if (gameOver())
                {
                    gameOverScreen();
                    return;
                }
                setCurrent(0);
                break;
            default:
                Console.WriteLine("Default case");
                break;

        }

        turnCount++;
        if (gameOver())
        {
            gameOverScreen();
            return;
        }
    }

    Boolean gameOver()
    {
        SolutionLine[] solutions = solutionLines(board);
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i].hasWinner())
            {
                return true;
            }
        }
        if (turnCount > 8)
            return true;
        return false;
    }

    void gameOverScreen()
    {
        int solutionNumber = -1;

        SolutionLine[] solutions = solutionLines(board);
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i].hasWinner())
            {
                solutionNumber = i;
            }
        }
        winningPannel.SetActive(true);
        if (solutionNumber == -1)
            winningText.text = "It's A Tie!";
        else
        {
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
        xButton.interactable = true;
        oButton.interactable = true;
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

    void checkAI()
    {
        if (!gameOver() && isAI[whoseTurn])
        {
            int aiMove;
            if (PlayerPrefs.GetInt("ai") == 1)
            {
                aiMove = AI.Easy(board);
            }
            if (PlayerPrefs.GetInt("ai") == 2)
            {
                aiMove = AI.Medium(board, whoseTurn);
            }
            else
            {
                aiMove = AI.Hard(board, whoseTurn);
            }
            TicTacToeButton(aiMove);
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static SolutionLine[] solutionLines(int[] board)
    {
        SolutionLine[] solutions = new SolutionLine[8];
        for (int i = 1; i < 9; i++)
        {
            solutions[i - 1] = new SolutionLine(board, i);
        }
        return solutions;
    }
}