using System;
using UnityEngine;

public class metal_Bin : MonoBehaviour
{
    [SerializeField] private quota_Control quota_Script;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Metal"))
            quota_Script.Increase_Quota();
        if (other.CompareTag("Organic"))
            quota_Script.Decrease_Available();
        
        Destroy(other.gameObject);
    }
}
