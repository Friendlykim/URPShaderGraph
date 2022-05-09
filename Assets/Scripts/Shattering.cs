using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shattering : MonoBehaviour
{
    public GameObject FractureObj;


        private void OnTriggerEnter(Collider other)
    {
        Instantiate(FractureObj, this.transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

}
