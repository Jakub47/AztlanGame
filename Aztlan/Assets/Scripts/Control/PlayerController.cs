﻿using Aztlan.Combat;
using Aztlan.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aztlan.Control
{
    public class PlayerController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
           RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.gameObject.GetComponent<CombatTarget>();
                if (target != null)
                {
                    if(Input.GetMouseButtonDown(0))
                    {
                        GetComponent<Fighter>().Attack(target);
                    }
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            bool hasHit = Physics.Raycast(GetMouseRay(), out RaycastHit hit);
            if (hasHit)
            {
                if(Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().MoveTo(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
