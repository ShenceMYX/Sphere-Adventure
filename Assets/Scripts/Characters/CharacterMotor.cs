using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Character
{
	/// <summary>
	/// 玩家马达
	/// </summary>
	public class CharacterMotor : MonoBehaviour
	{
		public float moveSpeed = 100f;

		public float dashSpeed = 100f;
		public float dashDuration = 0.1f;
		private float startDashTime = 0;


		public void Movement(Vector3 dir)
        {
			transform.parent.Translate(dir * moveSpeed * Time.deltaTime);
        }

		public void RotateToTarget(Vector3 targetPoint)
        {
			Quaternion dir = Quaternion.LookRotation(targetPoint - transform.position);
			Vector3 eulerDir = dir.eulerAngles;
			transform.eulerAngles = new Vector3(0, eulerDir.y, 0);
		}

		private IEnumerator StartDash(Vector3 dir)
        {
			startDashTime = Time.time + dashDuration;
            while (Time.time < startDashTime)
            {
				Movement(dir * dashSpeed * Time.deltaTime);
				yield return null;
			}
		}

        public void Dash(Vector3 dir)
        {
			StartCoroutine(StartDash(dir));
        }
	}
}