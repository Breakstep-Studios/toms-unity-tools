using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace StudioName.Runtime.EditorGizmos
{
    /// <summary>
    /// Visualizes the bounds of a collider 2d (<see cref="BoxCollider2D"/> only for now) without it having to be selected
    /// </summary>
    public class Collider2DVisualizer : MonoBehaviour
    {
        /// <summary>
        /// The default color of the bounds in scene view.
        /// </summary>
        [Tooltip("The default color of the bounds in scene view.")]
        public Color defaultColor = Color.cyan;

        /// <summary>
        /// The collider 2d to visualize.
        /// </summary>
        [Tooltip("The collider 2d to visualize.")]
        public Collider2D collider2D;
        private void OnDrawGizmos()
        {
            if (collider2D == null)
            {
                return;
            }
            Gizmos.color = defaultColor;
            Vector2[] points;
            var cachedTransform = transform;
            switch (collider2D)
            {
                case BoxCollider2D boxCollider2D:
                    Gizmos.matrix = Matrix4x4.TRS(transform.TransformPoint(boxCollider2D.offset),
                        cachedTransform.rotation, cachedTransform.lossyScale);
                    Gizmos.DrawWireCube(Vector3.zero,boxCollider2D.size);
                    break;
                case CompositeCollider2D compositeCollider2D:
                    Gizmos.matrix = Matrix4x4.TRS(transform.position,
                        cachedTransform.rotation, Vector3.one);
                    for (var i = 0; i < compositeCollider2D.pathCount; ++i)
                    {
                        // see https://answers.unity.com/questions/1456235/how-to-get-specific-path-in-a-composite-collider2d.html
                        points = new Vector2[compositeCollider2D.GetPathPointCount(i)];
                        //this method fills the points above 
                        compositeCollider2D.GetPath(i, points);
                        for (var j = 0; j < points.Length; ++j)
                        {
                            Gizmos.DrawLine((Vector3) points[j],
                                 (Vector3) points[(j + 1) % points.Length]);
                        }
                    }
                    break;
                case PolygonCollider2D polygonCollider2D:
                    Gizmos.matrix = Matrix4x4.TRS(transform.position,
                        cachedTransform.rotation, cachedTransform.lossyScale);
                    points = polygonCollider2D.points;
                    for (var j = 0; j < points.Length; ++j)
                    {
                        Gizmos.DrawLine((Vector3) points[j],
                            (Vector3) points[(j + 1) % points.Length]);
                    }
                    break;
                case CapsuleCollider2D capsuleCollider2D:
                case CircleCollider2D circleCollider2D:
                case EdgeCollider2D edgeCollider2D:
                case TilemapCollider2D tilemapCollider2D:
                default:
                    throw new ArgumentOutOfRangeException(nameof(collider2D));
            }
        }
        
    }
}