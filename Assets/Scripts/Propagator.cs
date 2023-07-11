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
    int t = 1;
    int trailer = 0;
    public int skip = 1;
    public int velocity;

    // Start is called before the first frame update
    void Start()
    {
        positions[0] = Spacecraft.position;
        velocities[0] = start_vel;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = Universe.iteration_length;
        lr.SetPosition(0, positions[0]);


        for (int i = 1; i < Universe.iteration_length; i++)
        {

            Vector3 accel = acceleration(i-1);
            velocities[i] = velocities[i-1] + accel * Universe.time_step;
            positions[i] = positions[i-1] + velocities[i] * Universe.time_step;

            lr.SetPosition(i, positions[i]);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("."))
        {
            skip = skip + 1;
        }
        if (Input.GetKeyDown(","))
        {
            skip = skip - 1;
            if (skip <= 0)
            {
                skip = 0;
            }
        }
        
        if (skip > 0)
        {
            Spacecraft.position = positions[t % Universe.iteration_length];
            velocity = (int)(1000 * (velocities[t % Universe.iteration_length].magnitude));
            Spacecraft.rotation = Quaternion.LookRotation(velocities[t % Universe.iteration_length].normalized) * Quaternion.Euler(90,0,0);
        }
        
        while (trailer <= t)
        {
            int trm = (trailer - 1) % Universe.iteration_length;
            if (trm < 0)
            {
                trm = Universe.iteration_length - 1;
            }
            int tr = (trailer) % Universe.iteration_length;

            Vector3 accel = acceleration(trm);
            velocities[tr] = velocities[trm] + accel * Universe.time_step;
            positions[tr] = positions[trm] + velocities[tr] * Universe.time_step;
            
            lr.positionCount = lr.positionCount + 1;
            lr.SetPosition(lr.positionCount - 1, positions[tr]);

            trailer = trailer + 1;
        }       

        t = t + skip;
    }

    Vector3 acceleration(int k)
    {
        Vector3 moon_position = GameObject.Find("Moon").GetComponent<MoonOrbit>().moon_place(k);
        Vector3 r_moon = positions[k] - moon_position;
        float sqrtDist = (positions[k]).sqrMagnitude;
        float sqrtDistMoon = r_moon.sqrMagnitude;
        Vector3 accel = positions[k].normalized * (Universe.G * M / sqrtDist) + r_moon.normalized * (Universe.G * m / sqrtDistMoon);
        return accel;
    }
}
