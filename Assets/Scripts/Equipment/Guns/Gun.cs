using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Equipment
{
    public enum GUN_MODE {
        SINGLE,
        AUTOMATIC,
        BOTH,
    }

    [Header("Gun Settings")]
    public float bps = 10.0f;
    public float bulletSpread = 0.0f;
    public int bulletCount = 1;
    public GUN_MODE mode = GUN_MODE.BOTH;
    public float rumble = 1.0f;
    public float rumbleDuration = 0.1f;
    [Header("Prefabs")]
    public GameObject bullet;
    public GameObject shootEffect;
    public GameObject casingEffect;
    [Header("Pointers")]
    public GameObject bulletExit;
    public GameObject casingExit;
    public AmmoHandler ammoHandler;

    private ObjectPool bulletPool;
    private ObjectPool shootEffectPool;
    private ObjectPool casingPool;
    private bool automatic = false;
    private float lastShotTime = -1000f;
    private float shotCooldown = 0.0f;
    
    new public void Start() {
        base.Start();
        bulletPool = gameObject.AddComponent<ObjectPool>();
        bulletPool.prefab = bullet;
        bulletPool.initialPoolSize = Mathf.CeilToInt(1.0f/(bps*bulletCount));

        if(shootEffect) {
            shootEffectPool = gameObject.AddComponent<ObjectPool>();
            shootEffectPool.prefab = shootEffect;
            shootEffectPool.initialPoolSize = bulletPool.initialPoolSize;
        }

        if(casingEffect && casingExit) {
            casingPool = gameObject.AddComponent<ObjectPool>();
            casingPool.prefab = casingEffect;
            casingPool.initialPoolSize = bulletPool.initialPoolSize;
        }

        shotCooldown = 1.0f/bps;
        automatic = mode == GUN_MODE.AUTOMATIC;
    }

    public void Update() {
        HandleInputs();
    }

    private void HandleInputs() {
        if(!IsGrabbed()) return;

        if(mode == GUN_MODE.BOTH && grabController.SecondaryJustPressed()) automatic = !automatic;
        if(automatic) {
            if(grabController.TriggerPressed()) Shoot();
        } else {
            if(grabController.TriggerJustPressed()) Shoot();
        }

        if(grabController.PrimaryJustPressed()) ammoHandler?.EjectAmmo();
    }

    private void Shoot() {
        if(!HasAmmo()) return;
        if(Time.time - lastShotTime < shotCooldown) return;
        lastShotTime = Time.time;

        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = bulletExit.transform.position;
        bullet.transform.rotation = bulletExit.transform.rotation;

        if(bulletSpread > 0) {
            var randomNumberX = Random.Range(-bulletSpread, bulletSpread);
            var randomNumberY = Random.Range(-bulletSpread, bulletSpread);
            var randomNumberZ = Random.Range(-bulletSpread, bulletSpread);
            bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
        }

        if(rumble > 0 && rumbleDuration > 0) grabController.Rumble(rumble, rumbleDuration);

        ammoHandler?.ConsumeAmmo();
        ShootEffect();
        EjectCasing();
    }

    private void ShootEffect() {
        if(shootEffectPool == null) return;
        GameObject shootEffect = shootEffectPool.GetObject();
        shootEffect.transform.position = bulletExit.transform.position;
        shootEffect.transform.rotation = bulletExit.transform.rotation;
    }

    private void EjectCasing() {
        if(casingPool == null) return;
        GameObject casing = casingPool.GetObject();
        casing.transform.position = casingExit.transform.position;
        casing.transform.rotation = casingExit.transform.rotation;
        casing.GetComponent<Rigidbody>().velocity = casingExit.transform.right*Random.Range(1.0f,2.0f);
    }

    private bool HasAmmo() {
        if(ammoHandler == null) return true;
        return ammoHandler.GetAmmoCount() > 0;
    }
}
