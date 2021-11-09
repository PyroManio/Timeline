using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum InteractionType { NONE,PickUp,Examine}
    public InteractionType type;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    public void Interact()
    {
        switch (type)
        {
            case InteractionType.PickUp:
                GameObject item = gameObject;
                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);
                gameObject.SetActive(false);
                break;
            case InteractionType.Examine:
                Debug.Log("EXAMINE");
                break;
            default:
                Debug.Log("NULL ITEM");
                break;
        }
    }
}
