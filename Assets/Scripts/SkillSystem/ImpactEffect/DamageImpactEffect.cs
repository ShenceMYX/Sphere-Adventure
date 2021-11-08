using System.Collections;
using System.Collections.Generic;
using SphereAdventure.Character;
using UnityEngine;

namespace SphereAdventure.Skill
{
    /// <summary>
    /// 
    /// </summary>
    public class DamageImpactEffect : IImapctEffect
    {
        //private SkillDeployer skillDeployer;

        public void Execute(SkillDeployer deployer)
        {
            //skillDeployer = deployer;
            deployer.StartCoroutine(RepeatDamage(deployer));
        }

        private void DamageOnce(SkillDeployer skillDeployer)
        {
            for (int i = 0; i < skillDeployer.SkillData.attackTargets.Length; i++)
            {
                skillDeployer.SkillData.attackTargets[i].GetComponent<CharacterStatus>().Damage(skillDeployer.SkillData.damage);
            }
        }

        private IEnumerator RepeatDamage(SkillDeployer skillDeployer)
        {
            float startTime = 0;
            do
            {
                DamageOnce(skillDeployer);
                yield return new WaitForSeconds(skillDeployer.SkillData.damageInterval);
                skillDeployer.CalculateTargets();
                startTime += skillDeployer.SkillData.damageInterval;
            }
            while (startTime < skillDeployer.SkillData.durationTime);
        }
    }
}