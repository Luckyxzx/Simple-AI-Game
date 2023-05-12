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

    // Start is called before the first frame update
    void Start()
    {
        aiMovement = GetComponent<AiMovement>();
        // enable input
        InputSystem.EnableDevice(Keyboard.current);

        //
        currentMe = me;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        // check if target is in range if so
        if (Vector3.Distance(transform.position, me.position) < maxRange)
        {
            //Debug.Log(" Target Locked to");
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

        //check for Arrow Key pressed
        if (Keyboard.current.pKey.wasPressedThisFrame)       
        {
            // set current me to null
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

    public void SetMe(Transform me)
    {
        currentMe = me;
    }
}
