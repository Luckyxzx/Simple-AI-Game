using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class AiState : MonoBehaviour
{
    // assign me object
    public Transform me;

    // obtains the stuff i need from AiMovement.cs
    private AiMovement aiMovement;

    // set the current target the ai is targeting
    private Transform currentMe = null;

    // set the max range of which the ai will follow me
    private float maxRange = 8.5f;

    // calling upon start before the first frame update
    protected virtual void Start()
    {
        aiMovement = GetComponent<AiMovement>();
        // enable input
        InputSystem.EnableDevice(Keyboard.current);

        // sets currentme to me (player)
        currentMe = me;
    }
    
    // calling upon the update once per frame
    protected virtual void Update()
    {
        // check if target is in range if so
        if (Vector3.Distance(transform.position, me.position) < maxRange)
        {
            //Debug.Log(" Target Locked ");
            // set the target for the ai
            aiMovement.SetMe(currentMe);
            currentMe = me.transform;
        }
        else
        {
            //Debug.Log(" Target Not Locked");
            // if the player model is out of range it will stop targeting
            aiMovement.ClearMe();
            currentMe = null;
            
        }


        // this code sole purpose is to use for testing nothing else really
        // check for Arrow Key pressed P
        if (Keyboard.current.pKey.wasPressedThisFrame)       
        {
            // set current me to null
            // once p is pressed the ai will start running towards player with the max range of 20 and pressing it again will set the range back to default
            if (currentMe == null)
            {
                SetMe(me);
                maxRange = 20f;
            }
            else
            {
                aiMovement.ClearMe();
                currentMe = null;
                maxRange = 8.5f;
            }
        }

    }

    // sets me to me
    public void SetMe(Transform me)
    {
        currentMe = me;
    }
}
