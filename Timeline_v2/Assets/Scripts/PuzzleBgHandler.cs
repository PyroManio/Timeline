using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBgHandler : MonoBehaviour
{
    [SerializeField] private Sprite[] bgSprites;
    public void changeBG(int index)
    {
        if (index >= bgSprites.Length && index < 0) return;
        GetComponent<SpriteRenderer>().sprite = bgSprites[index];
    }
}
