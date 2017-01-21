using UnityEngine;
using DG.Tweening;

public class Firefly : Entite
{
    public float intensity;
    public Vector3 velocity;

    private void Start()
    {
        this.GetComponent<Light>().intensity = intensity;
    }

    public void WaveForward()
    {
        this.transform.DOBlendableMoveBy(velocity, 15f);
    }

    public void Wane()
    {
        // decrease intensity each time unit
    }

    void OnCollisionEnter(Collision col)
    {
        Tile tileCol = col.gameObject.GetComponent<Tile>();
        if (tileCol != null && tileCol.type == Tile.Type.Mur)
        {
            Destroy(gameObject);
        }
    }

}
