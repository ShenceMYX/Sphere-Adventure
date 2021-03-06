using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Character
{
	/// <summary>
	/// 角色状态类
	/// </summary>
	public class CharacterStatus : MonoBehaviour
	{
		public float maxHealth;
		public float currentHealth;

        private void Start()
        {
			currentHealth = maxHealth;
        }

		public void Damage(float amount)
        {
			currentHealth -= amount;
			if (currentHealth <= 0)
				Death();
        }

        private void Death()
        {
			Destroy(gameObject);
        }
    }
}