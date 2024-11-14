using System;
using System.Collections;
using UnityEngine;

public class boundary_Control : MonoBehaviour
{
    [SerializeField] private fade_Controller fader;
    [SerializeField] private alien_Dialogue dialogue;
    [SerializeField] private quota_Control quota;
    [SerializeField] private Rigidbody player_Controller;
    [SerializeField] private Transform reset_Point;
    

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag + " has left the boundary ");
        if (other.tag == "Player")
        {
            StartCoroutine(Reset_Player());
        }
        else if (other.tag == "Metal" || other.tag == "Organic")
            quota.amount_Of_Objects_Left--;
    }

    private IEnumerator Reset_Player()
    {
        Debug.Log("Resetting the player");
        yield return StartCoroutine(fader.Fade_To_Black());
        
        player_Controller.MovePosition(reset_Point.position);
        dialogue.Start_Dialogue("Out Of Bounds");

        yield return StartCoroutine(fader.Fade_From_Black());

        Debug.Log("Reset complete");
        
        yield return null;
    }
    
}// end script
