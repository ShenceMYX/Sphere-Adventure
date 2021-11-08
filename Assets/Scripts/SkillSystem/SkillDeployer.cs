using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Skill
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class SkillDeployer : MonoBehaviour
	{
		private SkillData skillData;
        public SkillData SkillData
		{
			get
			{
				return skillData;
			}
			set
			{
				skillData = value;
				InitDeployer();
			}
		}

		private IAttackSelector attackSelector;
		private IImapctEffect[] impactEffects;

        private void InitDeployer()
        {
			attackSelector = DeployerConfigFactory.CreateAttackSelector(skillData);
			impactEffects = DeployerConfigFactory.CreateImpactEffect(skillData);
		}

		public void CalculateTargets()
        {
			skillData.attackTargets = attackSelector.SelectTargets(skillData, transform);
        }

		public void ImpactTargets()
        {
            for (int i = 0; i < impactEffects.Length; i++)
            {
				impactEffects[i].Execute(this);
            }
        }

		public abstract void DeploySkill();
	}
}