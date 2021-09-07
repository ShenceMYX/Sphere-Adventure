using System.Collections;
using System.Collections.Generic;
using SphereAdventure.Character;
using UnityEngine;

namespace SphereAdventure.Layout
{
	/// <summary>
	/// 
	/// </summary>
	public class SingleGrid : MonoBehaviour
	{
		public bool occupied;

		private Color occupiedColor;
		private Color unoccupiedColor;
		public Color originalColor;

		public SpriteRenderer spRenderer;

		private float mZCoord;

		//在组件被启用的第一帧检测物体是否在碰撞器内
		private bool checkGirdOccupiedOnce = false;

		private void Start()
        {
			occupiedColor = Color.red;
			unoccupiedColor = Color.green;
			spRenderer = GetComponentInChildren<SpriteRenderer>();
			originalColor = spRenderer.color;

			mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
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

        private void OnEnable()
        {
			checkGirdOccupiedOnce = true;
			StartCoroutine(UnckeckGirdOccupied());
		}

        private void OnTriggerEnter(Collider other)
        {
			if (other.CompareTag("Player"))
            {
				//如果正在 排兵布阵 且射线检测到当前物体为带有Player标签带可抓取物体
				if (other.GetComponent<DragableObject>().canDragged)
                {
					//如果正在拖动该物体
                    if (other.GetComponent<DragableObject>().isDragging)
                    {
						if (!occupied)
						{
							other.GetComponent<DragableObject>().targetPos = transform.position;
							other.GetComponent<DragableObject>().targetGrid = this;
							spRenderer.color = unoccupiedColor;
						}
                        else
                            spRenderer.color = occupiedColor;
                    }
					


				}

                if (checkGirdOccupiedOnce)
                {
					occupied = true;
					other.GetComponent<DragableObject>().currentGrid = this;
				}



			}
		}

		private IEnumerator UnckeckGirdOccupied()
        {
			yield return new WaitForSeconds(0.1f);
			checkGirdOccupiedOnce = false;
		}

		private void OnTriggerStay(Collider other)
        {
			if (other.CompareTag("Player"))
			{
				if (other.GetComponent<DragableObject>().canDragged)
				{
					if (other.GetComponent<DragableObject>().isDragging)
                    {
						if (!occupied)
						{
							other.GetComponent<DragableObject>().targetPos = transform.position;
							other.GetComponent<DragableObject>().targetGrid = this;
						}
                    }
					


				}
			}
		}

        private void OnTriggerExit(Collider other)
        {
			if (other.CompareTag("Player"))
            {
				if (other.GetComponent<DragableObject>().canDragged)
				{
                    if (other.GetComponent<DragableObject>().isDragging)
                    {
						other.GetComponent<DragableObject>().targetPos = Vector3.zero;
						other.GetComponent<DragableObject>().targetGrid = null;
						spRenderer.color = originalColor;
					}
				
				}
				
			}
		}

        private void Update()
        {
			//spRenderer.color = occupied ? occupiedColor : unoccupiedColor;
        }

    }
}