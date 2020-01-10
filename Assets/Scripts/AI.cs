using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{
    static System.Random random = new System.Random();

    public static int Easy(int[] board)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] != 0)
                list.Add(i);
        }
        return random.Next(list.Count);
    }

    public int Impossible(int[] board, int whoseTurn)
     {
        if (whoseTurn == -1)
            return Minimize(board, int.MinValue, int.MaxValue, 9).Item1;
        else
            return Maximize(board, int.MinValue, int.MaxValue, 9).Item1;
    }



    Tuple<int, int> Minimize(int[] board, int alpha, int beta, int depth)
    {

        List<int> openSpots = new List<int>();
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
                openSpots.Add(i);
        }
        int bestMoveSoFar = 0;
        int bestScoreSoFar = int.MaxValue;

        if (depth == 0 || openSpots.Count == 0)
        {
            for (int i = 0; i < openSpots.Count; i++)
            {
                int[] possibleBoard = board;
                possibleBoard[i] = -1;
                int possibleScore = heuristic(possibleBoard);
                if (possibleScore <= bestScoreSoFar)
                {
                    bestMoveSoFar = i;
                    bestScoreSoFar = possibleScore;
                    beta = Math.Min(beta, bestMoveSoFar);
                    if (beta <= alpha)
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < openSpots.Count; i++)
            {
                int[] possibleBoard = board;
                possibleBoard[i] = -1;
                Tuple<int, int> possibleMove = Maximize(possibleBoard, alpha, beta, depth - 1);
                if (possibleMove.Item2 <= bestScoreSoFar)
                {
                    bestMoveSoFar = i;
                    bestScoreSoFar = possibleMove.Item2;
                    beta = Math.Min(beta, bestMoveSoFar);
                    if (beta <= alpha)
                        break;
                }
            }
        }
        return new Tuple<int, int>(bestMoveSoFar, bestScoreSoFar);
    }

    Tuple<int, int> Maximize(int[] board, int alpha, int beta, int depth)
    {

        List<int> openSpots = new List<int>();
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
                openSpots.Add(i);
        }
        int bestMoveSoFar = 0;
        int bestScoreSoFar = int.MinValue;

        if (depth == 0 || openSpots.Count == 0)
        {
            for (int i = 0; i < openSpots.Count; i++)
            {
                int[] possibleBoard = board;
                possibleBoard[i] = 1;
                int possibleScore = heuristic(possibleBoard);
                if (possibleScore >= bestScoreSoFar)
                {
                    bestMoveSoFar = i;
                    bestScoreSoFar = possibleScore;
                    alpha = Math.Max(alpha, bestScoreSoFar);
                    if (beta <= alpha)
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < openSpots.Count; i++)
            {
                int[] possibleBoard = board;
                possibleBoard[i] = 1;
                Tuple<int, int> possibleMove = Minimize(possibleBoard, alpha, beta, depth - 1);
                if (possibleMove.Item2 >= bestScoreSoFar)
                {
                    bestMoveSoFar = i;
                    bestScoreSoFar = possibleMove.Item2;
                    alpha = Math.Max(alpha, bestScoreSoFar);
                    if (beta <= alpha)
                        break;
                }
            }
        }
        return new Tuple<int, int>(bestMoveSoFar, bestScoreSoFar);
    }


    public int heuristic(int[] board)
    {
        int score = 0;
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
            if (solutions[i] == -3)
            {
                score = int.MinValue;
                break;
            }
            else if (solutions[i] == 3)
            {
                score = int.MaxValue;
                break;
            }
            else
            {
                score += solutions[i];
            }
        }
        return score;
    }
}