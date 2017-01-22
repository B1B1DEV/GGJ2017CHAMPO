using UnityEngine;
using DG.Tweening;

public class Firefly : Entite
{
    public float intensity;
    public Vector3 velocity;
    private GameManager gm;
    public int age;

	public delegate void DestroyFireflyAction();
	public event DestroyFireflyAction OnDestroy = null;


	protected override void Start()
    {
		base.Start ();
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
        Vector3 posNextTile = new Vector3(transform.position.x + velocity.x, transform.position.y, transform.position.z + velocity.z);
        float timeAnim = 6f;
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
			DestroyFirefly ();
        }

    }

	public void DestroyFirefly()
	{
		GameManager.Instance.DestroyFireflyEvent += delegate {
			OnDestroy.Invoke();
			Destroy(gameObject);
			gm.fireflies.Remove(this);	
			positions.Terminer();
		};
	}

}
