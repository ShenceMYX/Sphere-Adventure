using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Player
{
	/// <summary>
	/// 玩家马达
	/// </summary>
	public class PlayerMotor : MonoBehaviour
	{
		public float moveSpeed = 100f;

		public void Movement(Vector3 dir)
        {
			transform.Translate(dir * moveSpeed * Time.deltaTime);
        }
	}
}