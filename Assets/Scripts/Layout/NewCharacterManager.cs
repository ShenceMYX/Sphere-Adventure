using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

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
                other.transform.parent = transform.parent.FindChildByName("Followers");
                other.transform.localPosition = new Vector3(0, 0, -1.34f);
                other.tag = "Player";
                other.gameObject.AddComponent<DragableObject>();
            }
        }
    }
}