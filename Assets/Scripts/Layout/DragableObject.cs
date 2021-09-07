using System.Collections;
using System.Collections.Generic;
using SphereAdventure.Character;
using UnityEngine;

namespace SphereAdventure.Layout
{
	/// <summary>
	/// 
	/// </summary>
	public class DragableObject : MonoBehaviour
	{
        private Vector3 mOffset;

        private float mZCoord;

        /// <summary>
        /// 通过射线检测 识别为标签为Player的可抓取物体 否则所有物体都可以被抓取了
        /// </summary>
        public bool canDragged;

        /// <summary>
        /// 是否正在抓取当前物体
        /// </summary>
        public bool isDragging;

        private Vector3 initialPos;
        public Vector3 targetPos;
        public SingleGrid currentGrid;
        public SingleGrid targetGrid;
        private SingleGrid lastTargetGrid;


        private void OnMouseDown()
        {
            if (!canDragged) return;
            if (!CharacterInputController.Instance.layoutOrganizing) return;

            initialPos = transform.position;

            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            // Store offset = gameobject world pos - mouse world pos
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

            if(currentGrid != null)
                currentGrid.occupied = false;
        }

        private Vector3 GetMouseAsWorldPoint()
        {
            // Pixel coordinates of mouse (x,y)
            Vector3 mousePoint = Input.mousePosition;

            // z coordinate of game object on screen
            mousePoint.z = mZCoord;

            // Convert it to world points
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }


        private void OnMouseDrag()
        {
            if (!canDragged) return;
            if (!CharacterInputController.Instance.layoutOrganizing) return;

            isDragging = true;

            //if (lastTargetGrid != null)
            //{
            //    if (lastTargetGrid != targetGrid)
            //        lastTargetGrid.spRenderer.color = lastTargetGrid.originalColor;
            //}
            //if (targetGrid != null)
            //{
            //    targetGrid.spRenderer.color = targetGrid.occupied ? Color.red : Color.green;
            //    lastTargetGrid = targetGrid;
            //}
            

            Vector3 target = GetMouseAsWorldPoint() + mOffset;
            transform.position = new Vector3(target.x, 0, target.z);
        }

        private void OnMouseUp()
        {
            canDragged = false;
            isDragging = false;
            //transform.position = targetPos == Vector3.zero ? initialPos : targetPos;
            if (targetGrid == null)
            {
                currentGrid.occupied = true;
                transform.position = currentGrid.transform.position;
            }
            else
            {
                targetGrid.occupied = true;
                transform.position = targetGrid.transform.position;
                currentGrid = targetGrid;
                targetGrid.spRenderer.color = targetGrid.originalColor;
            }
        }



    }
}