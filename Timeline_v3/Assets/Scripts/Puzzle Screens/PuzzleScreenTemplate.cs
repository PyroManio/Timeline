using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class PuzzleScreenTemplate : MonoBehaviour
{
    //Event that executes when the puzzle is solved
    [SerializeField] private UnityEvent solutionEvent;
    [SerializeField] private GameObject puzzleContainer;
    [HideInInspector] public bool isSolved = false;
    public bool IsOpen { get ; protected set; } 
    public void OpenPuzzle(){
        if (isSolved) return;
        //GetComponent<ClickableObjectHandler>().imOutOfVariableNames = false;
        IsOpen=true;
        puzzleContainer.SetActive(true);
    }
    public void ClosePuzzle()
    {
        //GetComponent<ClickableObjectHandler>().imOutOfVariableNames = true;
        IsOpen = false;
        puzzleContainer.SetActive(false);
    }
    protected abstract void CheckSolved();
    
    // Should only run once. Run after the puzzle is solved.
    protected void Solved(){
        isSolved = true;
        solutionEvent.Invoke();
        ClosePuzzle();
    }
}
