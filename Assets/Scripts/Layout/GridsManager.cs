using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace SphereAdventure.Layout
{
	/// <summary>
	/// 
	/// </summary>
	public class GridsManager : MonoSingleton<GridsManager>
	{
		public List<SingleGrid> allGrids = new List<SingleGrid>();

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                allGrids.Add(transform.GetChild(i).GetComponent<SingleGrid>());
            }
        }

        public SingleGrid GetAvaliableGrid()
        {
            foreach (var grid in allGrids)
            {
                if (!grid.occupied)
                {
                    return grid;
                }
            }
            return null;
        }

        
    }
}