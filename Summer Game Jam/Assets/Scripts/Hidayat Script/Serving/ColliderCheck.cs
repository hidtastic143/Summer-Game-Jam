﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    public GameObject WaitingIceCream;

    private void OnCollisionEnter2D(Collision2D currentTouchingGameObject)
    {
        if (WaitingIceCream == null && currentTouchingGameObject.gameObject.CompareTag("Ingredients"))
        {
            WaitingIceCream = currentTouchingGameObject.gameObject;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(),
                WaitingIceCream.gameObject.GetComponent<Collider2D>(), true);

            IceCreamStructure iceCreamMade = new IceCreamStructure();
            if (WaitingIceCream.GetComponent<Cone>() != null)
                iceCreamMade.ConeType.Id = currentTouchingGameObject.transform.GetComponent<Cone>().Id;
            else
                iceCreamMade.ConeType.Id = -1;
            
            // If there are ingredients on top of cone
            if (WaitingIceCream.transform.childCount > 0)
            {
                // Take in Ice Cream Flav
                if (WaitingIceCream.transform.GetChild(0).GetComponent<IceCream_Flav>() != null)
                    iceCreamMade.IceCream_FlavType.Id = WaitingIceCream.transform.GetChild(0).GetComponent<IceCream_Flav>().Id;
                else
                    iceCreamMade.ConeType.Id = -1;

                // If there are any toppings on Ice Cream Flav
                if (WaitingIceCream.transform.GetChild(0).childCount > 0)
                {
                    // Check names
                    for (int i = 0; i < WaitingIceCream.transform.GetChild(0).childCount; i++)
                    {
                        if (WaitingIceCream.transform.GetChild(0).GetChild(i).name == "Syrup")
                        {
                            iceCreamMade.SyrupType.Id = currentTouchingGameObject.transform.GetChild(0).GetChild(i).GetComponent<Syrup>().Id;
                        }
                        else if (WaitingIceCream.transform.GetChild(0).GetChild(i).name == "Sprinkle")
                        {
                            iceCreamMade.SprinkleType.Id = currentTouchingGameObject.transform.GetChild(0).GetChild(i).GetComponent<Sprinkle>().Id;
                        }
                    }
                }

            }
            // If there are no ingredients
            else
            {
                iceCreamMade.SprinkleType.Id = iceCreamMade.IceCream_FlavType.Id = iceCreamMade.SyrupType.Id = -1;
            }
            iceCreamMade.obj = WaitingIceCream;
            GameObject.FindGameObjectWithTag("Game Logic").GetComponent<GameManager>().IceCream = iceCreamMade;
        }
    }

    void OnCollisionStay2D(Collision2D currentTouchingGameObjectStayCollision)
    {
        if (WaitingIceCream == null && currentTouchingGameObjectStayCollision.gameObject.CompareTag("Ingredients"))
        {
            WaitingIceCream = currentTouchingGameObjectStayCollision.gameObject;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), WaitingIceCream.gameObject.GetComponent<Collider2D>(), true);
        }
    }

    public void OrderUp()
    {
        WaitingIceCream = null;
    }

}
