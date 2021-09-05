using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Layout
{
	/// <summary>
	/// 
	/// </summary>
	public class SingleGrid : MonoBehaviour
	{
		public bool occupied;

        private void OnTriggerEnter(Collider other)
        {
			Debug.Log(other.name);
			if (other.CompareTag("Player"))
				occupied = true;
        }

        private void OnTriggerExit(Collider other)
        {
			if (other.CompareTag("Player"))
				occupied = false;
		}

    }
}