using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Skill
{
	/// <summary>
	/// 
	/// </summary>
	public interface IAttackSelector
	{
		Transform[] SelectTargets(SkillData skillData, Transform skillTF);
	}
}