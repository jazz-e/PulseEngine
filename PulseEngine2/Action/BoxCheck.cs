using Microsoft.Xna.Framework;
using PulseEngine.Component.Collision;

namespace PulseEngine.Action
{
    public enum CollisionSide
    {
        None, Top, Bottom, Left, Right
    }

    public class BoxCheck
    {
        static BoxCheck instance = null;

        public CollisionSide CollisionState { get; set; }

        public static int Penetration { get; set; }

        public static BoxCheck GetInstance(Rectangle primary,
            Rectangle secondary)
        {
            if (instance == null)
                instance = new BoxCheck(primary, secondary);

            return instance;
        }

        public BoxCheck(Rectangle primary,
            Rectangle secondary)
        {
            Rectangle Top =
                new Rectangle(primary.Left, primary.Top - Penetration,
                primary.Width, Penetration);
            Rectangle Bottom =
                new Rectangle(primary.Left, primary.Bottom,
                primary.Width, Penetration);
            Rectangle Left =
                new Rectangle(primary.Left - Penetration, 
                primary.Top + Penetration, Penetration, 
                primary.Height- Penetration);
            Rectangle Right =
                new Rectangle(primary.Right,
                primary.Top + Penetration, Penetration,
                primary.Height - Penetration);

            if (secondary.Intersects(Left))
            { CollisionState = CollisionSide.Left; return; }
            if (secondary.Intersects(Right))
            { CollisionState = CollisionSide.Right; return; }
            if (secondary.Intersects(Top))
            { CollisionState = CollisionSide.Top; return; }
            if (secondary.Intersects(Bottom))
            { CollisionState = CollisionSide.Bottom; return; }

            CollisionState = CollisionSide.None;
        }
    }
}
