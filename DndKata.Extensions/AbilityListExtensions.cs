using System;
using System.Collections.Generic;
using System.Linq;
using DndKata.Contracts;

namespace DndKata.Extensions
{
    public static class AbilityListExtensions
    {
        public static bool ContainsAbility(this List<IAbility> collection, Type type)
        {
            return collection.Any(i => i.GetType() == type);
        }
    }
}
