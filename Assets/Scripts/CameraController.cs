using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure
{
	/// <summary>
	/// 摄像机控制器	
	/// </summary>
	public class CameraController : MonoBehaviour
	{
		public Transform target;
		public Vector3 offset;
		public float smoothTime = 0.01f;
		private Vector3 cameraVelocity = Vector3.zero;
		private Camera mainCamera;

        private void Awake()
        {
			mainCamera = Camera.main;
        }

        private void Update()
        {
			transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref cameraVelocity, smoothTime);
        }

    }
}