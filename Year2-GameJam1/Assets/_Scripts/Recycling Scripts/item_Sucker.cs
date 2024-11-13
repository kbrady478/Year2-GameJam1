using System;
using UnityEngine;

public class item_Sucker : MonoBehaviour
{
    [SerializeField] private Transform target; // where the item will get sucked to (in end collider)
    private GameObject current_Item;
    private Item_Script current_Items_Script;

    private void Update()
    {
        if (current_Item == null)
            return;
        if (current_Items_Script.being_Held)
            return;
            
        current_Item.transform.position = Vector3.MoveTowards(current_Item.transform.position, target.position, 5f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Metal") || other.CompareTag("Organic"))
        {
            current_Item = other.gameObject;
            current_Items_Script = current_Item.GetComponent<Item_Script>();
        }// end if target object
        
    }// end OnTriggerEnter()
    
}// end script
