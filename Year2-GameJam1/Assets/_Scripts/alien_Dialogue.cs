// Tutorial used: https://www.youtube.com/watch?v=8oTYabhj248

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;


public class alien_Dialogue : MonoBehaviour, IInteractable
{
    [Header("General")] 
    // To disable scripts during dialogue
    [SerializeField] private GameObject player_Controller; 
    private player_Interactor player_Interactor_Script;
    private player_Movement player_Movement_Script;
    [SerializeField] private Camera player_Camera;
    private camera_Controller camera_Controller_Script;
    // Components for functional dialogue
    [SerializeField] private GameObject dialogue_Box;
    [SerializeField] private GameObject dialogue_Box_Background; // A repeat of object above to make darker, not needed in future projects
    [SerializeField] private TextMeshProUGUI text_Component;
    [SerializeField] private float text_Speed;

    [Header("Dialogue")] // Dialogue types, contain individual lines
    [SerializeField] private string[] starting_Dialogue;
    [SerializeField] private string[] good_Ending_Dialogue;
    [SerializeField] private string[] bad_Ending_Dialogue;
    [SerializeField] private string[] angry_Dialogue;
    [SerializeField] private string[] interact_Dialogue;
    [SerializeField] private string[] out_Of_Bounds_Dialogue;
    
    private string[][] dialogue = new string[6][]; // Array for dialogue types
    
    [Header("For Display - Do Not Change")] // To iterate through dialogue type and line
    public int current_Dialogue_String_I = 0; // the current string array listed above
    public int current_Dialogue_Line_I = 0; // the current string within that array 
    
    // Checks for script enabling
    private bool in_Dialogue = false;
    private bool intro_Finished = false;
    
    
    void Start()
    {
        text_Component.text = string.Empty;

        player_Interactor_Script = player_Controller.GetComponent<player_Interactor>();
        player_Movement_Script = player_Controller.GetComponent<player_Movement>();
        camera_Controller_Script = player_Camera.GetComponent<camera_Controller>();
        
        dialogue[0] = starting_Dialogue;
        dialogue[1] = good_Ending_Dialogue;
        dialogue[2] = bad_Ending_Dialogue;
        dialogue[3] = angry_Dialogue;
        dialogue[4] = interact_Dialogue;
        dialogue[5] = out_Of_Bounds_Dialogue;
        
        Start_Dialogue("Start Dialogue");
    }// end Start()

    // Only used when in_Dialogue = true
    void Update()
    {
        if (in_Dialogue == false)
            return;
        
        player_Interactor_Script.enabled = false;
        player_Movement_Script.enabled = false;
        camera_Controller_Script.enabled = false;
        
        // Face player towars alien
        Vector3 direction_To_Alien = transform.position - player_Camera.transform.position;
        Quaternion target_Rotation = Quaternion.LookRotation(direction_To_Alien);
        player_Camera.transform.rotation = Quaternion.RotateTowards(player_Camera.transform.rotation, target_Rotation, text_Speed * 350 * Time.deltaTime); 
        
        // Get input to skip/move on
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            if (text_Component.text == dialogue[current_Dialogue_String_I][current_Dialogue_Line_I])
            {
                Next_Line();
            }

            else
            {
                StopAllCoroutines();
                text_Component.text = dialogue[current_Dialogue_String_I][current_Dialogue_Line_I];
            }
        }
        
    }// end Update()

    public void Start_Dialogue(string dialogue_Type)
    {
        in_Dialogue = true;
        
        // Assign the correct array
        if (dialogue_Type == "Start Dialogue")
            current_Dialogue_String_I = 0;
        if (dialogue_Type == "Good Ending")
            current_Dialogue_String_I = 1;
        if (dialogue_Type == "Bad Ending")
            current_Dialogue_String_I = 2;
        if (dialogue_Type == "Angry")
            current_Dialogue_String_I = 3;
        if (dialogue_Type == "Interact")
            current_Dialogue_String_I = 4;
        if (dialogue_Type == "Out Of Bounds")
            current_Dialogue_String_I = 5;
        
        current_Dialogue_Line_I = 0;
        
        dialogue_Box.SetActive(true);
        dialogue_Box_Background.SetActive(true);
        StartCoroutine(Type_Line());
    }// end Start_Dialogue()

    IEnumerator Type_Line()
    {
        // for each letter in the current line of the current dialogue
        foreach (char c in dialogue[current_Dialogue_String_I][current_Dialogue_Line_I].ToCharArray())
        {
            text_Component.text += c;
            yield return new WaitForSeconds(text_Speed);
        }
        
    }// end Type_Line()

    private void Next_Line()
    {
        if (current_Dialogue_Line_I < dialogue[current_Dialogue_String_I].Length - 1)
        {
            current_Dialogue_Line_I++;
            text_Component.text = string.Empty;
            StartCoroutine(Type_Line());
        }
        // Ending + resetting dialogue
        else
        {
            if (current_Dialogue_String_I == 0 && intro_Finished == false)
                intro_Finished = true;
                
            current_Dialogue_Line_I = 0;
            dialogue_Box.SetActive(false);
            dialogue_Box_Background.SetActive(false);

            text_Component.text = string.Empty;
            in_Dialogue = false;
            player_Interactor_Script.enabled = true;
            player_Movement_Script.enabled = true;
            camera_Controller_Script.enabled = true;
        }
    }// end Next_Line()
    
    public void Interact()
    {
        Start_Dialogue("Interact");
    }// end Interact()
    
}// end script
