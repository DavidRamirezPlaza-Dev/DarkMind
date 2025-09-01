using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraScript : MonoBehaviour
{
    
    public GameObject Dark;
    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Dark.transform.position.x;
        position.y = Dark.transform.position.y;
        transform.position = position;
    }
}
