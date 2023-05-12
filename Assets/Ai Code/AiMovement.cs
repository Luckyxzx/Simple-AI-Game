using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{

    // setting the speed of which the enemy ai will be moving
    public float movementSpeed = 4f;

    // increases the movement speed of the ai
    public float increaseSpeed = 6f;

    // sets the rotation speed of the ai
    public float rotateSpeed = 5f;

    // creating a private transform for me which will be the player and directing the ai to move towards the player
    public Transform me;

    void Update()
    {
        // Check if there is a target
        if (me != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = me.position - transform.position;
            direction.y = 0f; // ignore y-axis

            // Calculate the rotation towards the target
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Rotate the AI towards the target
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

            // Calculate the movement towards the target
            Vector3 movement = direction.normalized * movementSpeed * Time.deltaTime;

            // Move the AI towards the target
            transform.Translate(movement, Space.World);
        }
    }

    // Set the target of the AI
    public void SetMe(Transform meTransform)
    {
        me = meTransform;
    }

    // Clear the target of the AI
    public void ClearMe()
    {
        me = null;
        
    }
    

}