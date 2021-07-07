using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWeaponPickup : MonoBehaviour
{
    // Player Game Object
    public GameObject player;

    // Array to hold all the guns
    public WeaponClass[] weapons;
    public void Update()
    {
        Debug.Log(player.transform.forward);
    }
    public void OnTriggerEnter(Collider other)
    {
        // Three guns created so far
        // Standard backwards
        if (other.gameObject.CompareTag("Rifle"))
        {
            // Destroy the current weapon the player is using
            Destroy(GameObject.FindWithTag("CurrentWeapon"));

            // Replace it with the appropriate weapon
            player.GetComponent<Pawn>().weapon = weapons[0];

            // Make the player its new parent
            other.transform.SetParent(player.transform);

            // Put the gun in the player's hands
            other.transform.localPosition = new Vector3(.265f, 1.149f, .494f);

            // Change the tag so that the next weapon is ready to be picked up
            other.tag = "CurrentWeapon";
        }

        // Backwards but thrice
        else if (other.gameObject.CompareTag("3-Gun"))
        {
            Destroy(GameObject.FindWithTag("CurrentWeapon"));
            player.GetComponent<Pawn>().weapon = weapons[1];
            other.transform.SetParent(player.transform);
            other.transform.localPosition = other.transform.forward;
            other.transform.localRotation = new Quaternion(0, 0, 0, 0);
            other.tag = "CurrentWeapon";
        }

        // Forwards, but will shoot a dud every other bullet.
        else if (other.gameObject.CompareTag("Random"))
        {
            Destroy(GameObject.FindWithTag("CurrentWeapon"));
            player.GetComponent<Pawn>().weapon = weapons[2];
            other.transform.SetParent(player.transform);
            other.transform.localPosition = new Vector3(.265f, 1.149f, .494f);
            other.tag = "CurrentWeapon";
        }
    }
}
