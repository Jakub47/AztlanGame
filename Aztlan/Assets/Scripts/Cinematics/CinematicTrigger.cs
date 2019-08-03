using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Aztlan.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool isTrigeredOnce;
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player" && !isTrigeredOnce)
            {
                GetComponent<PlayableDirector>().Play();
                isTrigeredOnce = true;
            }
                
        }
    }
}