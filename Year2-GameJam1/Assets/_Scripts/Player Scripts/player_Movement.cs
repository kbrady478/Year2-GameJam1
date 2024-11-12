// tutorial used: https://www.youtube.com/watch?v=f473C43s8nE

using System;
using System.Collections;
using UnityEngine;

public class player_Movement : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private Transform camera_Transform;
    
    private Rigidbody rb;

    [Header("Movement")] 
    [SerializeField] private float max_Move_Speed;
    [SerializeField] private float move_Speed_Increase; // to increase as player holds input
    [SerializeField] private float move_Speed_Decrease; // to decrease after player lets go of input
    [SerializeField] private float current_Move_Speed = 0f;
    
    private Vector3 move_Direction;
    
    private float horizontal_Input;
    private float vertical_Input;
    private bool is_Inputing;
    private bool is_Moving;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        is_Inputing = false;
        is_Moving = false;
    }// end Start()

    // fix rotation of game object
    // remove decel? maybe reuse
    // fix movement speed carrying over between axis (going forward accelerates your speed when going back
    
    
    private void FixedUpdate()
    {
        Calculate_Direction();
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            is_Inputing = true;
            is_Moving = true;
            Accelerate();
            rb.AddForce(move_Direction.normalized * current_Move_Speed * 10f, ForceMode.Force);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            is_Inputing = false;
            
        }

        if (is_Inputing == false && is_Moving == true)
        {
            Decelerate();
            rb.AddForce(-move_Direction.normalized * current_Move_Speed * 10f, ForceMode.Force);
        }

    }// end FixedUpdate()

    private void Calculate_Direction()
    {
        horizontal_Input = Input.GetAxisRaw("Horizontal");
        vertical_Input = Input.GetAxisRaw("Vertical");
        
        // Calculate movement direction
        move_Direction = camera_Transform.forward * vertical_Input + camera_Transform.right * horizontal_Input;
        
    }// end Calculate_Direction()

    private void Accelerate()
    {
        if (current_Move_Speed == max_Move_Speed)
            return;

        if(current_Move_Speed > max_Move_Speed)
            current_Move_Speed = max_Move_Speed;
        
        else if (current_Move_Speed < max_Move_Speed)
            current_Move_Speed += move_Speed_Increase * Time.deltaTime;
        
    }// end Accelerate()

    private void Decelerate()
    {
        if (current_Move_Speed <= 0)
        {
            current_Move_Speed = 0;
            is_Moving = false;
        }
        
        if (current_Move_Speed > 0)
            current_Move_Speed -= move_Speed_Decrease * Time.deltaTime;
        
    }// end Decelerate()
    
}// end script
