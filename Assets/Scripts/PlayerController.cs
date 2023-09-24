using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 15.0f;
	private float boundary = 10;
	public int lifes = 3;
	[HideInInspector]
	public GameObject shootEffect;
	public GameObject laserPrefab;
	private Vector3 offsetFire = new Vector3(0, 2);
	private SpriteRenderer spriteRenderer;
	public Sprite leftSprite;
	public Sprite rightSprite;
	public Sprite normalSprite;
	public Sprite damageSprite;
	private GameManager gameManager;
    void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = normalSprite;
		gameManager = GameObject.Find(nameof(GameManager)).GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
	{
		Fire();
		if(lifes < 2)
		{
			spriteRenderer.sprite = damageSprite;
		}
		MovePlayer();

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("LaserEnemy"))
		{
			
			DecreaseLife();
			if(lifes == 0)
			{
				gameManager.isGameActive  = false;
				gameManager.SetGameOver();
				Destroy(gameObject);
			}

			
			Destroy(collision.gameObject);

		}
	}
	

	private void DecreaseLife()
	{
		if (lifes > 0)
		{
			--lifes;
		}
	}

	private void Fire()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate(laserPrefab, transform.position + offsetFire, laserPrefab.transform.rotation);
			shootEffect.SetActive(true);
			StartCoroutine(AnimationFire());
		}
	}

	IEnumerator AnimationFire()
	{
		yield return new WaitForSeconds(0.1f);

		shootEffect.SetActive(false);
	}

	private void MovePlayer()
	{
		float horizontal = Input.GetAxis("Horizontal");
		AnimateMovePlayer(horizontal);
		if (transform.position.x >= boundary)
		{
			transform.position = new Vector2(boundary, transform.position.y);
		}

		if (transform.position.x <= -boundary)
		{
			transform.position = new Vector2(-boundary, transform.position.y);
		}
		transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
	}

	private void AnimateMovePlayer(float horizontal)
	{
		if (horizontal > 0)
		{
			spriteRenderer.sprite = rightSprite;
		}
		else if (horizontal < 0)
		{
			spriteRenderer.sprite = leftSprite;
		}
		else
		{
			if(lifes > 2)
			{
				spriteRenderer.sprite = normalSprite;
			}
			else
			{
				spriteRenderer.sprite = damageSprite;
			}
			
		}
	}
}
