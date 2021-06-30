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
        
        healthBar.SetMaxHealth(maxHealth);

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        


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
        if (this.GetComponent<PlayerController>() != null)
        {
            // If they are hit by an enemy bullet...
            if (other.CompareTag("EnemyBullet"))
            {
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
