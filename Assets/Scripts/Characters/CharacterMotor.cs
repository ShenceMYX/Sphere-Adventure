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

		public void Movement(Vector3 dir)
        {
			transform.Translate(dir * moveSpeed * Time.deltaTime);
        }

		public void RotateToTarget(Vector3 targetPoint)
        {
			Quaternion dir = Quaternion.LookRotation(targetPoint - transform.position);
			Vector3 eulerDir = dir.eulerAngles;
			transform.eulerAngles = new Vector3(0, eulerDir.y, 0);
		}
	}
}