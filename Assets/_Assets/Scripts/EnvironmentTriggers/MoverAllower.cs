using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverAllower : MonoBehaviour
{
    
    [SerializeField] private bool canMoveX;
    [SerializeField] private bool canMoveZ;
    [SerializeField] private Transform[] limits;
    
    public bool CanMoveX => canMoveX;
    public bool CanMoveZ => canMoveZ;
    public Transform[] Limits => limits;
}
