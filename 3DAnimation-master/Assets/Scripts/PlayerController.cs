using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]public Pawn pawn;
    private Camera cam;
    private Animator anim;
    [SerializeField, Tooltip("The speed the player moves.")] private float speed;
    [SerializeField, Tooltip("The speed the player turns in degrees/second.")] private float turnSpeed;
    public int runSpeed;
    public int walkSpeed;

    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pawn = GetComponent<Pawn>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get direction of input
        Vector3 stickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Controls cap of thumbstick direction
        stickDirection = Vector3.ClampMagnitude(stickDirection, 1);

        //Invert movement to world based and not local
        Vector3 animationDirection = transform.InverseTransformDirection(stickDirection);

        //Shift Key to run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }

        else
            speed = walkSpeed;

        //Convert input into the correct animation in the animator
        anim.SetFloat("Forward", animationDirection.z * speed);
        anim.SetFloat("Right", animationDirection.x * speed);

        if (!isDead)
        {
            if (cam != null)
            {
                RotateToMousePointer();
            }
        }
    }
    /// <summary>
    /// Find the mouse pointer in relation to the screen and faces the player towards it.
    /// </summary>
    public void RotateToMousePointer()
    {
        
        //Find game plane
        Plane gamePlane = new Plane(Vector3.up, transform.position);

        //Draw ray from mouse towards game plane
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

        //Using distance to intersection, find point in world space
        float distance;
        gamePlane.Raycast(mouseRay, out distance);
        Vector3 targetPoint = mouseRay.GetPoint(distance);

        //Rotate towards point
        RotateTowards(targetPoint);

    }

    /// <summary>
    /// Rotates an object towards a given target.
    /// </summary>
    /// <param name="lookAtPoint"></param>
    public void RotateTowards(Vector3 lookAtPoint)
    {
        //Find rotation to look at target
        Quaternion goalRotation;
        goalRotation = Quaternion.LookRotation(lookAtPoint - transform.position, Vector3.up);

        //Rotate slighty towards goal
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, turnSpeed * Time.deltaTime);
    }
}
