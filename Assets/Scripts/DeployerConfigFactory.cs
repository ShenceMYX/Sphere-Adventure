using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Skill
{
	/// <summary>
	/// 
	/// </summary>
	public class DeployerConfigFactory
	{
		private static Dictionary<string, object> cache = new Dictionary<string, object>();

		public static IAttackSelector CreateAttackSelector(SkillData skillData)
		{
			string className = string.Format("SphereAdventure.Skill.{0}AttackSelector", skillData.selectorType);
			return CreateObject<IAttackSelector>(className);
		}

		public static IImapctEffect[] CreateImpactEffect(SkillData skillData)
		{
			IImapctEffect[] impactEffects = new IImapctEffect[skillData.impactType.Length];
			for (int i = 0; i < skillData.impactType.Length; i++)
			{
				string className = string.Format("SphereAdventure.Skill.{0}ImpactEffect", skillData.impactType[i]);
				impactEffects[i] = CreateObject<IImapctEffect>(className);
			}
			return impactEffects;
		}

		private static T CreateObject<T>(string className) where T : class
		{
			if (!cache.ContainsKey(className))
            {
				Type type = Type.GetType(className);
				object instance = Activator.CreateInstance(type);
				cache.Add(className, instance);
			}
			
			return cache[className] as T;
		}
	}
}