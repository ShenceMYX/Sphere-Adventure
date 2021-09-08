using System.Collections;
using System.Collections.Generic;
using SphereAdventure.Character;
using UnityEngine;

namespace SphereAdventure.Ability
{
	/// <summary>
	/// 
	/// </summary>
	public class Explosion : MonoBehaviour
	{
        public float atk;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
                other.GetComponent<CharacterStatus>().Damage(20);
        }
    }
}