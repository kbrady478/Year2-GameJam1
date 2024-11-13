using UnityEngine;

public class organic_Bin : MonoBehaviour
{
    [SerializeField] private quota_Control quota_Script;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Organic"))
            quota_Script.current_Deposited++;
        if (other.CompareTag("Metal"))
            print("bad");
        
        Destroy(other.gameObject);
    }
}
