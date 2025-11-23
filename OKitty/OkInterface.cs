// Imports

using System.Collections.ObjectModel;
using static OKitty.OkInstance;
using static OKitty.OkMath;
using static OKitty.OkStyling;

namespace OKitty;

// OkInterface

public class OkInterface
{
    // Interfaces

    public interface IOInterface : IOInstance
    {
        // Enums

        [Flags]
        public enum OAutoSizeRule
        {
            None = 0,
            Horizontal = 1 << 0,
            Vertical = 1 << 1,
            Both = Horizontal | Vertical
        }
        
        // Properties

        public bool Active { get; set; }
        public bool Visible { get; set; }
        public OVector2<float> AbsoluteSize { get; }
        public OVector2<float> AbsolutePosition { get; }
        public OAutoSizeRule AutoSizeRule { get; set; }
        public OLayoutVector2<float, float> Size { get; set; }
        public OLayoutVector2<float, float> Position { get; set; }
        public float Rotation { get; set; }
        public int Layer { get; set; }
        public OColor BackgroundColor { get; set; }
        public float BackgroundOpacity { get; set; }
        public bool Clip { get; set; }
    }

    // Classes

    public static class OShapes
    {
        // Enums

        public enum OTriangleType
        {
            Acute,
            Equilateral,
            Isosceles,
            Obtuse,
            Right,
            Scalene
        }
        
        // Method and Functions

        public static OShapeInfo Triangle(OVector2<float> size, OVector3<float> position, float rotation, OTriangleType type, OColor color, OShapeInfo? mask = null)
        {
            (OVector3<float> p1, OVector3<float> p2, OVector3<float> p3) = (new OVector3<float>(), new OVector3<float>(), new OVector3<float>());

            switch (type)
            {
                case OTriangleType.Acute:
                    p1 = new OVector3<float>(0, size.Y);
                    p2 = new OVector3<float>(size.X, size.Y);
                    p3 = new OVector3<float>(size.X * 0.25f, 0);

                    break;
                case OTriangleType.Equilateral:
                    float side = size.X;
                    float height = side * (float)Math.Sqrt(3) / 2f;

                    p1 = new OVector3<float>(0, height);
                    p2 = new OVector3<float>(side, height);
                    p3 = new OVector3<float>(side / 2f, 0);

                    break;
                case OTriangleType.Isosceles:
                    p1 = new OVector3<float>(0, size.Y);
                    p2 = new OVector3<float>(size.X, size.Y);
                    p3 = new OVector3<float>(size.X / 2f, 0);

                    break;
                case OTriangleType.Obtuse:
                    p1 = new OVector3<float>(0, size.Y);
                    p2 = new OVector3<float>(size.X, size.Y);
                    p3 = new OVector3<float>(size.X * 0.2f, 0);

                    break;
                case OTriangleType.Right:
                    p2 = new OVector3<float>(size.X, 0);
                    p3 = new OVector3<float>(0, size.Y);

                    break;
                case OTriangleType.Scalene:
                    p1 = new OVector3<float>(0, size.Y);
                    p2 = new OVector3<float>(size.X, size.Y * 0.8f);
                    p3 = new OVector3<float>(size.X * 0.1f, 0);

                    break;
            }

            if (rotation != 0)
            {
                OVector2<float> center = size / 2;
                float cos = MathF.Cos(rotation * MathF.PI / 180.0f);
                float sin = MathF.Sin(rotation * MathF.PI / 180.0f);

                p1 = new OVector3<float>(
                    ((p1.X - center.X) * cos - (p1.Y - center.Y) * sin) + center.X,
                    ((p1.X - center.X) * sin + (p1.Y - center.Y) * cos) + center.Y
                );
                p2 = new OVector3<float>(
                    ((p2.X - center.X) * cos - (p2.Y - center.Y) * sin) + center.X,
                    ((p2.X - center.X) * sin + (p2.Y - center.Y) * cos) + center.Y
                );
                p3 = new OVector3<float>(
                    ((p3.X - center.X) * cos - (p3.Y - center.Y) * sin) + center.X,
                    ((p3.X - center.X) * sin + (p3.Y - center.Y) * cos) + center.Y
                );
            }

            p1 += position;
            p2 += position;
            p3 += position;
            
            List<OLineInfo> lines = new List<OLineInfo>()
            {
                new OLineInfo() { Start = p1, End = p2, Color = color },
                new OLineInfo() { Start = p2, End = p3, Color = color },
                new OLineInfo() { Start = p3, End = p1, Color = color }
            };
            OShapeInfo shape = new OShapeInfo()
            {
                Lines = lines,
                Mask = mask,
                Color = color
            };
            
            return shape;
        }

        public static OShapeInfo Rectangle(OVector2<float> size, OVector3<float> position, float rotation, OColor color, OShapeInfo? mask = null)
        {
            (OVector3<float> p1, OVector3<float> p2, OVector3<float> p3, OVector3<float> p4) = (
                new OVector3<float>(), new OVector3<float>(size.X, 0), new OVector3<float>(size.X, size.Y), new OVector3<float>(0, size.Y)
            );

            if (rotation != 0)
            {
                OVector2<float> center = size / 2;
                float radius = rotation * MathF.PI / 180.0f;
                float cos = MathF.Cos(radius);
                float sin = MathF.Sin(radius);

                p1 = new OVector3<float>(
                    ((p1.X - center.X) * cos - (p1.Y - center.Y) * sin) + center.X,
                    ((p1.X - center.X) * sin + (p1.Y - center.Y) * cos) + center.Y
                );
                p2 = new OVector3<float>(
                    ((p2.X - center.X) * cos - (p2.Y - center.Y) * sin) + center.X,
                    ((p2.X - center.X) * sin + (p2.Y - center.Y) * cos) + center.Y
                );
                p3 = new OVector3<float>(
                    ((p3.X - center.X) * cos - (p3.Y - center.Y) * sin) + center.X,
                    ((p3.X - center.X) * sin + (p3.Y - center.Y) * cos) + center.Y
                );
                p4 = new OVector3<float>(
                    ((p4.X - center.X) * cos - (p4.Y - center.Y) * sin) + center.X,
                    ((p4.X - center.X) * sin + (p4.Y - center.Y) * cos) + center.Y
                );
            }

            p1 += position;
            p2 += position;
            p3 += position;
            p4 += position;
            
            List<OLineInfo> lines = new List<OLineInfo>()
            {
                new OLineInfo() { Start = p1, End = p2, Color = color },
                new OLineInfo() { Start = p2, End = p3, Color = color },
                new OLineInfo() { Start = p3, End = p4, Color = color },
                new OLineInfo() { Start = p4, End = p1, Color = color }
            };
            OShapeInfo shape = new OShapeInfo()
            {
                Lines = lines,
                Mask = mask,
                Color = color
            };
            
            return shape;
        }

        public static OShapeInfo Square(float size, OVector3<float> position, float rotation, OColor color, OShapeInfo? mask = null)
        {
            return Rectangle(new OVector2<float>(size, size), position, rotation, color, mask);
        }
    }
}