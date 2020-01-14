using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gun
{
    public float shootRate;
    public int mag;
    public int magSize;
    public int reserveAmmo;
    public float reloadTime;
    public string gunName;
    public AudioClip gunSound;
    public RuntimeAnimatorController animator;

    public Gun()
    {

    }
}

public class Shooting : MonoBehaviour {


    Gun rifle = new Gun();
    Gun handGun = new Gun();
    Gun shotGun = new Gun();

    Gun gun = new Gun();

    public Transform firePoint;
    public GameObject bulletPrefab;
    public Text text;


    public float fireRate = 0.2f;
    private float nextFire = 0.0f;
    bool isShooting;
    Animator animator;
    public RuntimeAnimatorController rifleController;
    public RuntimeAnimatorController handGunController;
    public RuntimeAnimatorController shotgunController;

    public int magazine = 10;
    int magSize;
    public int reserveAmmo = 20;

    float reloadTime = 3f;
    float reloadDone = 0.0f;

    bool isMelee;
    float meleeDuration = 1.0f;
    float meleeTime = 0.0f;


    public AudioSource gunSource;
    public AudioClip rifleClip;
    public AudioClip handgunClip;
    public AudioClip shotgunClip;

    public void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 12); //So bullets can go through certain collidables;
        Physics2D.IgnoreLayerCollision(11, 11);
        animator = GetComponent<Animator>();
        isShooting = false;
        isMelee = false;
        magSize = magazine;



        rifle.mag = 10;
        rifle.magSize = 10;
        rifle.reserveAmmo = 50;
        rifle.reloadTime = 3f;
        rifle.shootRate = 0.2f;
        rifle.gunName = "rifle";
        rifle.gunSound = rifleClip;
        rifle.animator = rifleController;

        handGun.mag = 5;
        handGun.magSize = 5;
        handGun.reserveAmmo = 25;
        handGun.reloadTime = 0.75f;
        handGun.shootRate = 0.2f;
        handGun.gunName = "handgun";
        handGun.gunSound = handgunClip;
        handGun.animator = handGunController;

        shotGun.mag = 5;
        shotGun.magSize = 5;
        shotGun.reserveAmmo = 25;
        shotGun.reloadTime = 5f;
        shotGun.shootRate = 0.75f;
        shotGun.gunName = "shotgun";
        shotGun.gunSound = shotgunClip;
        shotGun.animator = shotgunController;

        //gun = ref rifle;
        //UseHandgun();
        UseRifle();

        gunSource = GetComponent<AudioSource>();
    }

    public float bulletForce = 200f;
    // Update is called once per frame
    void Update() {

        text.text = "Ammo: " + gun.mag + "/" + gun.reserveAmmo;

        if (gun.mag <= 0)
        {
            Reload();
        }

        if (Input.GetKey(KeyCode.Space) && gun.mag > 0 && isMelee == false)
        {
            if (isShooting == false)
            {
                isShooting = true;
                animator.SetBool("IsShooting", isShooting);
            }
            if (Time.time > nextFire)
            {
                gunSource.Play();
                if(gun.gunName != "shotgun")
                {
                    FireProjectile();
                }
                else
                {
                    FireShell();
                }
                
                nextFire = gun.shootRate + Time.time;
                gun.mag -= 1;
            }
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            if (isShooting == true)
            {
                isShooting = false;
                animator.SetBool("IsShooting", isShooting);
            }

        }
        CheckForEmptyMagazine();

        


        //melee
        if (Input.GetKeyDown(KeyCode.M) && !isShooting)
        {
            Melee();
        }

        if (isMelee)
        {
            meleeTime += Time.deltaTime;
            if (meleeTime >= meleeDuration)
            {
                isMelee = false;

                meleeTime = 0;
            }
        }


        if(Input.GetKeyDown(KeyCode.H) && !isShooting && !animator.GetBool("IsReloading"))
        {
            UseHandgun();
        }
        else if(Input.GetKeyDown(KeyCode.R) && !isShooting && !animator.GetBool("IsReloading"))
        {
            UseRifle();
        }
        else if (Input.GetKeyDown(KeyCode.S)&& !isShooting && !animator.GetBool("IsReloading"))
        {
            UseShotgun();
        }

    }
    void CheckForEmptyMagazine()
    {
        if (gun.mag == 0 || gun.mag == 0 && gun.reserveAmmo == 0)
        {
            if (isShooting)
            {
                isShooting = false;
            }

            animator.SetBool("IsShooting", isShooting);
        }
       
    }
    void Melee()
    {
        isMelee = true;
        animator.SetTrigger("IsMelee");
    }
    void FireProjectile()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
    void FireShell()
    {
        //centre pellet
        GameObject pellet1 = Instantiate(bulletPrefab, firePoint.position,
            new Quaternion(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z, firePoint.rotation.w));
        pellet1.transform.Rotate(new Vector3(0, 0, 0));

        Rigidbody2D rb = pellet1.GetComponent<Rigidbody2D>();
        rb.AddForce(pellet1.transform.up * bulletForce, ForceMode2D.Impulse);

        GameObject pellet2 = Instantiate(bulletPrefab, firePoint.position,
            new Quaternion(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z , firePoint.rotation.w));
        pellet2.transform.Rotate(new Vector3(0, 0, 5f));
        Rigidbody2D rb2 = pellet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(pellet2.transform.up * bulletForce, ForceMode2D.Impulse);

        GameObject pellet3 = Instantiate(bulletPrefab, firePoint.position,
            new Quaternion(firePoint.rotation.x, firePoint.rotation.y, firePoint.rotation.z , firePoint.rotation.w));
        pellet3.transform.Rotate(0,0,-5f);
        Rigidbody2D rb3 = pellet3.GetComponent<Rigidbody2D>();
        rb3.AddForce(pellet3.transform.up * bulletForce, ForceMode2D.Impulse);
    }
    void Reload()
    {
        if (gun.reserveAmmo - gun.magSize >= 0)
        {
            if (!animator.GetBool("IsReloading"))
            {
                animator.SetBool("IsReloading", true);
            }

            reloadDone += Time.deltaTime;


            if (reloadDone > gun.reloadTime)
            {

                gun.mag = gun.magSize;
                gun.reserveAmmo = gun.reserveAmmo - gun.magSize;
                animator.SetBool("IsReloading", false);
                reloadDone = 0;
            }

        }

    }

    private void AssignGun(ref Gun g)
    {
        gun = g;
        gunSource.clip = g.gunSound;
        animator.runtimeAnimatorController = g.animator;
    }

    private void UseRifle()
    {
        AssignGun(ref rifle);
    }
    private void UseHandgun()
    {

        AssignGun(ref handGun);
    }
    private void UseShotgun()
    {
        AssignGun(ref shotGun);
    }
}
