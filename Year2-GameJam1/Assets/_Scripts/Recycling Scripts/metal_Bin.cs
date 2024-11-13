using System;
using UnityEngine;

public class metal_Bin : MonoBehaviour
{
    [SerializeField] private quota_Control quota_Script;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Metal"))
            quota_Script.current_Deposited++;
        if (other.CompareTag("Organic"))
            print("bad");
        
        Destroy(other.gameObject);
    }
}
