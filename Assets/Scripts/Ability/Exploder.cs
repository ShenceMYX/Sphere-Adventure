using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using SphereAdventure.Character;
using UnityEngine;

namespace SphereAdventure.Ability
{
	/// <summary>
	/// 
	/// </summary>
	public class Exploder : AbiliyReleaser
	{
        public GameObject explodePrefab;
        public float explosionDistance = 5f;
        public float explodeDelay = 0.5f;
        private float startCountTime;

        private void OnEnable()
        {
            transform.parent.parent.GetComponent<CharacterInputController>().ExplodeAttckHandler += StartExplosion;
        }

        private void OnDisable()
        {
            transform.parent.parent.GetComponent<CharacterInputController>().ExplodeAttckHandler -= StartExplosion;
        }

        private IEnumerator Explosion(Vector3 dir)
        {
            yield return new WaitForSeconds(explodeDelay);
            GameObject go = GameObjectPool.Instance.CreateObject("explosion", explodePrefab, transform.position - dir * explosionDistance, Quaternion.identity);
            GameObjectPool.Instance.CollectObject(go, 1f);
        }

        private void StartExplosion(Vector3 dir)
        {
            StartCoroutine(Explosion(dir));
        }
    }
}