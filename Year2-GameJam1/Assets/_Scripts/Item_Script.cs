using UnityEngine;

public class Item_Script : MonoBehaviour, IInteractable
{
    private GameObject target_Position;
    private Rigidbody rb;
    public bool being_Held;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target_Position = GameObject.FindGameObjectWithTag("Hold Point");
        being_Held = false;
    }// end Start()

    void Update()
    {
        if(being_Held)
            gameObject.transform.position = Vector3.Lerp(transform.position, target_Position.transform.position, Time.deltaTime * 10);
     
        if (being_Held && Input.GetKeyDown(KeyCode.E))
        {
            being_Held = false;
            rb.isKinematic = false;
        }
    }// end Update()


    public void Interact()
    {
        if (being_Held == false)
        {
            being_Held = true;
            rb.isKinematic = true;
        }
        
    }// end Interact()
    
    
}// end script
