using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzle : PuzzleScreenTemplate
{
    [SerializeField] private GameObject spotTemplate;
    [SerializeField] private GameObject spotContainer;
    [HideInInspector] private GameObject[,] spotList; 
    // 0 = blank, 1 = filled
    [HideInInspector] private int[][] boardInfo;
    [SerializeField] private int[][] solution;
    //Look at Locker Code for reference
    // Commands to use-  OpenPuzzle();  ClosePuzzle();  Solved();
    // Possibly have a 2d array of ints to represent the board, since I think this is just sudoku
    // At the start, duplicate a button where on picked response gives the x,y coords of it to either fill it or erase it
    void Start()
    {
        spotList=new GameObject[ solution.Length, solution[0].Length ];
        for (int i = 0; i < solution.Length; i++)
        {
            for (int j = 0; j < solution[0].Length; j++)
            {

            }
        }

    }
    protected override void CheckSolved(){
        //Insert checking if solution is right code
    }
}
