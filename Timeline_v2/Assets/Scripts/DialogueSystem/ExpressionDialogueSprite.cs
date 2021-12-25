using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ExpressionDialogueSprite : MonoBehaviour
{
    static Sprite[] LeoSprites;
    private Dictionary<Expression,Sprite> expressionSprite;
  private void Awake(){
    Debug.Log("startup");
    LeoSprites = Resources.LoadAll<Sprite>("Leo HUD Sprite Sheet 1.png");
    Debug.Log(LeoSprites.Length);
    Object[] temppp =  Resources.LoadAll("Leo HUD Sprite Sheet 1.png", typeof(Texture2D));
    Debug.Log(temppp.Length);
    expressionSprite = new Dictionary<Expression, Sprite>()
    {
       { Expression.Neutral, LeoSprites[0] },
       { Expression.Happy, LeoSprites[1] },
       { Expression.Frown, LeoSprites[2] },
       { Expression.Sad, LeoSprites[3] },
       { Expression.Shock, LeoSprites[4] },
       { Expression.Upset, LeoSprites[5] },
       { Expression.Angry, LeoSprites[6] },
       { Expression.Thinking, LeoSprites[7] },
       { Expression.Idea, LeoSprites[8] },
       { Expression.Stress, LeoSprites[9] },
       { Expression.IdkExpressions, LeoSprites[10] },
       { Expression.Hit, LeoSprites[11] },
       { Expression.Void, LeoSprites[12] }
    };

  }
  public void changeExpression(CharacterTalking charSpeaking, Expression givenExpression)
  {
    //Sprite temp= Resources.Load("Assets/Sprites/Leo/Leo HUD Sprite Sheet 1.png");
    //Texture2D t = ()
    //Sprite temp = Resources.Load("Assets/Sprites/Leo/Leo HUD Sprite Sheet 1.png") as Sprite;
    
      //if (expressionIndex>expressionSprites.Length) expressionIndex = 0;
      //GetComponent<Image>().sprite=expressionSprites[expressionIndex];
      Debug.Log("test");
      GetComponent<Image>().sprite = expressionSprite[givenExpression];
  }
}
