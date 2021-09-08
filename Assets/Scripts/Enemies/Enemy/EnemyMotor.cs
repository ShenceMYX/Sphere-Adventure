using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Enemy
{
	public class EnemyMotor : MonoBehaviour
	{
		public float moveSpeed = 100f;

		public Transform[] wayPoints;

		public int currentPointIndex;

		public void MoveForward()
        {
			this.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        }

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

		public bool PathFinding()
        {
			
				
			RotateToTarget(wayPoints[currentPointIndex].position);
			MoveForward();

			if (Vector3.Distance(this.transform.position, wayPoints[currentPointIndex].position) < 0.1f)
				currentPointIndex = 1 - currentPointIndex;


			return true;
		}
	}
}