using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MoverPlatform : MonoBehaviour
{
    [SerializeField] private int movementSpeed = 8;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CharacterController character))
        {
            character.Move(this.transform.right* movementSpeed * Time.deltaTime);
        }
    }
    
}
