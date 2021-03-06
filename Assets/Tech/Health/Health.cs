﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour {
  public AgentType agentType;
  public float maxHealth = 100;
  public float currentHealth;
  public event Action<float, float> OnHealthChanged;
  public AudioSource audioSource;

    public AudioClip deathSoundClip;

  private void Awake() {
    currentHealth = maxHealth;

  }

  // Start is called before the first frame update
  void Start() {
    audioSource.volume = PlayerPrefs.GetFloat("Options_AudioVolume");
    Healthbar.LinkToHealthbar(this);
  }

  // Update is called once per frame
  void Update() {

  }

  public void TakeDamage(float amount) {
    currentHealth -= amount;
    OnHealthChanged?.Invoke(currentHealth, maxHealth);

    if (currentHealth <= 0)
      Kill();
  }

  public void Kill() {
    switch (agentType) {
      case AgentType.Player:
        /// Game over!
        break;
      case AgentType.Boss:
        /// Destroy boss obj.
        /// Open level complete popup.
        break;
      case AgentType.Enemy:
        /// Play destruction effect.
        /// Destroy enemy obj.
        audioSource.PlayOneShot(deathSoundClip);
        break;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    Projectile projectile = collision.GetComponent<Projectile>();

    if (projectile != null && projectile.agentType != this.agentType) {
      TakeDamage(projectile.damage);
      Destroy(projectile.gameObject);
    }
  }
}
