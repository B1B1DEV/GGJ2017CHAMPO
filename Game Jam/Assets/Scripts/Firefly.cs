using UnityEngine;
using DG.Tweening;

public class Firefly : Entite
{
    public float intensity;
    public Vector3 velocity;
    private GameManager gm;
    public int age;

	protected override void Start()
    {
        this.GetComponent<Light>().intensity = intensity;
        gm = GameManager.Instance;
        gm.fireflies.Add(this);
        age = 0;
    }

    public void WaveForward()
    {
        // InvokeRepeating("UpdateEntite", 0f, Constantes.DELTA_T_LIGHT);
    }

    public override void Move()
    {
        //Move to the next tile
        Vector3 posNextTile = new Vector3(transform.position.x + velocity.x, 0, transform.position.z + velocity.z);
        float timeAnim = 12f;
        this.transform.DOMove(posNextTile, timeAnim);
        
        //Wane
        Wane();
    }

    public void Wane()
    {
		
        // decrease intensity until light disappear
        intensity -= 0.1f;
        this.GetComponent<Light>().intensity = intensity;

        // Destroy if zero
        if (intensity <= 0f)
        {
            Destroy(gameObject);
            gm.fireflies.Remove(this);
        }

    }

}
