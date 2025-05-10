using System;
using UnityEngine;

namespace Attributes
{
    public class Restrict : PropertyAttribute
    {
        public Type Type { get; private set; }

        public Restrict(Type type)
        {
            Type = type;
        }
    }
}