using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    public ZoomIn sprite;
    // Start is called before the first frame update

    private void OnMouseDown() {
        gameObject.SetActive(true);
    }
    // public void Zoom() {
    //     gameObject.SetActive(true);
    // }
    // void Start()
    // {
    //     sprite = GetComponent<SpriteRenderer>();
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
