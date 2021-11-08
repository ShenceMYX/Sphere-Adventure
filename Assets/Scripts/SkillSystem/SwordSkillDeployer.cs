using System.Collections;
using System.Collections.Generic;
using Common;
using SphereAdventure.Character;
using UnityEngine;

namespace SphereAdventure.Skill
{
    /// <summary>
    /// 
    /// </summary>
    public class SwordSkillDeployer : SkillDeployer//, IResetable
    {
        public float zOffset;
        public override void DeploySkill()
        {
            transform.position += transform.forward * zOffset;
            CalculateTargets();
            ImpactTargets();
        }

        //public void OnReset()
        //{
        //    transform.position += transform.forward * zOffset;
        //}
    }
}