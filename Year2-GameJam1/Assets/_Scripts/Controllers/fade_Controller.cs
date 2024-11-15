using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class fade_Controller : MonoBehaviour
{
    [SerializeField] private Image fade_Image;

    [SerializeField] private float fade_Duration;

    [HideInInspector] public bool is_Fading = false;
    
    void Start()
    {
        fade_Image.color = new Color(0, 0, 0, 1);
        StartCoroutine(Fade_From_Black());
    }

    public IEnumerator Fade_From_Black()
    {
        Debug.Log("Fade_From_Black");
        is_Fading = true;
        
        float elapsed_Time = 0;

        while (elapsed_Time < fade_Duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed_Time / fade_Duration);
            fade_Image.color = new Color(0, 0, 0, alpha);
            elapsed_Time += Time.deltaTime;
            yield return null;
        }
        
        fade_Image.color = new Color(0, 0, 0, 0f);
        is_Fading = false;
        yield  return new WaitForSeconds(0.5f);

        yield return true;

    }// end Fade_From_Black
  
    public IEnumerator Fade_To_Black()
    {
        Debug.Log("Fade_To_Black");
        is_Fading = true;
        
        float elapsed_Time = 0;

        while (elapsed_Time < fade_Duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed_Time / fade_Duration);
            fade_Image.color = new Color(0, 0, 0, alpha);
            elapsed_Time += Time.deltaTime;
            yield return null;
        }
        
        fade_Image.color = new Color(0, 0, 0, 1f);
        is_Fading = false;
        yield  return new WaitForSeconds(0.5f);

        yield return true;

    }// end Fade_To_Black
    
    
}// end script
