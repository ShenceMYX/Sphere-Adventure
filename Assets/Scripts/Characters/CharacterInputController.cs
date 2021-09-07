using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using SphereAdventure.Layout;
using UnityEngine;

namespace SphereAdventure.Character
{
	/// <summary>
	/// 玩家角色控制器
	/// </summary>
	public class CharacterInputController : MonoSingleton<CharacterInputController>
	{
		private float xInput;
		private float yInput;

		private CharacterMotor motor;

		public bool layoutOrganizing;

		private GameObject gridLayout;

		//进行排兵布阵时 原来跟随玩家运动的子物体 需要取消它们与玩家的父子关系 否则排兵布阵时也会跟随玩家一起运动
		private List<GameObject> flexibleParentObjs = new List<GameObject>();
		//进行排兵布阵时 关闭吸引其他小球的触发器 否则会影响DragableObject的OnMouseDown OnMouseDrag的碰撞检测 
		private Transform attractor;

		private LayerMask layer;

        private void Start()
        {
			motor = GetComponent<CharacterMotor>();
			gridLayout = transform.FindChildByName("Grids").gameObject;

			flexibleParentObjs.Add(gridLayout);
			flexibleParentObjs.Add(transform.FindChildByName("Followers").gameObject);
			attractor = transform.FindChildByName("Attractor");
			layer = LayerMask.GetMask("Player");
		}

        private void Update()
        {
			if (Input.GetKeyDown(KeyCode.Tab))
			{
				layoutOrganizing = !layoutOrganizing;
				gridLayout.SetActive(layoutOrganizing);
				attractor.gameObject.SetActive(!layoutOrganizing);
				SetGridNFollowersParent(layoutOrganizing);
			}

			RaycastHit hit;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (layoutOrganizing)
			{
                if (Physics.Raycast(ray, out hit, 100, layer))
                {
					//Debug.Log(hit.collider.name);
					Debug.DrawLine(ray.origin, hit.point);

					if (hit.collider.CompareTag("Player"))
                        hit.collider.GetComponent<DragableObject>().canDragged = true;
                }
            }


			//如果正在排兵布阵 则小球不能移动 旋转
			if (layoutOrganizing) return;

			xInput = Input.GetAxis("Horizontal");
			yInput = Input.GetAxis("Vertical");

			Vector3 move = Vector3.right * xInput + Vector3.forward * yInput;
			motor.Movement(move);

			if (Physics.Raycast(ray, out hit, 100))
			{
				motor.RotateToTarget(new Vector3(hit.point.x, transform.position.y, hit.point.z));
			}
		}

        private void SetGridNFollowersParent(bool isModifyingLayout)
        {
            foreach (var go in flexibleParentObjs)
            {
				go.transform.parent = isModifyingLayout ? null : transform;
            }
        }
    }
}