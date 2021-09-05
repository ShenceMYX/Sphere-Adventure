using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Character
{
	/// <summary>
	/// 玩家角色控制器
	/// </summary>
	public class CharacterController : MonoBehaviour
	{
		private float xInput;
		private float yInput;

		private CharacterMotor motor;

        private void Start()
        {
			motor = GetComponent<CharacterMotor>();
        }

        private void Update()
        {
			xInput = Input.GetAxis("Horizontal");
			yInput = Input.GetAxis("Vertical");

			Vector3 move = Vector3.right * xInput + Vector3.forward * yInput;
			motor.Movement(move);

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100))
			{
				//transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
				motor.RotateToTarget(new Vector3(hit.point.x, transform.position.y, hit.point.z));
			}
		}
	}
}