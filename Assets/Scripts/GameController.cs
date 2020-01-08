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
        for (int i = 0; i < tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpace.Length; i++)
        {
            markedSpace[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TicTacToeButton(int spaceNumber)
    {
        tictactoeSpaces[spaceNumber].image.sprite = playerIcons[whoseTurn];
        tictactoeSpaces[spaceNumber].interactable = false;

        switch (whoseTurn)
        {
            case 0:
                markedSpace[spaceNumber] = -1;
                whoseTurn = 1;
                turnIcons[1].SetActive(true);
                turnIcons[0].SetActive(false);
                break;
            case 1:
                markedSpace[spaceNumber] = 1;
                whoseTurn = 0;
                turnIcons[0].SetActive(true);
                turnIcons[1].SetActive(false);
                break;
            default:
                Console.WriteLine("Default case");
                break;

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
        int[] solutions = {s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < solutions.Length; i++)
        {
            if (i == -3 || i == 3)
                Debug.Log("Player " + whoseTurn + " won!");

        }

    }
}
