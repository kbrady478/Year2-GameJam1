using System;
using UnityEngine;
using UnityEngine.UI;

public class quota_Control : MonoBehaviour
{
    [Header("General")] 
    [SerializeField] private GameObject quota_Bar;
    private Slider quota_Slider;
    [SerializeField] private alien_Dialogue dialogue_Script;
    [SerializeField] private float quota;
    
    [Header("For Display - Do Not Change")]
    public int amount_Of_Objects_Left;
    [SerializeField] private float current_Deposited = 0;

    
    private void Start()
    {
        GameObject[] metals = GameObject.FindGameObjectsWithTag("Metal");
        foreach (GameObject metal in metals)
            amount_Of_Objects_Left++;
        
        GameObject[] organics = GameObject.FindGameObjectsWithTag("Organic");
        foreach (GameObject organic in organics)
            amount_Of_Objects_Left++;
        
        quota_Slider = quota_Bar.GetComponent<Slider>();
        quota_Slider.value = current_Deposited;
    }// end Start()

    public void Increase_Quota()
    {
        current_Deposited++;
        amount_Of_Objects_Left--;
        quota_Slider.value = (current_Deposited/quota);
        
        if (current_Deposited >= quota)
            dialogue_Script.Start_Dialogue("Good Ending");
    }// end Increase_Quota()

    public void Decrease_Available()
    {
        amount_Of_Objects_Left--;
        
        if (amount_Of_Objects_Left < quota)
            dialogue_Script.Start_Dialogue("Bad Ending");
        else
            dialogue_Script.Start_Dialogue("Angry");
    }// end Decrease_Available()
    
}// end script
