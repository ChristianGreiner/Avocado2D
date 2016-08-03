using Microsoft.Xna.Framework;
using System;
using System.Globalization;

namespace Avocado2D.Util
{
    public struct RectangleF
    {
        public static readonly RectangleF Empty = new RectangleF();

        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public float Left
        {
            get { return X; }
        }

        public float Top
        {
            get { return Y; }
        }

        public float Right
        {
            get { return X + Width; }
        }

        public float Bottom
        {
            get { return Y + Height; }
        }

        public bool IsEmpty
        {
            get { return (Width <= 0) || (Height <= 0); }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RectangleF))
                return false;

            var comp = (RectangleF)obj;

            return (comp.X == X) &&
                   (comp.Y == Y) &&
                   (comp.Width == Width) &&
                   (comp.Height == Height);
        }

        public static bool operator ==(RectangleF left, RectangleF right)
        {
            return left.X == right.X
                   && left.Y == right.Y
                   && left.Width == right.Width
                   && left.Height == right.Height;
        }

        public static bool operator !=(RectangleF left, RectangleF right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return (int)((uint)X ^
                          (((uint)Y << 13) | ((uint)Y >> 19)) ^
                          (((uint)Width << 26) | ((uint)Width >> 6)) ^
                          (((uint)Height << 7) | ((uint)Height >> 25)));
        }

        public bool Contains(float x, float y)
        {
            return X <= x &&
                   x < X + Width &&
                   Y <= y &&
                   y < Y + Height;
        }

        public bool Contains(Vector2 value)
        {
            return Contains(value.X, value.Y);
        }

        public bool Contains(RectangleF rect)
        {
            return (X <= rect.X) &&
                   (rect.X + rect.Width <= X + Width) &&
                   (Y <= rect.Y) &&
                   (rect.Y + rect.Height <= Y + Height);
        }

        public void Inflate(float x, float y)
        {
            X -= x;
            Y -= y;
            Width += 2 * x;
            Height += 2 * y;
        }

        public void Inflate(Vector2 size)
        {
            Inflate(size.X, size.Y);
        }

        public static RectangleF Inflate(RectangleF rect, float x, float y)
        {
            var r = rect;
            r.Inflate(x, y);
            return r;
        }

        public void Intersect(RectangleF rect)
        {
            var result = Intersect(rect, this);

            X = result.X;
            Y = result.Y;
            Width = result.Width;
            Height = result.Height;
        }

        public static RectangleF Intersect(RectangleF a, RectangleF b)
        {
            var x1 = Math.Max(a.X, b.X);
            var x2 = Math.Min(a.X + a.Width, b.X + b.Width);
            var y1 = Math.Max(a.Y, b.Y);
            var y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

            if (x2 >= x1
                && y2 >= y1)
            {
                return new RectangleF(x1, y1, x2 - x1, y2 - y1);
            }
            return Empty;
        }

        public bool Intersects(RectangleF a, RectangleF b)
        {
            if (b.X < a.Right && a.X < b.Right && b.Y < a.Bottom)
                return a.Y < b.Bottom;
            return false;
        }

        public bool IntersectsWith(RectangleF rect)
        {
            return (rect.X < X + Width) &&
                   (X < rect.X + rect.Width) &&
                   (rect.Y < Y + Height) &&
                   (Y < rect.Y + rect.Height);
        }

        public void Offset(Vector2 pos)
        {
            Offset(pos.X, pos.Y);
        }

        public void Offset(float x, float y)
        {
            X += x;
            Y += y;
        }

        public static implicit operator RectangleF(Rectangle r)
        {
            return new RectangleF(r.X, r.Y, r.Width, r.Height);
        }

        public Vector2 Location
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public RectangleF(Vector2 location, Vector2 size)
        {
            X = location.X;
            Y = location.Y;
            Width = size.X;
            Height = size.Y;
        }

        public static RectangleF FromLTRB(float left, float top, float right, float bottom)
        {
            return new RectangleF(left,
                top,
                right - left,
                bottom - top);
        }

        public Vector2 Center()
        {
            return new Vector2(Width / 2, Height / 2);
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }

        public override string ToString()
        {
            return "{X=" + X.ToString(CultureInfo.CurrentCulture) + ",Y=" + Y.ToString(CultureInfo.CurrentCulture) +
                   ",Width=" + Width.ToString(CultureInfo.CurrentCulture) +
                   ",Height=" + Height.ToString(CultureInfo.CurrentCulture) + "}";
        }
    }
}