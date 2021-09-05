using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Enemy
{
	/// <summary>
	/// 角色状态类
	/// </summary>
	public class EnemyStatus : MonoBehaviour
	{
		public float maxHealth;
		private float currentHealth;

        private void Start()
        {
			currentHealth = maxHealth;
        }

		public void Damage(float amount)
        {
			currentHealth -= amount;
			if (amount <= 0)
				Death();
        }

        private void Death()
        {
			Destroy(gameObject);
        }
    }
}