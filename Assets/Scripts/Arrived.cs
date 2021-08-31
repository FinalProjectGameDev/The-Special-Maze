using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrived : MonoBehaviour
{
    public bool alredyArrived = false;
    public QuestGiver QG;

    void OnTriggerEnter(Collider other)
    {
        if(!alredyArrived){
            alredyArrived = true;       
            QG.openExplain();
        }  
    }

    // void OnTriggerExit(Collider other)
    // {
        
    // }
}
