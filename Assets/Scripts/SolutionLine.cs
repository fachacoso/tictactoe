using System;

public class SolutionLine
{
    int index1;
    int index2;
    int index3;

    int[] playingBoard;

    public SolutionLine(int[] board, int whichLine)
    {
        playingBoard = board;
        switch (whichLine)
        {
            case 1:
                index1 = 0;
                index2 = 1;
                index3 = 2;
                break;
            case 2:
                index1 = 3;
                index2 = 4;
                index3 = 5;
                break;
            case 3:
                index1 = 6;
                index2 = 7;
                index3 = 8;
                break;
            case 4:
                index1 = 0;
                index2 = 3;
                index3 = 6;
                break;
            case 5:
                index1 = 1;
                index2 = 4;
                index3 = 7;
                break;
            case 6:
                index1 = 2;
                index2 = 5;
                index3 = 8;
                break;
            case 7:
                index1 = 0;
                index2 = 4;
                index3 = 8;
                break;
            case 8:
                index1 = 2;
                index2 = 4;
                index3 = 6;
                break;
            default:
                break;
        }
    }

    public int sum()
    {
        return playingBoard[index1] + playingBoard[index2] + playingBoard[index3];
    }

    public int winningMove()
    {
        if (playingBoard[index1] == 0)
            return index1;
        else if (playingBoard[index2] == 0)
            return index2;
        else
            return index3;

    }

    public Boolean hasWinner()
    {
        return sum() == -3 || sum() == 3;
    }

    public int winner()
    {
        if (hasWinner())
            return playingBoard[index1];
        return 0;

    }
}