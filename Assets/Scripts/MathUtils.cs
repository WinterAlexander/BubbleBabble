using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class MathUtils
    {
        public static float XZDist(Vector3 a, Vector3 b)
        {
            return Mathf.Sqrt(
                (a.x - b.x) * (a.x - b.x) +
                (a.z - b.z) * (a.z - b.z)
                );
        }
    }
}
