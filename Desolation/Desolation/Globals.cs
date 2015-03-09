using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desolation
{

    public enum TagID { End, Byte, Short, Int, Long, Float, Double, ByteArray, String, List, Compound, IntArray };
    static class Globals
    {
        public static readonly String gamePath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly int ticksPerChunkLoad = 10000000;
    }
}
