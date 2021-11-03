using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;
public class ExpressionDialogueSprite : MonoBehaviour
{
    [SerializeField] public Sprite[] expressionSprites;
    
  public void changeExpression(int expressionIndex)
  {
      if (expressionIndex>expressionSprites.Length) expressionIndex = 0;
      GetComponent<Image>().sprite=expressionSprites[expressionIndex];
  }
}
