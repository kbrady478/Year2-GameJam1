using UnityEngine;

public class Item_Script : MonoBehaviour, IInteractable
{
    private GameObject target_Position;
    private Rigidbody rb;
    public bool being_Held;
    private bool can_Interact = true;
    private float interact_Cooldown = 0.1f;
    private AudioSource audio_Source;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target_Position = GameObject.FindGameObjectWithTag("Hold Point");
        being_Held = false;
        audio_Source = GameObject.FindGameObjectWithTag("Pick_Up_Audio").GetComponent<AudioSource>();
    }// end Start()

    void Update()
    {
        if (being_Held == false)
            return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Stops problem of picking item back up as soon as it is dropped
            can_Interact = false;
            Invoke("Reset_Interact", interact_Cooldown);
            
            being_Held = false;
            rb.isKinematic = false;
            return;
        }        
        
        gameObject.transform.position = Vector3.Lerp(transform.position, target_Position.transform.position, Time.deltaTime * 50);
     
    }// end Update()
    
    public void Interact()
    {
        if (can_Interact == false)
            return;
        
        if (being_Held == false)
        {
            audio_Source.Play();
            being_Held = true;
            rb.isKinematic = true;
        }
        
    }// end Interact()
    
    private void Reset_Interact()
    {
        can_Interact = true;
    }// end Reset_Interact()
    
}// end script
