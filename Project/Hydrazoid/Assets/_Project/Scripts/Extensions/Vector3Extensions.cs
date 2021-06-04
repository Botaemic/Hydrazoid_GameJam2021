using UnityEngine;

namespace Hydrazoid.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Return a copy of this vector with an altered x and/or y and/or z component
        /// </summary>
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }

  
        #region Distance
        /// <summary>
        /// Return a float. the distance between to points
        /// </summary>
        public static float DistanceTo(this Vector3 source, GameObject destination)
        {
            return Vector3.Magnitude(destination.transform.position - source);
        }

        /// <summary>
        /// Return a float. the distance between to points
        /// </summary>
        public static float DistanceTo(this Vector3 source, Transform destination)
        {
            return Vector3.Magnitude(destination.position - source);
        }

        /// <summary>
        /// Return a float. the distance between to points
        /// </summary>
        public static float DistanceTo(this Vector3 source, Vector3 destination)
        {
            return Vector3.Magnitude(destination - source);
        }

        /// <summary>
        /// Return a float. the squared distance between to points
        /// </summary>
        public static float SrqDistanceTo(this Vector3 source, GameObject destination)
        {
            return source.SqrDistanceTo(destination.transform.position);
        }

        /// <summary>
        /// Return a float. the squared distance between to points
        /// </summary>
        public static float SqrDistanceTo(this Vector3 source, Transform destination)
        {
            return source.SqrDistanceTo(destination.position);
        }

        /// <summary>
        /// Return a float. the sqoured distance between to points
        /// </summary>
        public static float SqrDistanceTo(this Vector3 source, Vector3 destination)
        {
            return Vector3.SqrMagnitude(destination - source);
        }
        #endregion

        #region Direction (sized)
        /// <summary>
        /// Return a Vector3, the direction from source to destination
        /// </summary>
        public static Vector3 DirectionTo(this Vector3 source, GameObject destination)
        {
            return source.DirectionTo(destination.transform.position);
        }

        /// <summary>
        /// Return a Vector3, the direction from source to destination
        /// </summary>
        public static Vector3 DirectionTo(this Vector3 source, Transform destination)
        {
            return source.DirectionTo(destination.position);
        }

        /// <summary>
        /// Return a Vector3, the direction from source to destination
        /// </summary>
        public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
        {
            return (destination - source);
        }
        #endregion

        #region Direction (Normalized)
        /// <summary>
        /// Return a normalized Vector3, the direction from source to destination
        /// </summary>
        public static Vector3 NormalDirectionTo(this Vector3 source, GameObject destination)
        {
            return source.NormalDirectionTo(destination.transform.position);
        }

        /// <summary>
        /// Return a normalized Vector3, the direction from source to destination
        /// </summary>
        public static Vector3 NormalDirectionTo(this Vector3 source, Transform destination)
        {
            return source.NormalDirectionTo(destination.position);
        }

        /// <summary>
        /// Return a normalized Vector3, the direction from source to destination
        /// </summary>
        public static Vector3 NormalDirectionTo(this Vector3 source, Vector3 destination)
        {
            return Vector3.Normalize(destination - source);
        }
        #endregion
    }
}
