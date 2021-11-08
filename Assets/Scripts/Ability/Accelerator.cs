using System.Collections;
using System.Collections.Generic;
using SphereAdventure.Character;
using UnityEngine;

namespace SphereAdventure.Ability
{
	/// <summary>
	/// 
	/// </summary>
	public class Accelerator : AbiliyReleaser
	{
        private void OnEnable()
        {
            //CharacterMotor.Instance.moveSpeed += 5;
        }

        private void OnDestroy()
        {
            //CharacterMotor.Instance.moveSpeed -= 5;
        }
    }
}