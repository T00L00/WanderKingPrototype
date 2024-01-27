using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WK
{
    public class Leader : MonoBehaviour
    {        
        [SerializeField] private int followerCount;

        public int FollowerCount => followerCount;     
    }
}