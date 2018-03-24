using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class Extensions
    {
        public static GameObject Find(this MonoBehaviour script, string tag)
        {
            var result = GameObject.FindGameObjectWithTag(tag);
            if (result == null)
                throw new Exception();

            return result;
        }

        public static T Find<T>(this MonoBehaviour script, string tag)
            where T : MonoBehaviour
        {
            var result = Find(script, tag)
                .GetComponent<T>();

            if (result == null)
                throw new Exception();

            return result;
        }

        public static T FindInChild<T>(this MonoBehaviour script, string tag)
        {
            var result = Find(script, tag)
                .GetComponentInChildren<T>();

            if (result == null)
                throw new Exception();

            return result;
        }

        public static int GetPoisson(double lambda)
        {
            return (lambda < 30.0) ? PoissonSmall(lambda) : PoissonLarge(lambda);
        }

        private static int PoissonSmall(double lambda)
        {
            // Algorithm due to Donald Knuth, 1969.
            double p = 1.0, L = Math.Exp(-lambda);
            int k = 0;
            do
            {
                k++;
                p *= GetUniform();
            }
            while (p > L);
            return k - 1;
        }

        private static int PoissonLarge(double lambda)
        {
            // "Rejection method PA" from "The Computer Generation of 
            // Poisson Random Variables" by A. C. Atkinson,
            // Journal of the Royal Statistical Society Series C 
            // (Applied Statistics) Vol. 28, No. 1. (1979)
            // The article is on pages 29-35. 
            // The algorithm given here is on page 32.

            double c = 0.767 - 3.36 / lambda;
            double beta = Math.PI / Math.Sqrt(3.0 * lambda);
            double alpha = beta * lambda;
            double k = Math.Log(c) - lambda - Math.Log(beta);

            for (; ; )
            {
                double u = GetUniform();
                double x = (alpha - Math.Log((1.0 - u) / u)) / beta;
                int n = (int)Math.Floor(x + 0.5);
                if (n < 0)
                    continue;
                double v = GetUniform();
                double y = alpha - beta * x;
                double temp = 1.0 + Math.Exp(y);
                double lhs = y + Math.Log(v / (temp * temp));
                double rhs = k + n * Math.Log(lambda) - LogFactorial(n);
                if (lhs <= rhs)
                    return n;
            }
        }
    }
}
