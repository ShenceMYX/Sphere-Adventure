using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphereAdventure.Layout
{
	/// <summary>
	/// 
	/// </summary>
	public class GridsManager : MonoBehaviour
	{
        private void Update()
        {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100))
			{
				if(hit.collider.CompareTag("Player"))
                {
					
                }
			}
		}
    }
}