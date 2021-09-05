using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Player
{
	/// <summary>
	/// 玩家角色控制器
	/// </summary>
	public class PlayerController : MonoBehaviour
	{
		private float xInput;
		private float yInput;

		private PlayerMotor motor;

        private void Start()
        {
			motor = GetComponent<PlayerMotor>();
        }

        private void Update()
        {
			xInput = Input.GetAxis("Horizontal");
			yInput = Input.GetAxis("Vertical");

			Vector3 move = transform.right * xInput + transform.forward * yInput;
			motor.Movement(move);
		}
	}
}