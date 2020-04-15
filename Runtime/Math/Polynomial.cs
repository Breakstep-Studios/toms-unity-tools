using UnityEngine;

public class Polynomial {
    //Solve cubic equation according to Cardano. 
    //Source: https://www.cs.rit.edu/~ark/pj/lib/edu/rit/numeric/Cubic.shtml
    public static void SolveCubic(out int nRoots, out float x1, out float x2, out float x3, float a, float b, float c, float d) {

        float TWO_PI = 2f * Mathf.PI;
        float FOUR_PI = 4f * Mathf.PI;

        // Normalize coefficients.
        float denom = a;
        a = b / denom;
        b = c / denom;
        c = d / denom;

        // Commence solution.
        float a_over_3 = a / 3f;
        float Q = (3f * b - a * a) / 9f;
        float Q_CUBE = Q * Q * Q;
        float R = (9f * a * b - 27f * c - 2f * a * a * a) / 54f;
        float R_SQR = R * R;
        float D = Q_CUBE + R_SQR;

        if (D < 0.0f) {

            // Three unequal real roots.
            nRoots = 3;
            float theta = Mathf.Acos(R / Mathf.Sqrt(-Q_CUBE));
            float SQRT_Q = Mathf.Sqrt(-Q);
            x1 = 2f * SQRT_Q * Mathf.Cos(theta / 3f) - a_over_3;
            x2 = 2f * SQRT_Q * Mathf.Cos((theta + TWO_PI) / 3f) - a_over_3;
            x3 = 2f * SQRT_Q * Mathf.Cos((theta + FOUR_PI) / 3f) - a_over_3;
        }

        else if (D > 0.0f) {

            // One real root.
            nRoots = 1;
            float SQRT_D = Mathf.Sqrt(D);
            float S = CubeRoot(R + SQRT_D);
            float T = CubeRoot(R - SQRT_D);
            x1 = (S + T) - a_over_3;
            x2 = float.NaN;
            x3 = float.NaN;
        }

        else {

            // Three real roots, at least two equal.
            nRoots = 3;
            float CBRT_R = CubeRoot(R);
            x1 = 2 * CBRT_R - a_over_3;
            x2 = CBRT_R - a_over_3;
            x3 = x2;
        }
    }

    //Mathf.Pow is used as an alternative for cube root (Math.cbrt) here.
    private static float CubeRoot(float d) {

        if (d < 0.0f) {

            return -Mathf.Pow(-d, 1f / 3f);
        }

        else {

            return Mathf.Pow(d, 1f / 3f);
        }
    }
}
