
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public bool isFocus = false;

    private Transform player;

    [SerializeField] public Transform interactionTransform;

    private bool hasInteracted = false;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    public virtual void Interact()
    {
        // This method is meant to be overwritten;
        Debug.Log("Interacting with" + interactionTransform.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public void onFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;

        hasInteracted = false;
    }
}
