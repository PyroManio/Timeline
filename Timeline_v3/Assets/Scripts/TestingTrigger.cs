using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingTrigger : MonoBehaviour
{
    EventManager eM;
    [SerializeField] public MultiSceneLoader msL;
    private void OnTriggerEnter2D(Collider2D other)
    {
        msL.LoadScene(SceneName.Hallway);
        EventManager.TriggerEvent("testMan",  new Dictionary<string, object> { { "pause", true } });
       // GuidReference reference = new GuidReference();
        
        //em=reference.gameObject.GetComponent<EventManager>();
        //eM.TriggerEvent("testMan",  null);
    }
    
}

