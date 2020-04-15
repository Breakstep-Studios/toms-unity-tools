using System;
using UnityEngine;

namespace StudioName.Runtime.Math {
    /// <summary>
    /// Used to create a parabola and extract properties associated with it.
    /// </summary>
    public class Parabola {
        private float a;
        private float b;
        private float c;
        /// <summary>
        /// To reduce floating point imprecision(spatial jitter) localVertex is placed as close to origin as possible.
        /// <para>localVertex = (vertex - <see cref="graphOffset"/></para>
        /// </summary>
        private Vector2 localVertex;
        /// <summary>
        /// The offset distance to the actual "parabola" and it's points.
        /// </summary>
        private Vector2 graphOffset;
        
        /// <summary>
        /// Create parabola from a vertex and some arbitrary point that will lie on the parabola.
        /// </summary>
        /// <param name="vertex">The axis of symmetry of the parabola.</param>
        /// <param name="parabolaPoint">A point that lies on the parabola.</param>
        public Parabola(Vector2 vertex, Vector2 parabolaPoint) {
            parabolaPoint -= (parabolaPoint.x > vertex.x)? new Vector2((parabolaPoint.x-vertex.x)*2,0) : Vector2.zero;
            graphOffset = parabolaPoint;
            parabolaPoint = Vector2.zero;
            vertex -= graphOffset;
            var components = CalculateParabolicComponents(vertex,parabolaPoint);
            a = components.Item1;
            b = components.Item2;
            c = components.Item3;
            localVertex = vertex;
        }
        
        /// <summary>
        /// Create parabola from a, b, and c components of a parabolic equation y=ax^2 + bx + c.
        /// </summary>
        /// <param name="a">The coefficient of x^2 in the parabolic equation.</param>
        /// <param name="b">The coefficient of x in the parabolic equation.</param>
        /// <param name="c">The constant term of the parabolic equation.s</param>
        public Parabola(float a, float b, float c) {
            this.a = a;
            this.b = b;
            this.c = c;
            localVertex = CalculateVertex(a, b, c);
        }
        
        /// <summary>
        /// Return the x coordinate associated with it's y counterpart on the parabola.
        /// <para>Note if passing in a y value outside the parabola null will be returned.</para>
        /// </summary>
        /// <param name="y">Y coordinate counterpart.</param>
        /// <param name="rightOfVertex">Get x value to the right of left of the vertex.</param>
        /// <returns>The x coordinate associated with the y coordinate passed in. || null if none is found</returns>
        public float? GetXCoordinate(float y, bool rightOfVertex = true) {
            y -= graphOffset.y;
            //x = sqrt((y - k)/a) + h derived from vertex form
            return ((rightOfVertex ? 1 : -1) * Mathf.Sqrt((y - localVertex.y) / a) + localVertex.x)+graphOffset.x;
        }

        /// <summary>
        /// Return the y coordinate associated with it's x counterpart on the parabola.
        /// </summary>
        /// <param name="x">x coordinate counterpart</param>
        /// <returns>The y coordinate associated with the x coordinate passed in.</returns>
        public float GetYCoordinate(float x) {
            x -= graphOffset.x;
            //y = ax^2 + bx + c
            return (a * Mathf.Pow( x, 2f ) + b*x + c) + graphOffset.y;
        }

        /// <summary>
        /// Checks if the given point is encapsolated by parabolas curve.
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <returns>true if point is contained, false otherwise.</returns>
        public bool ContainsPoint(Vector2 point) {
            point -= graphOffset;
            if (a > 0) { 
                //y >= ax^2 + bx + c
                return (point.y >= a * Mathf.Pow(point.x, 2) + b * point.x + c); 
            }
            
            //y <= ax^2 +bx + c
            return (point.y <= a * Mathf.Pow(point.x, 2) + b * point.x + c); 
        }

        /// <summary>
        /// Given an arbitrary point will return the closest point to it on the parabola.
        /// </summary>
        /// <param name="point">Arbitrary point.</param>
        /// <returns>The closest point on the parabola to the point given.</returns>
        public Vector2 ClosestPoint(Vector2 point) {
            point -= graphOffset;
            
            //find x values from distance formula derivative (4a^2x^3 + 6abx^2 +4acx + 2b^2x + 2x - 4chx + 2bc - 2bh -2g)
            var x3 = 4 * Mathf.Pow(a, 2);
            var x2 = 6 * a * b;
            var x = (4 * a * c) + (2 * Mathf.Pow(b, 2)) + 2 - (4 * a * point.y);
            var coef = (2 * b * c) - (2 * b * point.y) - (2 * point.x);

            //find the roots of our distance derivative
            int rootAmount;
            var roots = new float[3];
            Polynomial.SolveCubic(out rootAmount, out roots[0], out roots[1], out roots[2], x3, x2, x, coef);

            //evaluate root with smallest distance using distance formula d = sqrt((x-g)^2 + (ax^2+bx+c-h)^2)
            Func<float,float> distanceFormula = (xRoot) => Mathf.Sqrt(Mathf.Pow(xRoot - point.x, 2) +
                                                                  Mathf.Pow((a * Mathf.Pow(xRoot, 2)) + (b * xRoot) + c - point.y,2));
            var absMin = distanceFormula(roots[0]);
            var smallestRootIndex = 0;
            for (var i = 0; i < rootAmount; ++i) {
                var newAbsMin = distanceFormula(roots[i]);
                if (newAbsMin < absMin) {
                    smallestRootIndex = i;
                    absMin = newAbsMin;
                }
            }

            roots[smallestRootIndex] += graphOffset.x;
            //find associated y with newly found min x and return point
            return new Vector2(roots[smallestRootIndex], GetYCoordinate(roots[smallestRootIndex]));
        }

        /// <summary>
        /// Calculates the vertex from the given a, b and c components of a parabola.
        /// </summary>
        /// <param name="a">The coefficient of x^2 in the parabolic equation.</param>
        /// <param name="b">The coefficient of x in the parabolic equation.</param>
        /// <param name="c">The constant term of the parabolic equation.s</param>
        /// <returns>The calculated vertex.</returns>
        public static Vector2 CalculateVertex(float a, float b, float c) {
            //find x of axis of symmetry of parabola using axis of sym equation x = -b/2a
            var xAxisOfSymmetry = -b / (2 * a);
            //find y of axis of symmetry by plugging into og equation
            return new Vector2(xAxisOfSymmetry, (a * Mathf.Pow( xAxisOfSymmetry, 2f ) + b*xAxisOfSymmetry + c));
        }
        
        /// <summary>
        /// Calculates the a, b, and c components of the parabola.
        /// </summary>
        /// <param name="vertex">The vertex of the parabola.</param>
        /// <param name="parabolaPoint">A point other then the vertex that lies on the parabola.</param>
        /// <returns>A tuple containing the result of a, b, and c respectively</returns>
        public static Tuple<float,float,float> CalculateParabolicComponents(Vector2 vertex, Vector2 parabolaPoint) {
            //a = (y - k) / (x - h)^2 derived from vertex form
            var a = (parabolaPoint.y - vertex.y) / Mathf.Pow(parabolaPoint.x - vertex.x,2);
            //b & c computed from expanded vertex form y = ax^2 - 2ahx + ah^2 + k where b = -2ahx & c = ah^2 + k
            var b = (-2 * a * vertex.x);
            var c = a * Mathf.Pow(vertex.x, 2) + vertex.y;
            
            return new Tuple<float,float,float>(a,b,c);
        }
        
        /// <summary>
        /// The calculated a value of the parabola. See parabola Standard Form.
        /// <para>This value affects how wide or narrow the graph is.</para>
        /// </summary>
        public float A {
            get { return a; }
        }

        /// <summary>
        /// The calculated b value of the parabola. See parabola Standard Form.
        /// <para>This value affects the displacement of the vertex from the y-axis.</para>
        /// </summary>
        public float B {
            get { return b; }
        }

        /// <summary>
        /// The calculated c value of the parabola. See parabola Standard Form.
        /// <para>This value will vertically shift the parabola.</para>
        /// </summary>
        public float C {
            get { return c; }
        }

        /// <summary>
        /// The point at which the parabola crosses it's axis of symmetry.
        /// <para>In other words the lowest or highest point of the parabola.</para>
        /// </summary>
        public Vector2 Vertex {
            get { return localVertex + graphOffset; }
        }

        /// <summary>
        /// Returns the standard form of the parabola with a, b, and c replaced with actual values.
        /// <para>y = ax^2 + bx + c</para>
        /// </summary>
        public string StandardForm {
            get { return "y = " + a + "x^2 + " + b + "x + " + c; }
        }
        
        /// <summary>
        /// Returns the vertex form of the parabola with h, and k replaced with actual values.
        /// <para>y = a(x - h)^2 + k</para>
        /// </summary>
        public string VertexForm {
            get { return "y = " + a + "(x - " + Vertex + ")^2 + " + Vertex; }
        }
    } 
}