using Assets;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpComponent : MonoBehaviour
{
	public static readonly float SHOTGUN_ANGLE = 50f;
	public static readonly float SHOTGUN_REACH = 3f;
    public Sprite[] icons = null;

	public PowerUpType type;
    // Use this for initialization
    private bool hasTimedPowerUp;
	private int lastShoot = -1;

	public GameObject particleShooter;
	public GameObject bazoubulle;
	public GameObject bazoubulleExplosion;

	private Rigidbody body;
    public int playerId;

	// Use this for initialization
	void Start () {
        type = PowerUpType.NONE;
        hasTimedPowerUp = false;

		body = GetComponent<Rigidbody>();
        playerId = IdFromName(gameObject) - 1;
    }
	
	// Update is called once per frame
	void Update () {
        GameObject[] images = GameObject.FindGameObjectsWithTag("UIImage");
        
        foreach (GameObject g in images)
        {
            Image im = g.GetComponent<Image>();

            int imageID = IdFromName(g);

            if(IdFromName(gameObject) - 1 == imageID)
            {
                im.sprite = icons[(int)type];
                break;
            }
        }
        
        switch(type)
        {
            case PowerUpType.GIANT_BUBBLE :
                    if (!hasTimedPowerUp)
                        StartCoroutine(BeingGiant());

                    gameObject.transform.localScale = new Vector3(2, 2, 2);
                break;

			case PowerUpType.SHOTBULLE:
		        if(Input.GetButton("Fire1"))
		        {
			        ShootABubble();
			        type = PowerUpType.NONE;
		        }
		        break;

			case PowerUpType.BAZOUBULLE:
				if(Input.GetButton("Fire1"))
				{
					Bazoubulle();
					type = PowerUpType.NONE;
				}
				break;

			case PowerUpType.GRABULLE:

		        break;

			default:
				type = PowerUpType.NONE;
				break;
        }
	}

	private void ShootABubble()
	{
		if(lastShoot != -1 && lastShoot + 20 > Time.frameCount)
			return;
		lastShoot = Time.frameCount;

		Vector2 shootDir = new Vector2(transform.forward.x, transform.forward.z);
		shootDir.Normalize();

		foreach(GameObject player in GameObject.FindWithTag("WorldController").GetComponent<CheckAlives>().GetPlayers())
		{
			Vector2 playerDir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.z - transform.position.z);

			if(playerDir.magnitude > SHOTGUN_REACH)
				continue;

			if(Vector2.Angle(shootDir, playerDir) < SHOTGUN_ANGLE)
			{
				float force = player.GetComponent<PowerUpComponent>().isGiant() ? 10 : 14;

				player.GetComponent<Rigidbody>().AddForce(shootDir.x * force, 0, shootDir.y * force, ForceMode.Impulse);
			}
		}

		body.velocity = Vector3.zero;
		body.AddForce(shootDir.x * -8, 0, shootDir.y * -8, ForceMode.Impulse);      
        Shake.ShakeEffect(Camera.main.gameObject, 0.25f, 0.12f);

		Destroy(Instantiate(particleShooter, transform.position, transform.rotation), 0.7f);
	}


	void Bazoubulle()
	{
		if(lastShoot != -1 && lastShoot + 20 > Time.frameCount)
			return;
		lastShoot = Time.frameCount;

		GameObject clone = Instantiate(bazoubulle, transform.position - new Vector3(0, 0.25f, 0) + transform.forward, transform.rotation);

		clone.GetComponent<Bazoubulle>().baseVel = new Vector3(transform.forward.x * 20, 0, transform.forward.z * 20);
		clone.GetComponent<Bazoubulle>().holder = gameObject;

		body.velocity = Vector3.zero;
		body.AddForce(transform.forward.x * -2, 0, transform.forward.y * -2, ForceMode.Impulse);

		Destroy(clone, 30f);
	}

	IEnumerator BeingGiant()
    {
        hasTimedPowerUp = true;
        yield return new WaitForSeconds(10);

        type = PowerUpType.NONE;
        hasTimedPowerUp = false;
	}

	public bool isGiant()
	{
		return type == PowerUpType.GIANT_BUBBLE;
	}

    private int IdFromName(GameObject obj)
    {
        string name = obj.name;
        playerId = System.Int32.Parse(name[name.Length - 1].ToString());
        return playerId;
    }
}
