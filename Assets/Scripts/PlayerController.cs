using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private Camera viewCamera;
    private PlayerMotor motor;

    public Interactable focus;

    [SerializeField] private LayerMask movementMask;

    // Start is called before the first frame update
    private void Start()
    {
        viewCamera = Camera.main;
        motor = GetComponent<PlayerMotor>();
       
    }

    // Update is called once per frame
    private void Update()
    {
        CheckMovement();
        CheckFocus();
    }

    private void CheckMovement()
    {
        if (!Input.GetMouseButton(0)) {
            return;
        }

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 100, movementMask)) return;
        // Move Our player to what we hit

        motor.MoveToPoint(hit.point);
        RemoveFocus();
    }

    private void CheckFocus()
    {
        if (!Input.GetMouseButton(1)) return;

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 100)) return;

        Interactable interactable = hit.collider.GetComponent<Interactable>();
        if (interactable == null) return;

        SetFocus(interactable);
    }

    private void SetFocus(Interactable newFocus)
    {
        if (focus != null) focus.OnDefocused(); 

        focus = newFocus;
        motor.FollowTarget(focus);
        focus.onFocused(transform);
    }

    private void RemoveFocus()
    {
        if (focus != null) focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }
}
