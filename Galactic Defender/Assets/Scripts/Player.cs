using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    Vector2 rawInput;
    private Health currentHealth;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter; 

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }
    void Start()
    {
        currentHealth = GameObject.FindObjectOfType<Health>();
        InitBounds();
    }

    // Update is called once per frame
    void Update()
    {
       Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            if (value.isPressed)
            {
                shooter.StartFiring();
            }
            else
            {
                shooter.StopFiring();
            }
        }
    }

    public void OnSpreadShot(InputValue value)
    {
        if (value.isPressed)
        {
            shooter.ActivateSpreadShot();
        }
    }

    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="Power")
        currentHealth.RecoverHealth();
        Destroy(other.gameObject);
    }
 
}
