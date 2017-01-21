using UnityEngine;
using System;

public class Pulse : MonoBehaviour
{
    public Vector3 sourcePoint;
    public float power;
    public GameObject fireflyPrefab;

    // Shoot fireflies
    public void Fireflash()
    {
        // shoots 6 fireflies
        int angle_degree = 0;

        for (int i = 0; i < Constantes.N_LUCIOLES; i++)
        {
            // Instantiate firefly
            GameObject ff = (GameObject)Instantiate(fireflyPrefab, sourcePoint, Quaternion.Euler(new Vector3(0, 0, angle_degree)));
            
            // On donne un nom à nos cases
            ff.name = "firefly" + i.ToString();
            
            // Set velocity
            //ff.GetComponent<FireFly>().velocity

            // select next angle
            angle_degree += 60;
        }
    }



}
