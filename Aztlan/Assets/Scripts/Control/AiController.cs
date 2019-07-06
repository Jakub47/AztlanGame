using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aztlan.Control
{
    public class AiController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; //What distance they should attack 

        private void Update()
        {
            GameObject player =  GameObject.FindWithTag("Player");
            if(Vector3.Distance(player.transform.position, gameObject.transform.position) <= chaseDistance)
            {
                print("Object is " + gameObject.name);
            }
        }
    }
}