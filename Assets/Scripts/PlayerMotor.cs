using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    private NavMeshAgent meshAgent;
    private Transform target;
    private AnimationController animation;

    private Vector3? currentPoint;

    // Start is called before the first frame update
    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        animation = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(target != null)
        {
            meshAgent.SetDestination(target.position);
            currentPoint = target.position;

            FaceTarget();

        }

        UpdateAnimation();
    }

    public void MoveToPoint(Vector3 point)
    {
        meshAgent.SetDestination(point);
        currentPoint = point;
    }

    public void FollowTarget(Interactable newTarget)
    {
        meshAgent.stoppingDistance = newTarget.radius * .8f;
        meshAgent.updateRotation = false;

        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        meshAgent.stoppingDistance = 0f;
        meshAgent.updateRotation = true;

        target = null;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void UpdateAnimation()
    {
        Debug.Log(meshAgent.destination - currentPoint);
        if (currentPoint == null) return;
        if(meshAgent.destination == currentPoint)
        {
            animation.setWalking(false);
            currentPoint = null;
            return;
        }

        animation.setWalking(true);
    }
}
