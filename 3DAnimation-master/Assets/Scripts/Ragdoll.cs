using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Animator anim;
    private Collider topCol;
    private Rigidbody topRb;
    private List<Collider> ragdollCols;
    private List<Rigidbody> ragdollRbs;

    private PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        topCol = GetComponent<Collider>();
        topRb = GetComponent<Rigidbody>();
        ragdollCols = new List<Collider>(GetComponentsInChildren<Collider>());
        ragdollRbs = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());

        ragdollCols.Remove(topCol);
        ragdollRbs.Remove(topRb);

        pc = GetComponent<PlayerController>();
        StopRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartRagdoll();
            pc.isDead = true;
            pc.GetComponentInChildren<WeaponClass>().isDead = true;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            StopRagdoll();
            pc.isDead = false;
            pc.GetComponentInChildren<WeaponClass>().isDead = false;
        }
    }

    public void StartRagdoll()
    {
        //Turn off animator
        anim.enabled = false;
        //Turn off big collider
        topCol.enabled = false;
        //Turn off rigidbody
        topRb.isKinematic = true;

        //Turn on ragdoll colliders
        foreach (Collider currentCol in ragdollCols)
        {
            currentCol.enabled = true;
        }
        //Turn on ragdoll rigidbodies
        foreach (Rigidbody currentRb in ragdollRbs)
        {
            currentRb.isKinematic = false;
        }
    }

    public void StopRagdoll()
    {
        //Turn on animator
        anim.enabled = true;
        //Turn on big collider
        topCol.enabled = true;
        //Turn on rigidbody
        topRb.isKinematic = false;

        //Turn off ragdoll colliders
        foreach (Collider currentCol in ragdollCols)
        {
            currentCol.enabled = false;
        }
        //Turn off ragdoll rigidbodies
        foreach (Rigidbody currentRb in ragdollRbs)
        {
            currentRb.isKinematic = true;
        }
    }
}
