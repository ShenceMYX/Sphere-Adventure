using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Enemy
{
	/// <summary>
	/// AI
	/// </summary>
	public class EnemyAI : MonoBehaviour
	{
        public enum State
        {
            Attack,
            PathFinding
        }

        private State currentState = State.PathFinding;
        private EnemyMotor motor;

        private void Start()
        {
            motor = GetComponent<EnemyMotor>();
        }

        private void Update()
        {
            switch (currentState)
            {
                case State.PathFinding:
                    PathFinding();
                    break;
                case State.Attack:
                    Attack();
                    break;
            }
        }

        private void Attack()
        {

        }

        private void PathFinding()
        {
            if (motor.PathFinding() == false)
            {
                currentState = State.Attack;
            }
        }

	}
}