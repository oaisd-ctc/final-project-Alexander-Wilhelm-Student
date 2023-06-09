using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // a list of variables half as big as your mom
    GameManager gm;
    [Header("General")]
    [SerializeField] int health = 50;
    [SerializeField] int cashCost = 0;
    [SerializeField] int cashOnDeath;
    [SerializeField] float towerCooldown = 1f;
    public bool LawnmowerImmunity;

    [Header("Effects")]
    [SerializeField] float flashLength = 0.025f;
    [SerializeField] GameObject deathEffect;
    [SerializeField] float hitShakeAmount;
    [SerializeField] float deathShakeAmount;
    [SerializeField] float ShakeDecay;
    float flashTimer;
    Material flash;
    CameraShake shake;

    [Header("SFX")]
    AudioPlayer audioPlayer;
    [SerializeField] AudioClip spawnSound;
    [SerializeField] float spawnVolume;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] float hitVolume = 0.6f;
    [SerializeField] AudioClip[] deathSounds;
    [SerializeField] float deathVolume = 0.6f;
    [SerializeField] AudioClip[] idleSounds;
    [SerializeField] float idleVolume = 0.6f;
    [SerializeField] float idleSoundInterval;
    [SerializeField] float idleSoundIntervalVariance;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        flash = GetComponentInChildren<SpriteRenderer>().material;
        shake = FindObjectOfType<CameraShake>();
        if (spawnSound != null) audioPlayer.PlayClip(spawnSound, spawnVolume);
        if (idleSounds.Length > 0) StartCoroutine(IdleSound());

    }

    void Update()
    {
        if (flashTimer > 0)
        {
            flash.SetFloat("_FlashAmount", 1);
        }
        else flash.SetFloat("_FlashAmount", 0);

        flashTimer -= Time.deltaTime;
    }

    public void DeathStuff()
    {
        if (cashOnDeath > 0)
        {
            gm.AddCash(cashOnDeath);
            FindObjectOfType<UIScript>().UpdateMoneyText();
        }
        if (deathEffect != null) Instantiate(deathEffect, transform.position, Quaternion.identity);
        if (deathSounds.Length > 0)
        {
            AudioClip deathSound = deathSounds[Random.Range(0, deathSounds.Length)];
            if (deathSound != null) audioPlayer.PlayClip(deathSound, deathVolume);
        }
        //shake.setShake(deathShakeAmount, ShakeDecay);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {

        health -= damage;
        flashTimer = flashLength;
        if (hitSounds.Length > 0)
        {
            AudioClip hitSound = hitSounds[Random.Range(0, hitSounds.Length)];
            if (hitSound != null) audioPlayer.PlayClip(hitSound, hitVolume);
        }
        shake.setShake(hitShakeAmount, ShakeDecay);
        if (health <= 0)
        {
            DeathStuff();
        }
    }

    IEnumerator IdleSound()
    {
        yield return new WaitForSeconds(idleSoundInterval + Random.Range(0, idleSoundIntervalVariance));

        if (idleSounds.Length > 0)
        {
            AudioClip idleSound = idleSounds[Random.Range(0, idleSounds.Length)];
            if (idleSound != null) audioPlayer.PlayClip(idleSound, idleVolume);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetCashCost()
    {
        return cashCost;
    }

    public float GetCooldown() {
        return towerCooldown;
    }



}
