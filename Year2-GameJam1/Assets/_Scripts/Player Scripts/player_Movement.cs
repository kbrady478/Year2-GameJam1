// tutorial used: https://www.youtube.com/watch?v=f473C43s8nE

using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class player_Movement : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private Transform camera_Transform;
    [SerializeField] private AudioSource jetpack_Clip;
    
    private Rigidbody rb;

    [Header("Movement")] 
    [SerializeField] private float max_Move_Speed;
    [SerializeField] private float move_Speed_Increase; // to increase as player holds input
    [SerializeField] private float current_Move_Speed = 0f;
    [SerializeField] private float player_Rotation_Speed;
    
    private Vector3 move_Direction;
    private Input last_Input;
    
    private float horizontal_Input;
    private float vertical_Input;
    private bool audio_Playing = false;

    
    public Vector3 velocity;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }// end Start()

    // fix rotation of game object
   
    
    private void FixedUpdate()
    {
        Calculate_Direction();
        velocity = rb.linearVelocity.normalized;

        // Rotate body in direction of camera
        gameObject.transform.rotation = Quaternion.Euler(camera_Transform.rotation.eulerAngles.x, camera_Transform.rotation.eulerAngles.y, camera_Transform.rotation.eulerAngles.z);
        
        // Reset speed when input is let go, stops forward momentum from increasing backward momentum
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            jetpack_Clip.Pause();
            audio_Playing = false;
            current_Move_Speed = 0;
        }        
        
        // Increase momentum on input if current velocity is below the limit
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if(audio_Playing == false)
            {
                jetpack_Clip.Play();
                audio_Playing = true;
            }
            
            if (velocity.magnitude >= max_Move_Speed)
                rb.linearVelocity = velocity.normalized * max_Move_Speed;  
            else
            {
                Accelerate();
                rb.AddForce(move_Direction.normalized * current_Move_Speed * 10f, ForceMode.Force);
            }
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
    
}// end script
