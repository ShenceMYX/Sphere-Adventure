using System.Collections;
using System.Collections.Generic;
using SphereAdventure.Character;
using UnityEngine;
using Common;

namespace SphereAdventure.Ability
{
	/// <summary>
	/// 
	/// </summary>
	public class Bullet : MonoBehaviour, IResetable
	{
        public float atk = 10f;
        public float shootDistance = 50f;
        public float moveSpeed = 30f;

        private Vector3 targetPos;

        public void OnReset()
        {
            targetPos = transform.TransformPoint(0, 0, shootDistance);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log("hit!!!!!!!!!!!!!!!!!!!!1");
                other.GetComponent<CharacterStatus>().Damage(atk);
                targetPos = other.GetComponent<CharacterStatus>().transform.position;
            }
        }

        private void Update()
        {
            //Debug.Log(targetPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                GameObjectPool.Instance.CollectObject(gameObject);
        }

    }
}