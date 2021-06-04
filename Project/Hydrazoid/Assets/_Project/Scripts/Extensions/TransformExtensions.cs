using UnityEngine;

namespace Hydrazoid.Extensions
{
    public static class TransformExtensions
    {
        public static Vector3 NormalDirectionTo(this Transform source, Vector3 destination)
        {
            return Vector3.Normalize(destination - source.position);
        }
    }
}