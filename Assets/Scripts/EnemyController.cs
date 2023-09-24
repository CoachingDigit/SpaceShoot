using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject laserEnemyPrefab;
	public GameObject shootEffect;
	private Vector3 offsetFire = new Vector3(0, -2);
	public float repeatRate = 2;
	public int score = 10;
	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find(nameof(GameManager)).GetComponent<GameManager>();
		InvokeRepeating(nameof(Fire), 1, repeatRate);
	}
	void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime  * speed);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("LaserPlayer"))
		{
			gameManager.score += score;
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}

	IEnumerator AnimationFire()
	{
		yield return new WaitForSeconds(0.1f);

		shootEffect.SetActive(false);
	}

	private void Fire()
	{
		Instantiate(laserEnemyPrefab, transform.position + offsetFire, laserEnemyPrefab.transform.rotation);
		shootEffect.SetActive(true);
		StartCoroutine(AnimationFire());
	}


}
