﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DndKata.Domain
{
    public static class ModifierTable
    {
        public static Dictionary<int, int> GetModifierTable()
        {
            return new Dictionary<int, int>()
            {
                {1, -5},
                {2, -4},
                {3, -4},
                {4, -3},
                {5, -3},
                {6, -2},
                {7, -2},
                {8, -1},
                {9, -1},
                {10, 0},
                {11, 0},
                {12, 1},
                {13, 1},
                {14, 2},
                {15, 2},
                {16, 3},
                {17, 3},
                {18, 4},
                {19, 4},
                {20, 5},
            };
        }
    }
}
