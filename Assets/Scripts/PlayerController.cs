using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Control Settings")]
    private float clickedScreenX;
    private float clickedPlayerX;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxX;

    void Start()
    {
        
    }


    void Update()
    {
        ManageControl();
    }

    private void ManageControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenX = Input.mousePosition.x;
            clickedPlayerX = transform.position.x; // storing the position x of the player, this includes the updated position.
        }

        else if (Input.GetMouseButton(0))
        {
            float xDifference = Input.mousePosition.x - clickedScreenX;

            xDifference /= Screen.width;  // every device has its own width with pixels and you want your game to be responsive, so you need to divide it by the width of the screen.
                                          // same with device height.
            xDifference *= moveSpeed;

            float newXPosition = clickedPlayerX + xDifference; // Adding the difference to the updated state.

            newXPosition = Mathf.Clamp(newXPosition, -maxX, maxX); // declaring the bounds for the player movement.

            transform.position = new Vector2(newXPosition, transform.position.y); // storing the new position and updating it.

            //Debug.Log("X Difference =  " + xDifference);
        }
    }


}
