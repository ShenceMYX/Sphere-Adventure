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
		private Transform followersTrans;

		//进行排兵布阵时 原来跟随玩家运动的子物体 需要取消它们与玩家的父子关系 否则排兵布阵时也会跟随玩家一起运动
		private List<GameObject> flexibleParentObjs = new List<GameObject>();
		//进行排兵布阵时 关闭吸引其他小球的触发器 否则会影响DragableObject的OnMouseDown OnMouseDrag的碰撞检测 
		private Transform attractor;

		private LayerMask layer;

        public event Action<Vector3> ShootHandler;
        public event Action<Vector3> ExplodeAttckHandler;

        public float dashIterval = 0.7f;
        private float startDashTime;

        private void Start()
        {
			motor = GetComponent<CharacterMotor>();
			gridLayout = transform.FindChildByName("Grids").gameObject;
			followersTrans = transform.FindChildByName("Followers");

			flexibleParentObjs.Add(gridLayout);
			flexibleParentObjs.Add(followersTrans.gameObject);
			attractor = transform.FindChildByName("Attractor");
			layer = LayerMask.GetMask("Player");
		}

        private void Update()
        {
            //按下tab 进行排兵布阵的切换
            ArrangeLayoutControl();

            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (layoutOrganizing)
            {
                if (Physics.Raycast(ray, out hit, 100, layer))
                {
                    //Debug.Log(hit.collider.name);
                    //Debug.DrawLine(ray.origin, hit.point);

                    if (hit.collider.CompareTag("Player"))
                        hit.collider.GetComponent<DragableObject>().canDragged = true;
                }
            }

            //如果正在排兵布阵 则小球不能移动 旋转
            if (layoutOrganizing) return;

            //xInput = Input.GetAxis("Horizontal");
            //yInput = Input.GetAxis("Vertical");

            //Vector3 move = Vector3.right * xInput + Vector3.forward * yInput;
            //if (xInput != 0 || yInput != 0)
            //	motor.Dash(move);

            if (Input.GetMouseButtonDown(1))
            {
                if(startDashTime < Time.time)
                {
                    motor.Dash(transform.forward);
                    ExplodeAttckHandler?.Invoke(transform.forward);
                    startDashTime = Time.time + dashIterval;
                }
            }

            //屏幕鼠标位置到世界坐标的射线检测
            if (Physics.Raycast(ray, out hit, 100))
            {
                Vector3 dir = hit.point - transform.position;

                //Debug.Log(dir.magnitude);
                if (dir.magnitude > 3f)
                {
                    motor.RotateToTarget(new Vector3(hit.point.x, transform.position.y, hit.point.z));

                    dir.Normalize();
                    //Debug.DrawLine(hit.point, transform.position);
                    motor.Movement(new Vector3(dir.x, 0, dir.z));
                }

                if (Input.GetMouseButtonDown(0))
                    ShootHandler?.Invoke(hit.point);

            }

            //transform.position = followersTrans.GetChild(0).position;
        }

        private void ArrangeLayoutControl()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                layoutOrganizing = !layoutOrganizing;
                for (int i = 0; i < gridLayout.transform.childCount; i++)
                {
                    gridLayout.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().enabled = layoutOrganizing;
                }
                //gridLayout.SetActive(layoutOrganizing);
                attractor.gameObject.SetActive(!layoutOrganizing);
                SetGridNFollowersParent(layoutOrganizing);

                //if (!layoutOrganizing)
                //{
                //    transform.position = -followersTrans.GetChild(0).position;
                //    followersTrans.GetChild(0).position = Vector3.zero;
                //}
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