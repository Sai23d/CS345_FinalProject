using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthPowerup : MonoBehaviour
{
    
    void Start()
    {
        
    }

  void onTriggerEnter2D(Collider2D trigger){
    if(trigger.gameObject.GetComponent<Player> ()){
        Destroy(gameObject);
    }
  }
}

