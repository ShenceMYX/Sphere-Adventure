using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using SphereAdventure.Character;

namespace SphereAdventure.Skill
{
    /// <summary>
    /// 
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        //option+上/下 上下移动当前行
        //shift+command+d 向下复制
        //"cw"+tab+tab Console.WrtieLine
        //control+o 向下加入空行

        public Transform[] SelectTargets(SkillData skillData, Transform skillTF)
        {
            List<Transform> targets = new List<Transform>();
            for (int i = 0; i < skillData.attackTargetTags.Length; i++)
            {
                GameObject[] tempGOArr = GameObject.FindGameObjectsWithTag(skillData.attackTargetTags[i]);
                targets.AddRange(tempGOArr.Select(g => g.transform));
            }

            targets = targets.FindAll(t =>
                       Vector3.Distance(t.position, skillTF.position) <= skillData.attackDistance &&
                       Vector3.Angle(t.position - skillTF.position, skillTF.forward) <= skillData.attackAngle / 2
                       );

            targets = targets.FindAll(g => g.GetComponent<CharacterStatus>().currentHealth > 0);

            Transform[] results = targets.ToArray();

            if (skillData.attackType == SkillAttackType.Group || results.Length == 0)
                return results;

            Transform min = results.GetMin(g => Vector3.Distance(skillTF.position, g.position));
            return new Transform[] { min };
        }
    }
}