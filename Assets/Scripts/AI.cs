using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI
{
    static System.Random random = new System.Random();

    public static int Easy(int[] board)
    {
        List<int> legalMovesList = legalMoves(board);
        int randomMove = legalMovesList[random.Next((legalMovesList.Count))];
        return randomMove;
    }

    public static int Medium(int[] board, int whoseTurn)
    {
        if (whoseTurn == 0)
            return Minimize(board, int.MinValue, int.MaxValue, 2).Item1;
        else
            return Maximize(board, int.MinValue, int.MaxValue, 2).Item1;
    }

    public static int Hard(int[] board, int whoseTurn)
     {
        if (whoseTurn == 0)
            return Minimize(board, int.MinValue, int.MaxValue, 9).Item1;
        else
            return Maximize(board, int.MinValue, int.MaxValue, 9).Item1;
    }



    private static Tuple<int, int> Minimize(int[] board, int alpha, int beta, int depth)
    {
        List<int> legalMovesList = legalMoves(board, 0);

        int bestMoveSoFar = -1;
        int bestScoreSoFar = int.MaxValue;

        if (legalMovesList.Count == 0)
        {
            return new Tuple<int, int>(-1, heuristic(board));
        }
        else if (depth == 0)
        {
            foreach (int i in legalMovesList)
            {
                int[] possibleBoard = (int[]) board.Clone();
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
            foreach (int i in legalMovesList)
            {
                int[] possibleBoard = (int[])board.Clone();
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

    private static Tuple<int, int> Maximize(int[] board, int alpha, int beta, int depth)
    {

        List<int> legalMovesList = legalMoves(board, 1);

        int bestMoveSoFar = -1;
        int bestScoreSoFar = int.MinValue;

        if (legalMovesList.Count == 0)
        {
            return new Tuple<int, int>(-1, heuristic(board));
        }
        else if (depth == 0)
        {
            foreach (int i in legalMovesList)
            {
                int[] possibleBoard = (int[])board.Clone();
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
            foreach (int i in legalMovesList)
            {
                int[] possibleBoard = (int[])board.Clone();
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

    private static List<int> legalMoves(int[] board)
    {
        List<int> openSpots = new List<int>();
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == 0)
                openSpots.Add(i);
        }
        return openSpots;
    }


    private static List<int> legalMoves(int[] board, int whoseTurn)
    {
        List<int> needToPlace = new List<int>();
        List<int> openSpot = legalMoves(board);

        SolutionLine[] solutionLines = GameController.solutionLines(board);

        if (whoseTurn == 0)
        {
            for (int i = 0; i < solutionLines.Length; i++)
            {
                SolutionLine line = solutionLines[i];
                if (line.sum() == -2)
                    return new List<int> {line.winningMove()};
                else if (line.sum() == 2)
                    needToPlace.Add(line.winningMove());
            }
        }
        else if (whoseTurn == 1)
        {
            for (int i = 0; i < solutionLines.Length; i++)
            {
                SolutionLine line = solutionLines[i];
                if (line.sum() == 2)
                    return new List<int> { line.winningMove() };
                else if (line.sum() == -2)
                    needToPlace.Add(line.winningMove());
            }
        }
        if (needToPlace.Count != 0)
            return needToPlace;
        else
            return openSpot;
    }

    private static int heuristic(int[] board)
    {
        int score = 0;
        SolutionLine[] solutions = GameController.solutionLines(board);
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i].sum() == -3)
            {
                score = int.MinValue;
                break;
            }
            else if (solutions[i].sum() == 3)
            {
                score = int.MaxValue;
                break;
            }
            else if (solutions[i].sum() == -2)
            {
                score += -100;
            }
            else if (solutions[i].sum() == 2)
            {
                score += 100;
            }
            else
            {
                score += solutions[i].sum();
            }
        }
        return score;
    }
}