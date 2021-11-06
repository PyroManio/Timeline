using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparancy : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0.5f);
    }
}
