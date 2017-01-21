using UnityEngine;
using DG.Tweening;

public class Firefly : MonoBehaviour
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
}
