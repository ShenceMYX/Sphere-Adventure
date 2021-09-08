using System.Collections;
using System.Collections.Generic;
using Common;
using MoreMountains.Feedbacks;
using SphereAdventure.Character;
using UnityEngine;

namespace SphereAdventure.Ability
{
    /// <summary>
    /// 
    /// </summary>
    public class Shooter : MonoBehaviour
    {
        public GameObject bulletPrefab;

        public MMFeedbacks shootFeedbacks;

        public float shootInterval = 0.5f;
        private float startShootTime;

        private void Start()
        {
            transform.parent.parent.GetComponent<CharacterInputController>().ShootHandler += Shoot;
        }

        public void Shoot(Vector3 target)
        {
            if(startShootTime < Time.time)
            {
                shootFeedbacks?.PlayFeedbacks();
                Vector3 dir = target - transform.position;
                GameObjectPool.Instance.CreateObject("bullet", bulletPrefab, transform.position, Quaternion.LookRotation(new Vector3(dir.x, transform.position.y, dir.z)));
                startShootTime = Time.time + shootInterval;
            }
            
        }

    }  
}