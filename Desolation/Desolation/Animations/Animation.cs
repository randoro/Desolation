using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Desolation
{
    class Animation
    {
        public AnimationType animationType { get; set; }
        public Vector2 position { get; set; }
        public double TTL { get; set; }
        public Animation(AnimationType animationType, Vector2 position, double TTL)
        {
            this.animationType = animationType;
            this.position = position;
            this.TTL = TTL;
        }


    }
}
