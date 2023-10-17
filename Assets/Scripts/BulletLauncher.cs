using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BulletLauncher : MonoBehaviour
{
    public GameObject bulletParent;
    public Transform spawnPoint;
    public float bulletVelocity;

    public XRGrabInteractable spawner;


    // Start is called before the first frame update
    void Start()
    {
        spawner.activated.AddListener(Shoot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(ActivateEventArgs args)
    {
        // Spawn bullet
        GameObject bullet = Instantiate(bulletParent);
        bullet.transform.position = spawnPoint.position;
        bullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * bulletVelocity;

        Debug.Log("Pow!");

        //remove bullet
        Destroy(bullet, 10);
    }
}
