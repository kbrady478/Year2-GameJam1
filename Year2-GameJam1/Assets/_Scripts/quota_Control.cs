using System;
using UnityEngine;

public class quota_Control : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private alien_Dialogue dialogue_Script;
    [SerializeField] private int quota;
    
    [Header("For Display - Do Not Change")]
    [SerializeField] private int amount_Of_Objects_Left;
    [SerializeField] private int current_Deposited;

    
    private void Start()
    {
        GameObject[] metals = GameObject.FindGameObjectsWithTag("Metal");
        foreach (GameObject metal in metals)
            amount_Of_Objects_Left++;
        
        GameObject[] organics = GameObject.FindGameObjectsWithTag("Organic");
        foreach (GameObject organic in organics)
            amount_Of_Objects_Left++;
    }// end Start()
    
    public void Increase_Quota()
    {
        current_Deposited++;
        amount_Of_Objects_Left--;
        
        if (current_Deposited >= quota)
            dialogue_Script.Start_Dialogue("Good Ending");
    }// end Increase_Quota()

    public void Decrease_Available()
    {
        amount_Of_Objects_Left--;

        if (amount_Of_Objects_Left < quota)
        {
            dialogue_Script.Start_Dialogue("Bad Ending");
        }
        else
            dialogue_Script.Start_Dialogue("Angry");
    }// end Decrease_Available()
    
}// end script
