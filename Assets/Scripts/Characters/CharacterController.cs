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

		private Vector3 mousePos;

		private CharacterMotor motor;

		public Camera cam;

        private void Start()
        {
			motor = GetComponent<CharacterMotor>();
			//cam = Camera.main;
        }

        private void Update()
        {
			xInput = Input.GetAxis("Horizontal");
			yInput = Input.GetAxis("Vertical");

			Vector3 move = transform.right * xInput + transform.forward * yInput;
			motor.Movement(move);

			mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

			Debug.Log(mousePos);

			motor.RotateToTarget(new Vector3(mousePos.x, transform.position.y, mousePos.z));
		}
	}
}