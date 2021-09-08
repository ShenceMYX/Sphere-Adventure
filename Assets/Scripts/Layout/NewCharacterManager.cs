using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using SphereAdventure.Ability;

namespace SphereAdventure.Layout
{
	/// <summary>
	/// 
	/// </summary>
	public class NewCharacterManager : MonoBehaviour
	{
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("NewCharacter"))
            {
                if (GridsManager.Instance.GetAvaliableGrid() == null) { Debug.Log("No Available Space"); return; }
                other.transform.parent = transform.parent.FindChildByName("Followers");
                other.transform.localPosition = GridsManager.Instance.GetAvaliableGrid().transform.localPosition;
                other.tag = "Player";
                other.gameObject.AddComponent<DragableObject>();
                if (other.GetComponent<AbiliyReleaser>() != null)
                    other.GetComponent<AbiliyReleaser>().enabled = true;
            }
        }
    }
}