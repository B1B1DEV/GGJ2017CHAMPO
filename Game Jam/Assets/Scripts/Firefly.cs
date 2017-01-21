using UnityEngine;
using DG.Tweening;

public class Firefly : Entite
{
    public float intensity;
    public Vector3 velocity;
    private GameManager gm;

    private void Start()
    {
        //this.GetComponent<Light>().intensity = intensity;
        gm = GameManager.Instance;
        gm.fireflies.Add(this);
    }

    public void WaveForward()
    {
        //Move to the next tile
        Vector3 posNextTile = new Vector3(transform.position.x + Mathf.Sqrt(3)/2*velocity.x, 0, transform.position.z + Mathf.Sqrt(3)/2*velocity.z);
        float timeAnim = 3f;
        this.transform.DOMove(posNextTile, timeAnim);
    }

    public void Wane()
    {
        // decrease intensity until light disappear
        intensity -= 0.1f;

        // Destroy if zero
        if (intensity <= 0f)
        {
            Destroy(gameObject);
            gm.fireflies.Remove(this);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Tile tileCol = col.gameObject.GetComponent<Tile>();
        if (tileCol != null && tileCol.type == Tile.Type.Mur)
        {
            Destroy(gameObject);
            gm.fireflies.Remove(this);
        }
    }

}
