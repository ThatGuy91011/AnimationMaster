using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pawn : MonoBehaviour
{
    private Animator anim;
    [Header("Movement Settings")]
    [SerializeField, Tooltip("The speed the player moves.")] private float speed;
    [SerializeField, Tooltip("The speed the player turns in degrees/second.")] private float turnSpeed;
    private Camera cam;

    public int runSpeed;
    public int walkSpeed;


    public WeaponClass weapon;

    public HealthBar healthBar;

    public int maxHealth;

    public int currentHealth;

    public int hurtHealth;

    public int healHealth;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        healthBar.SetMaxHealth(maxHealth);

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
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

        if (cam != null)
        {
            RotateToMousePointer();
        }


        // If the player's health is greater than the max health...
        if (currentHealth > maxHealth)
        {
            // Reset it to the max health.
            currentHealth = maxHealth;
        }
        // If the player loses all their health...
        if (currentHealth == 0)
        {
            // Quit the game.
            Application.Quit();
        }
        // Constantly set the health bar to the current health.
        healthBar.SetHealth(currentHealth);
    }

    // When the player interacts with something...
    private void OnTriggerEnter(Collider other)
    {
        // If they are hit by a bullet...
        if (other.CompareTag("EnemyBullet"))
        {
            Debug.Log("Shot");
            // ...and it didn't come from themselves...
            //if (other.GetComponentInParent<Pawn>() == null)
            {
                // ...the player loses health.
                currentHealth -= 2;
                // Destroy the bullet.
                Destroy(other.gameObject);
            }
        }
        // Otherwise, if it was a health kit...
        if (other.CompareTag("HealthKit"))
        {
            // Heal thy self
            currentHealth += healHealth;
            // Destroy the med kit
            Destroy(other.gameObject);
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

    public void AddToScore(float pointsToAdd)
    {
        //TODO: Add score
    }

    // Finds the right and left sides of the current weapon and moves the player's hands to those points.
    public void OnAnimatorIK(int layerIndex)
    {
        if (weapon != null)
        {
            if (weapon.rightHandPoint != null)
            {
                anim.SetIKPosition(AvatarIKGoal.RightHand, weapon.rightHandPoint.position);
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                anim.SetIKRotation(AvatarIKGoal.RightHand, weapon.rightHandPoint.rotation);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }

            if (weapon.leftHandPoint != null)
            {
                anim.SetIKPosition(AvatarIKGoal.LeftHand, weapon.leftHandPoint.position);
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, weapon.leftHandPoint.rotation);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
            }
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }
    }
}
