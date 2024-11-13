using UnityEngine;

interface IInteractable
{
    public void Interact();
}

public class player_Interactor : MonoBehaviour
{
    public Transform interactor_Source;
    public float interact_Range;

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode.E)))
        {
            Ray ray = new Ray(interactor_Source.position, interactor_Source.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interact_Range))
            {
                if (hit.collider.gameObject.TryGetComponent(out IInteractable interact_Obj))
                    interact_Obj.Interact();
            }
        }
    }
}
