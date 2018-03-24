using System;
using System.IO;
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

        public static string RandomLine(string[] lines)
        {

            using (StreamReader r = new StreamReader(fileName))
            {
                int length = 0;
                while (r.ReadLine() != null) { length++; }
            }
            using (StreamReader r = new StreamReader(fileName))
            { 
                while (r.ReadLine() != )
            }
            int count = file.Count();
            string line = file.Skip(UnityEngine.Random.Range(0, count)).First();
            return line;
        }
    }
}
