using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class GetAppleCheck : MonoBehaviour
{
    MainScript obj;

    // Start is called before the first frame update
    void Start()
    {
        obj = FindObjectOfType<MainScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Apple")
        {
            obj.Flag = !obj.Flag;
        }
        Destroy(other.gameObject);
    }

}
