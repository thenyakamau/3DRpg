using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void setWalking(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }
}
