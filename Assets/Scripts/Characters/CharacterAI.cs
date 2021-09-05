using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Character
{
	/// <summary>
	/// 角色AI
	/// </summary>
	public class CharacterAI : MonoBehaviour
	{
		private CharacterMotor motor;

        private void Start()
        {
            motor = GetComponent<CharacterMotor>();
        }

        private void Update()
        {
        }

        private void FollowTarget(Vector3 target)
        {
            if (Vector3.Distance(target, transform.position) < 1f) return;
            Vector3 dir = target - transform.position;
            motor.Movement(dir);
        }
	}
}