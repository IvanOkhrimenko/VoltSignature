using System;
using System.Collections.Generic;
using System.Text;

namespace VoltSignature.DbCore.Entity.Base
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CollectionNameAttribute : Attribute
    {
        public CollectionNameAttribute(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName)) 
                throw new ArgumentException("Empty collectionname not allowed", "value");

            this.Name = collectionName;
        }

        public virtual string Name { get; private set; }
    }
}
