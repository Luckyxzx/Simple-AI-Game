using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// it's used to track the state the current ai is in by using ENUM it's easier to manage the ai's behavior
public enum FMS
{
    Roaming, Following
}

public class AiMovement : MonoBehaviour
{

    // setting the speed of which the enemy ai will be moving
    public float movementSpeed = 2f;

    // increases the movement speed of the ai
    public float increaseSpeed = 3f;

    // sets the rotation speed of the ai
    public float rotateSpeed = 5f;

    // sets a rotation timer to 0 for default
    private float rotateTimer = 0f;

    // sets a interval for the rotation
    private float rotateInterval = 5f;

    // creating a private transform for me which will be the player and directing the ai to move towards the player
    public Transform me;

    // setting the current state of the ai
    private FMS currentState = FMS.Roaming;

    void Update()
    {
        // switch statement for the ai based on the current state
        switch (currentState)
        {
            // when ai is roaming, initilize Roaming state
            case FMS.Roaming:
            RoamingState();
            break;
            // when ai is following, initilize following state
            case FMS.Following:
            FollowingState();
            break;

        }
  

    }

    private void FollowingState()
    {
        // checking to see if there is a target which is me the player
        // if me not equal to operator null
        if (me != null)
        {
            // vector 3 direction to get a hold of players position and subtracts player position from ai by calculating a direction from ai to player
            Vector3 direction = me.position - transform.position;

            // setting the direction of y for 0 to ignore the y axis so it doesn't jump around
            direction.y = 0f; 

            // with Quaternion it calculates the rototation needed to face the player and with Quaternion.Slerp to make sure that the AI rotates smoothly and doesn't snap instantly
            // line of code has some issue with 'Look rotation viewing vector is zero' provided by Unity console - reminder to me
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

            // vector3 movement to calculate the movement needed towards the player me
            Vector3 movement = direction.normalized * movementSpeed * Time.deltaTime;

            // movement space world to move the ai towards the player using space.world https://docs.unity3d.com/ScriptReference/Space.World.html
            transform.Translate(movement, Space.World);
        }
    }

    private void RoamingState()
    {
        //rotation timer -= time delta to decrease the rotate timer
        rotateTimer -= Time.deltaTime;

        // runs a check to see if it's the right time to be changing the rotation so it doesn't go one direction
        if (rotateTimer <= 0f)
        {
            //randomly changes the direction of the ai
            RandRotate();
            // resets the timer for rotation
            rotateTimer = rotateInterval;
        }

        // be using vect3 movement it calculates the forward direction movement of the ai
        Vector3 movement = transform.forward * movementSpeed * Time.deltaTime;

        // movement space world to move the ai towards the player using space.world https://docs.unity3d.com/ScriptReference/Space.World.html
        transform.Translate(movement, Space.World);
    }


    private void RandRotate()
    {
        // by using the Y axis it will generate a random rotation around it
        Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        // it sets the rotation of the ai to the random rotation
        transform.rotation = randomRotation;
        
    }

    // sets up the player so when within the radius set from maxrange the ai will start following the player which is me
    public void SetMe(Transform meTransform)
    {
        me = meTransform;
        currentState = FMS.Following;
    }

    // clears the player so the ai will stop following the player 24/7
    public void ClearMe()
    {
        me = null;
        currentState = FMS.Roaming;
        
    }
    

}