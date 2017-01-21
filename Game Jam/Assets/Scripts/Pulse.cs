using UnityEngine;
using System;

public class Pulse : MonoBehaviour
{
    public Vector3 sourcePoint;
    public float power;
    public GameObject fireflyPrefab;


    // Test
    private void Start()
    {
        Fireflash();
    }

    // Shoot fireflies
    public void Fireflash()
    {
        // shoots 6 fireflies
        int angle_degree = 0;

        for (int i = 0; i < Constantes.N_LUCIOLES; i++)
        {
            // Instantiate firefly
            GameObject ffGO = (GameObject)Instantiate(fireflyPrefab, sourcePoint, Quaternion.Euler(new Vector3(0, 0, angle_degree)));
            ffGO.name = "firefly" + i.ToString();
            Firefly ff = ffGO.GetComponent<Firefly>();

            // Set intensity
            ff.intensity = this.power;

            // Set velocity
            float vx = 6*Mathf.Cos(Mathf.Deg2Rad*angle_degree);
            float vz = 6*Mathf.Sin(Mathf.Deg2Rad*angle_degree);
            ff.velocity = new Vector3(vx, 0, vz);

            // Let fly
            ff.WaveForward();

            // select next angle
            angle_degree += 60;
        }
    }



}
