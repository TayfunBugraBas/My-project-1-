using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Ahmet
{
   public abstract class Contructor : MonoBehaviour
   {
      [SerializeField, ColorUsage(true, true)] public Color constructorColor;
      internal Color[] color;
      internal List<Vector3> colorPos = new List<Vector3>();

   }
}
