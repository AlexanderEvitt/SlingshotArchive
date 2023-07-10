using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    public Rigidbody Moon;
    private LineRenderer lr;
    public int time_step = 10;
    public int moon_height = 38250;
    int t = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = (t * 2 * Mathf.PI)/2358720;
        Vector3 pos = new Vector3(moon_height*Mathf.Cos(angle),0, moon_height * Mathf.Sin(angle));
        Moon.position = pos;
        t++;
    }
}
