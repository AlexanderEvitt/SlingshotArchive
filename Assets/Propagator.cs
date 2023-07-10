using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propagator : MonoBehaviour
{
    public Rigidbody Spacecraft;
    public Rigidbody Moon;
    private LineRenderer lr;
    float M = 1f;
    float m = 0.012345f;
    public Vector3 start_vel = Vector3.forward;
    Vector3[] positions = new Vector3[Universe.iteration_length];
    Vector3[] velocities = new Vector3[Universe.iteration_length];
    int t = 0;
    int skip = 1;
    public int velocity;

    // Start is called before the first frame update
    void Start()
    {
        positions[0] = Spacecraft.position;
        velocities[0] = start_vel;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = Universe.iteration_length;


        for (int i = 1; i < Universe.iteration_length; i++)
        {
            Vector3 r_moon = positions[i - 1] - Moon.position;
            float sqrtDist = (positions[i-1]).sqrMagnitude;
            float sqrtDistMoon = r_moon.sqrMagnitude;
            Vector3 accel = positions[i-1].normalized * (Universe.G * M / sqrtDist) + r_moon.normalized * (Universe.G * m / sqrtDistMoon);

            velocities[i] = velocities[i-1] + accel * Universe.time_step;
            positions[i] = positions[i-1] + velocities[i] * Universe.time_step;

            lr.SetPosition(i, positions[i]);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (skip == 1)
                skip = 10;
            else
                skip = 1;
            // Adjust fixed delta time according to timescale
            // The fixed delta time will now be 0.02 real-time seconds per frame
            //Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
        if (t < Universe.iteration_length)
        {
            Spacecraft.position = positions[t];
            t = t + skip;
            velocity = (int)(velocities[t].magnitude);

        }
    }
}
