using System;
using UnityEngine;

public class quota_Control : MonoBehaviour
{
    [SerializeField] private int quota;
    public int current_Deposited;

    private void Update()
    {
        if (current_Deposited >= quota)
            print("you win");
    }
    
}
