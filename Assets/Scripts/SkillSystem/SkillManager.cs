using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace SphereAdventure.Skill
{
	/// <summary>
	/// 技能管理器
	/// </summary>
	public class SkillManager : MonoBehaviour
	{
		public SkillData[] skills;

        private void Start()
        {
            InitSkills();
        }

        private void InitSkills()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i].skillPrefab = ResourceManager.Load<GameObject>(skills[i].prefabName);
                skills[i].owner = gameObject;
            }
        }

        public SkillData PrepareSkill(int skillID)
        {
            SkillData skillData = skills.Find(s => s.skillID == skillID);
            if (skillData.coolRemain <= 0)
                return skillData;
            else
                return null;
        }

        public void GenerateSkill(SkillData data)
        {
            GameObject skillGO = GameObjectPool.Instance.CreateObject(data.skillName, data.skillPrefab, transform.position, transform.rotation);
            //GameObject skillGO = Instantiate(data.skillPrefab, transform.position, transform.rotation);
            SkillDeployer skillDeployer = skillGO.GetComponent<SkillDeployer>();
            skillDeployer.SkillData = data;
            skillDeployer.DeploySkill();
            //Destroy(skillGO, data.durationTime);
            GameObjectPool.Instance.CollectObject(skillGO, data.durationTime);
            StartCoroutine(SkillCoolDown(data));
        }

        private IEnumerator SkillCoolDown(SkillData data)
        {
            data.coolRemain = data.coolTime;
            while (data.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                data.coolRemain--;
            }
        }

	}
}