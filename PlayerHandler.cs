using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHandler : MonoBehaviour
{
    [Header( "Statistics" )]
    public float crosshairDistance = 5f;
    public float bulletVelocity = 20f;
    public float fireRate = 0.25f;
    public float bulletDamage = 75f;
    public static int _ammoCount = 5;
    //Preset Settings, feel free to tweak these around


    [Header( "Prefabs" )]
    public GameObject crosshair;
    public GameObject bulletPrefab;
    public TextMeshProUGUI _ammoText;
    public Animator topAnimator;
    public Animator bottomAnimator;
    public AudioSource shoot;
    public Health HealthBar;

    //Debugging Purposes
    private bool isArmed;
    private bool isAiming;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 aim;
    private Vector2 movementDirection;
    private float bulletDelay = 2f;
    private float timer;

    void Start()
    {
        shoot = GetComponent<AudioSource>();
        timer = fireRate;
    }

    void Update()
    {
        ProcessInput();
        AimandShoot();
        AnimationHandler();
        AmmoCalc();
        HealthBar();
    }

    private void ProcessInput()
    {
        //Top-Down Movement is Calculated Here
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        movementDirection = new Vector2(horizontalInput, verticalInput);
        Vector3 mouseMovement = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0f);

        //Mouse Aiming Movement is Calculated here
        aim = aim + mouseMovement;
        if (aim.magnitude > 1.0f)
            aim.Normalize();
        isAiming = Input.GetButtonDown("Fire1");

    }

    private void AimandShoot()
    {
        fireRate -= Time.deltaTime;
        Vector2 shootingDirection = new Vector2(aim.x, aim.y);
        if (_ammoCount == 0)
        {
            return;
        }

        if (aim.magnitude > 0.0f)
        {
            crosshair.transform.localPosition = aim * crosshairDistance;
            crosshair.SetActive(true);
            shootingDirection.Normalize();

            //Shooting is handled here when the player presses Fire1
            if (isAiming && 0 > fireRate)
            {
                shoot.Play();
                topAnimator.SetBool("Aim", true);  
                bottomAnimator.SetBool("Aim", false);  

                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); 
                bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletVelocity;
                bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);

                Destroy(bullet, bulletDelay);
                AmmoCount(-1);
                fireRate = timer;
            }


            if (isAiming == false)
            {
                topAnimator.SetBool("Aim", false);  
                bottomAnimator.SetBool("Aim", false);  
            }
        } else {
            crosshair.SetActive(false);
        }

    }

    public void AmmoCount(int bullet)
    {
        _ammoCount += bullet;
    }

    private void AnimationHandler()
    {
        //Handles Animation for the upper part of the player and the Lower Part of the player
        bottomAnimator.SetFloat("Horizontal", aim.x);
        bottomAnimator.SetFloat("Vertical", aim.y);
        bottomAnimator.SetFloat("Speed", movementDirection.magnitude);

        topAnimator.SetFloat("Horizontal", aim.x);
        topAnimator.SetFloat("Vertical", aim.y);
        topAnimator.SetFloat("Speed", movementDirection.magnitude);   

        //This makes the player face the last position instead of resetting to face forwards
        if(script.horizontalInput > 0.001 || script.horizontalInput < -0.001 || script.verticalInput > 0.001 || script.verticalInput < -0.001) 
        {
            bottomAnimator.SetFloat("LastX", script.horizontalInput);
            bottomAnimator.SetFloat("LastY", script.verticalInput);
            topAnimator.SetFloat("LastX", script.horizontalInput);
            topAnimator.SetFloat("LastY", script.verticalInput);
        }
    }

    private void AmmoCalc()
    {
        _ammoText.text = _ammoCount + "/5";
        if(_ammoCount > 5)
            _ammoCount = 5;
    }

    void HealthBar()
    {
        if (HealthBar.health < 0f)
        {
            Destroy (this.gameObject);
            Debug.Log("Death");
        }
    }

}
