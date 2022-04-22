using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Array2DEditor{
public class TilePuzzle : PuzzleScreenTemplate
{
    [SerializeField] private GameObject spotTemplate;
    [SerializeField] private GameObject spotContainer;
    [HideInInspector] private GameObject[,] spotList; 
    // true = filled, false=blank
    [HideInInspector] private bool[][] boardInfo;
    [SerializeField] private Array2DBool solution;
    //Look at Locker Code for reference
    // Commands to use-  OpenPuzzle();  ClosePuzzle();  Solved();
    // Possibly have a 2d array of ints to represent the board, since I think this is just sudoku
    // At the start, duplicate a button where on picked response gives the x,y coords of it to either fill it or erase it
    void Start()
    {
        spotTemplate.SetActive(false);
        spotList=new GameObject[ solution.GridSize.y, solution.GridSize.x ];
        for (int i = 0; i < solution.GridSize.y; i++)
        {
            for (int j = 0; j < solution.GridSize.x; j++)
            {
                GameObject spot = Instantiate(spotTemplate.gameObject, spotContainer.transform);
                spot.gameObject.SetActive(true);
                spot.GetComponent<Button>().onClick.AddListener( () => OnPickedResponse(i, j));
                spot.GetComponent<Image>().color=new Color(1f,1f,1f,1f);
                spotList[i,j] = spot;
            }
        }
    ClosePuzzle();
    }
    private void OnPickedResponse(int y, int x)
    {
        // If true
        if (boardInfo[y][x]) {
            boardInfo[y][x] = false;
            spotList[y,x].GetComponent<Image>().color = new Color(1f,1f,1f,1f);
        }
        else{
            boardInfo[y][x] = true;
            spotList[y,x].GetComponent<Image>().color = new Color(0f,0f,0f,1f);
        }
    }
    protected override void CheckSolved(){
        //Insert checking if solution is right code
    }
}

}