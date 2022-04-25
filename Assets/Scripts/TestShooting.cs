using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;

public class TestShooting : MonoBehaviour
{
    [SerializeField]
    VisualEffect MuzzleFash;
    // ParticleSystem Bullet;
    public GameObject Bullet;
    Transform spos;
    public float FireRate;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        GameObject obj1 = GameObject.Find("BulletVFX");
        GameObject obj2 = GameObject.Find("ShootPos");

        MuzzleFash = obj1.GetComponent<VisualEffect>();
        spos= obj2.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        timer = timer+=Time.deltaTime;
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(timer > FireRate)
            {
                MuzzleFash.Play();
                Instantiate(Bullet, spos.position, Quaternion.identity);
                timer = 0;
            }
        }
    }
}
