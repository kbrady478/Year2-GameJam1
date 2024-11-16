using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class end_Control : MonoBehaviour
{
    [SerializeField] private fade_Controller fade_Controller;


    public IEnumerator Start_Ending()
    {
        yield return StartCoroutine(fade_Controller.Fade_To_Black());
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }// end Start_Ending()
    
}// end script
