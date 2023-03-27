using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public enum state
    {
        Start,
        End,
        Blank,
        Hurdle,
        //Traversed,
        TraversedToSouth,
        TraversedToNorth,
        TraversedToEast,
        TraversedToWest,
        Backtracked,
        NoState
    };

    public enum dir
    {
        North,
        South,
        West,
        East,
        NA
    };
}
