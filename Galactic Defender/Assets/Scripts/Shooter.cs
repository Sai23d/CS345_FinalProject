using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float basefiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [Header("Spread Shot")]
    [SerializeField] bool useSpreadShot = false;
    [SerializeField] int spreadProjectileCount = 3;
    [SerializeField] float spreadAngle = 45f;

    [Header("Spread Shot Timer")]
    [SerializeField] float spreadShotDuration = 5f; // Duration of the spread shot active period
    [SerializeField] float spreadShotCooldown = 10f; // Cooldown before spread shot can be used again

    private bool isFiring;
    private bool isSpreadShotActive = false;
    private float spreadShotTimer = 0f;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;



    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    void Update()
    {
        Fire();

        if (useSpreadShot && isSpreadShotActive)
        {
            if (spreadShotTimer > 0)
            {
                spreadShotTimer -= Time.deltaTime;
            }
            else
            {
                isSpreadShotActive = false;
            }
        }
    }

    public void ActivateSpreadShot()
    {
        if (!isSpreadShotActive)
        {
            Debug.Log("Spread shot activated!");
            isSpreadShotActive = true;
            spreadShotTimer = spreadShotDuration;
            StartCoroutine(SpreadShotCooldown());
        }
    }

    IEnumerator SpreadShotCooldown()
    {
        yield return new WaitForSeconds(spreadShotCooldown);
        isSpreadShotActive = false;
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            if (isSpreadShotActive)
            {
                float startAngle = -spreadAngle / 2;
                float angleStep = spreadAngle / (spreadProjectileCount - 1);

                for (int i = 0; i < spreadProjectileCount; i++)
                {
                    GameObject instance = Instantiate(projectilePrefab,
                                                      transform.position,
                                                      Quaternion.Euler(0, 0, startAngle + angleStep * i));

                    Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = instance.transform.up * projectileSpeed;
                    }

                    Destroy(instance, projectileLifetime);
                }
            }
            else
            {
                GameObject instance = Instantiate(projectilePrefab,
                                                  transform.position,
                                                  Quaternion.identity);

                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = transform.up * projectileSpeed;
                }

                Destroy(instance, projectileLifetime);
            }

            float timeToNextProjectile = Random.Range(basefiringRate - firingRateVariance,
                                                      basefiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
