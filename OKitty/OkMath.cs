// Imports

using System.Collections.ObjectModel;
using System.Numerics;
using static OKitty.OkStyling;

namespace OKitty;

// OkMath

public static class OkMath
{
    // Structs
    
    public struct OVector2<TNumber>
        where TNumber : INumber<TNumber>
    {
        // Static Properties

        public static readonly OVector2<TNumber> Zero = new OVector2<TNumber>();
        public static readonly OVector2<TNumber> PositiveOne = new OVector2<TNumber>(TNumber.One, TNumber.One);
        public static readonly OVector2<TNumber> NegativeOne = new OVector2<TNumber>(-TNumber.One, -TNumber.One);
        public static readonly OVector2<TNumber> Right = new OVector2<TNumber>(TNumber.One, TNumber.Zero);
        public static readonly OVector2<TNumber> Left = new OVector2<TNumber>(-TNumber.One, TNumber.Zero);
        public static readonly OVector2<TNumber> Up = new OVector2<TNumber>(TNumber.Zero, TNumber.One);
        public static readonly OVector2<TNumber> Down = new OVector2<TNumber>(TNumber.Zero, -TNumber.One);
        
        // Properties and Fields
        
        public TNumber X { get; }
        public TNumber Y { get; }
        public TNumber Magnitude { get => TNumber.CreateChecked(Math.Sqrt(double.CreateChecked(X * X) + double.CreateChecked(Y * Y))); }
        public TNumber MagnitudeSquared { get => TNumber.CreateChecked(X * X + Y * Y); }

        // Methods and Functions

        public OVector2(TNumber? x = default, TNumber? y = default)
        {
            X = x ?? TNumber.Zero;
            Y = y ?? TNumber.Zero;
        }

        public OVector2<TNumber> Normalize()
        {
            TNumber magnitude = Magnitude;

            return magnitude == TNumber.Zero ? this : this / magnitude;
        }
        
        public OVector2<TNumber> Rotate(double radians)
        {
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            double x = double.CreateChecked(X);
            double y = double.CreateChecked(Y);
            double rx = x * cos - y * sin;
            double ry = x * sin + y * cos;
            TNumber cx = TNumber.CreateChecked(rx);
            TNumber cy = TNumber.CreateChecked(ry);

            return new OVector2<TNumber>(cx, cy);
        }

        public OVector2<TNumber> Perpendicular()
        {
            return new OVector2<TNumber>(-Y, X);
        }

        public OVector2<TNumber> Absolute()
        {
            TNumber x = TNumber.Abs(X);
            TNumber y = TNumber.Abs(Y);

            return new OVector2<TNumber>(x, y);
        }

        public static OVector2<TNumber> Min(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            TNumber x = TNumber.Min(left.X, right.X);
            TNumber y = TNumber.Min(left.Y, right.Y);

            return new OVector2<TNumber>(x, y);
        }

        public static OVector2<TNumber> Max(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            TNumber x = TNumber.Max(left.X, right.X);
            TNumber y = TNumber.Max(left.Y, right.Y);

            return new OVector2<TNumber>(x, y);
        }

        public static TNumber Dot(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            return left.X * right.X + left.Y * right.Y;
        }

        public static TNumber Distance(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            return (left - right).Magnitude;
        }

        public static OVector2<TNumber> Lerp(OVector2<TNumber> left, OVector2<TNumber> right, TNumber tau)
        {
            return left + (right - left) * tau;
        }

        public static OVector2<TNumber> Project(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            TNumber scale = OVector2<TNumber>.Dot(left, right) / OVector2<TNumber>.Dot(right, right);

            return right * scale;
        }

        public static OVector2<TNumber> Reflect(OVector2<TNumber> vector, OVector2<TNumber> normal)
        {
            return vector - normal * (OVector2<TNumber>.Dot(vector, normal) * TNumber.CreateChecked(2));
        }
        
        // To String

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
        
        // Equals and Hashing
        
        public override bool Equals(object? obj)
        {
            if (obj is OVector2<TNumber> other)
                return this == other;
            
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
        
        // Binary Operators

        public static OVector2<TNumber> operator +(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            TNumber x = left.X + right.X;
            TNumber y = left.Y + right.Y;

            return new OVector2<TNumber>(x, y);
        }

        public static OVector2<TNumber> operator +(OVector2<TNumber> left, TNumber right)
        {
            TNumber x = left.X + right;
            TNumber y = left.Y + right;

            return new OVector2<TNumber>(x, y);
        }

        public static OVector2<TNumber> operator -(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            TNumber x = left.X - right.X;
            TNumber y = left.Y - right.Y;

            return new OVector2<TNumber>(x, y);
        }

        public static OVector2<TNumber> operator -(OVector2<TNumber> left, TNumber right)
        {
            TNumber x = left.X - right;
            TNumber y = left.Y - right;

            return new OVector2<TNumber>(x, y);
        }

        public static OVector2<TNumber> operator *(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            TNumber x = left.X * right.X;
            TNumber y = left.Y * right.Y;

            return new OVector2<TNumber>(x, y);
        }

        public static OVector2<TNumber> operator *(OVector2<TNumber> left, TNumber right)
        {
            TNumber x = left.X * right;
            TNumber y = left.Y * right;

            return new OVector2<TNumber>(x, y);
        }
        
        public static OVector2<TNumber> operator /(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            TNumber x = left.X / right.X;
            TNumber y = left.Y / right.Y;

            return new OVector2<TNumber>(x, y);
        }
        
        public static OVector2<TNumber> operator /(OVector2<TNumber> left, TNumber right)
        {
            TNumber x = left.X / right;
            TNumber y = left.Y / right;

            return new OVector2<TNumber>(x, y);
        }

        public static OVector2<TNumber> operator %(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            TNumber x = left.X % right.X;
            TNumber y = left.Y % right.Y;

            return new OVector2<TNumber>(x, y);
        }

        public static OVector2<TNumber> operator %(OVector2<TNumber> left, TNumber right)
        {
            TNumber x = left.X % right;
            TNumber y = left.Y % right;

            return new OVector2<TNumber>(x, y);
        }
        
        public static bool operator ==(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            bool x = left.X == right.X;
            bool y = left.Y == right.Y;

            return x && y;
        }

        public static bool operator !=(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            return !(left == right);
        }

        public static bool operator >(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            bool x = left.X > right.X;
            bool y = left.Y > right.Y;

            return x && y;
        }

        public static bool operator >=(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            return left > right || left == right;
        }

        public static bool operator <(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            bool x = left.X < right.X;
            bool y = left.Y < right.Y;

            return x && y;
        }

        public static bool operator <=(OVector2<TNumber> left, OVector2<TNumber> right)
        {
            return left < right || left == right;
        }
        
        // Unary Operators

        public static OVector2<TNumber> operator +(OVector2<TNumber> vector)
        {
            TNumber x = +vector.X;
            TNumber y = +vector.Y;

            return new OVector2<TNumber>(x, y);
        }

        public static OVector2<TNumber> operator -(OVector2<TNumber> vector)
        {
            TNumber x = -vector.X;
            TNumber y = -vector.Y;

            return new OVector2<TNumber>(x, y);
        }
    }
    
    public struct OVector3<TNumber>
        where TNumber : INumber<TNumber>
    {
        // Static Properties

        public static readonly OVector3<TNumber> Zero = new OVector3<TNumber>();
        public static readonly OVector3<TNumber> PositiveOne = new OVector3<TNumber>(TNumber.One, TNumber.One, TNumber.One);
        public static readonly OVector3<TNumber> NegativeOne = new OVector3<TNumber>(-TNumber.One, -TNumber.One, -TNumber.One);
        public static readonly OVector3<TNumber> Right = new OVector3<TNumber>(TNumber.One, TNumber.Zero, TNumber.Zero);
        public static readonly OVector3<TNumber> Left = new OVector3<TNumber>(-TNumber.One, TNumber.Zero, TNumber.Zero);
        public static readonly OVector3<TNumber> Up = new OVector3<TNumber>(TNumber.Zero, TNumber.One, TNumber.Zero);
        public static readonly OVector3<TNumber> Down = new OVector3<TNumber>(TNumber.Zero, -TNumber.One, TNumber.Zero);
        public static readonly OVector3<TNumber> Forward = new OVector3<TNumber>(TNumber.Zero, TNumber.Zero, TNumber.One);
        public static readonly OVector3<TNumber> Backward = new OVector3<TNumber>(TNumber.Zero, TNumber.Zero, -TNumber.One);
        
        // Properties and Fields
        
        public TNumber X { get; }
        public TNumber Y { get; }
        public TNumber Z { get; }
        public TNumber Magnitude { get => TNumber.CreateChecked(Math.Sqrt(double.CreateChecked(X * X) + double.CreateChecked(Y * Y) + double.CreateChecked(Z * Z))); }
        public TNumber MagnitudeSquared { get => TNumber.CreateChecked(X * X + Y * Y); }

        // Methods and Functions

        public OVector3(TNumber? x = default, TNumber? y = default, TNumber? z = default)
        {
            X = x ?? TNumber.Zero;
            Y = y ?? TNumber.Zero;
            Z = z ?? TNumber.Zero;
        }

        public OVector3<TNumber> Normalize()
        {
            TNumber magnitude = Magnitude;

            return magnitude == TNumber.Zero ? this : this / magnitude;
        }

        public OVector3<TNumber> Absolute()
        {
            TNumber x = TNumber.Abs(X);
            TNumber y = TNumber.Abs(Y);
            TNumber z = TNumber.Abs(Z);

            return new OVector3<TNumber>(x, y, z);
        }

        public static OVector3<TNumber> Min(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            TNumber x = TNumber.Min(left.X, right.X);
            TNumber y = TNumber.Min(left.Y, right.Y);
            TNumber z = TNumber.Min(left.Z, right.Z);

            return new OVector3<TNumber>(x, y, z);
        }

        public static OVector3<TNumber> Max(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            TNumber x = TNumber.Max(left.X, right.X);
            TNumber y = TNumber.Max(left.Y, right.Y);
            TNumber z = TNumber.Max(left.Z, right.Z);

            return new OVector3<TNumber>(x, y, z);
        }

        public static TNumber Dot(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public static TNumber Distance(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            return (left - right).Magnitude;
        }

        public static OVector3<TNumber> Lerp(OVector3<TNumber> left, OVector3<TNumber> right, TNumber tau)
        {
            return left + (right - left) * tau;
        }

        public static OVector3<TNumber> Project(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            TNumber scale = OVector3<TNumber>.Dot(left, right) / OVector3<TNumber>.Dot(right, right);

            return right * scale;
        }

        public static OVector3<TNumber> Reflect(OVector3<TNumber> vector, OVector3<TNumber> normal)
        {
            return vector - normal * (OVector3<TNumber>.Dot(vector, normal) * TNumber.CreateChecked(2));
        }
        
        // To String

        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }
        
        // Equals and Hashing
        
        public override bool Equals(object? obj)
        {
            if (obj is OVector3<TNumber> other)
                return this == other;
            
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
        
        // Binary Operators

        public static OVector3<TNumber> operator +(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            TNumber x = left.X + right.X;
            TNumber y = left.Y + right.Y;
            TNumber z = left.Z + right.Z;

            return new OVector3<TNumber>(x, y, z);
        }

        public static OVector3<TNumber> operator +(OVector3<TNumber> left, TNumber right)
        {
            TNumber x = left.X + right;
            TNumber y = left.Y + right;
            TNumber z = left.Z + right;

            return new OVector3<TNumber>(x, y, z);
        }

        public static OVector3<TNumber> operator -(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            TNumber x = left.X - right.X;
            TNumber y = left.Y - right.Y;
            TNumber z = left.Z - right.Z;

            return new OVector3<TNumber>(x, y, z);
        }

        public static OVector3<TNumber> operator -(OVector3<TNumber> left, TNumber right)
        {
            TNumber x = left.X - right;
            TNumber y = left.Y - right;
            TNumber z = left.Z - right;

            return new OVector3<TNumber>(x, y, z);
        }

        public static OVector3<TNumber> operator *(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            TNumber x = left.X * right.X;
            TNumber y = left.Y * right.Y;
            TNumber z = left.Z * right.Z;

            return new OVector3<TNumber>(x, y, z);
        }

        public static OVector3<TNumber> operator *(OVector3<TNumber> left, TNumber right)
        {
            TNumber x = left.X * right;
            TNumber y = left.Y * right;
            TNumber z = left.Z * right;

            return new OVector3<TNumber>(x, y, z);
        }
        
        public static OVector3<TNumber> operator /(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            TNumber x = left.X / right.X;
            TNumber y = left.Y / right.Y;
            TNumber z = left.Z / right.Z;

            return new OVector3<TNumber>(x, y, z);
        }
        
        public static OVector3<TNumber> operator /(OVector3<TNumber> left, TNumber right)
        {
            TNumber x = left.X / right;
            TNumber y = left.Y / right;
            TNumber z = left.Z / right;

            return new OVector3<TNumber>(x, y, z);
        }

        public static OVector3<TNumber> operator %(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            TNumber x = left.X % right.X;
            TNumber y = left.Y % right.Y;
            TNumber z = left.Z % right.Z;

            return new OVector3<TNumber>(x, y, z);
        }

        public static OVector3<TNumber> operator %(OVector3<TNumber> left, TNumber right)
        {
            TNumber x = left.X % right;
            TNumber y = left.Y % right;
            TNumber z = left.Z % right;

            return new OVector3<TNumber>(x, y, z);
        }
        
        public static bool operator ==(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            bool x = left.X == right.X;
            bool y = left.Y == right.Y;
            bool z = left.Z == right.Z;

            return x && y && z;
        }
        
        public static bool operator !=(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            return !(left == right);
        }

        public static bool operator >(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            bool x = left.X > right.X;
            bool y = left.Y > right.Y;
            bool z = left.Z > right.Z;

            return x && y && z;
        }

        public static bool operator >=(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            return left > right || left == right;
        }

        public static bool operator <(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            bool x = left.X < right.X;
            bool y = left.Y < right.Y;
            bool z = left.Z < right.Z;

            return x && y && z;
        }

        public static bool operator <=(OVector3<TNumber> left, OVector3<TNumber> right)
        {
            return left < right || left == right;
        }
        
        // Unary Operators

        public static OVector3<TNumber> operator +(OVector3<TNumber> vector)
        {
            TNumber x = +vector.X;
            TNumber y = +vector.Y;
            TNumber z = +vector.Z;

            return new OVector3<TNumber>(x, y, z);
        }

        public static OVector3<TNumber> operator -(OVector3<TNumber> vector)
        {
            TNumber x = -vector.X;
            TNumber y = -vector.Y;
            TNumber z = -vector.Z;

            return new OVector3<TNumber>(x, y, z);
        }
    }
    
    public struct OLayoutVector<TScale, TOffset>
        where TScale : IBinaryFloatingPointIeee754<TScale>
        where TOffset : INumber<TOffset>
    {
        // Static Properties
        
        public static readonly OLayoutVector<TScale, TOffset> Zero = new OLayoutVector<TScale, TOffset>(TScale.Zero);
        public static readonly OLayoutVector<TScale, TOffset> Eight = new OLayoutVector<TScale, TOffset>(TScale.CreateChecked(0.125));
        public static readonly OLayoutVector<TScale, TOffset> Quarter = new OLayoutVector<TScale, TOffset>(TScale.CreateChecked(0.25));
        public static readonly OLayoutVector<TScale, TOffset> Third = new OLayoutVector<TScale, TOffset>(TScale.CreateChecked(0.333333333));
        public static readonly OLayoutVector<TScale, TOffset> Half = new OLayoutVector<TScale, TOffset>(TScale.CreateChecked(0.5));
        public static readonly OLayoutVector<TScale, TOffset> ThreeQuarter = new OLayoutVector<TScale, TOffset>(TScale.CreateChecked(0.75));
        public static readonly OLayoutVector<TScale, TOffset> One = new OLayoutVector<TScale, TOffset>(TScale.One);
        
        // Properties and Fields

        public TScale Scale { get; }
        public TOffset Offset { get; }
        
        // Methods and Functions

        OLayoutVector(TScale? scale = default, TOffset? offset = default)
        {
            Scale = scale ?? TScale.Zero;
            Offset = offset ?? TOffset.Zero;
        }

        public TOffset Resolve(TOffset size)
        {
            return TOffset.CreateChecked(Scale * TScale.CreateChecked(size)) + Offset;
        }

        public OLayoutVector<TScale, TOffset> Absolute()
        {
            TScale scale = TScale.Abs(Scale);
            TOffset offset = TOffset.Abs(Offset);

            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector<TScale, TOffset> Min(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            TScale scale = TScale.Min(left.Scale, right.Scale);
            TOffset offset = TOffset.Min(left.Offset, right.Offset);

            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector<TScale, TOffset> Max(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            TScale scale = TScale.Max(left.Scale, right.Scale);
            TOffset offset = TOffset.Max(left.Offset, right.Offset);

            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }
        
        public static OLayoutVector<TScale, TOffset> Lerp(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right, TScale tau)
        {
            TScale scale = left.Scale + (right.Scale - left.Scale) * tau;
            TOffset offset = left.Offset + (right.Offset - left.Offset) * TOffset.CreateChecked(tau);
            
            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }
        
        // To String

        public override string ToString()
        {
            return $"{Scale * TScale.CreateChecked(100.0)}% +{Offset}px";
        }
        
        // Equals and Hashing
        
        public override bool Equals(object? obj)
        {
            if (obj is OLayoutVector<TScale, TOffset> other)
                return this == other;
            
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Scale, Offset);
        }
        
        // Binary Operators

        public static OLayoutVector<TScale, TOffset> operator +(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            TScale scale = left.Scale + right.Scale;
            TOffset offset = left.Offset + right.Offset;

            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector<TScale, TOffset> operator -(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            TScale scale = left.Scale - right.Scale;
            TOffset offset = left.Offset - right.Offset;

            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector<TScale, TOffset> operator *(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            TScale scale = left.Scale * right.Scale;
            TOffset offset = left.Offset * right.Offset;

            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector<TScale, TOffset> operator /(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            TScale scale = left.Scale / right.Scale;
            TOffset offset = left.Offset / right.Offset;

            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector<TScale, TOffset> operator %(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            TScale scale = left.Scale % right.Scale;
            TOffset offset = left.Offset % right.Offset;

            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }

        public static bool operator ==(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            bool scale = left.Scale == right.Scale;
            bool offset = left.Offset == right.Offset;

            return scale && offset;
        }

        public static bool operator !=(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            return !(left == right);
        }

        public static bool operator >(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            bool scale = left.Scale > right.Scale;
            bool offset = left.Offset > right.Offset;

            return scale && offset;
        }

        public static bool operator >=(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            return left > right || left == right;
        }

        public static bool operator <(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            bool scale = left.Scale < right.Scale;
            bool offset = left.Offset < right.Offset;

            return scale && offset;
        }

        public static bool operator <=(OLayoutVector<TScale, TOffset> left, OLayoutVector<TScale, TOffset> right)
        {
            return left < right || left == right;
        }
        
        // Unary Operators

        public static OLayoutVector<TScale, TOffset> operator +(OLayoutVector<TScale, TOffset> vector)
        {
            TScale scale = +vector.Scale;
            TOffset offset = +vector.Offset;

            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector<TScale, TOffset> operator -(OLayoutVector<TScale, TOffset> vector)
        {
            TScale scale = -vector.Scale;
            TOffset offset = -vector.Offset;

            return new OLayoutVector<TScale, TOffset>(scale, offset);
        }
    }
    
    public struct OLayoutVector2<TScale, TOffset>
        where TScale : IBinaryFloatingPointIeee754<TScale>
        where TOffset : INumber<TOffset>
    {
        // Static Properties

        public static readonly OLayoutVector2<TScale, TOffset> Zero = new OLayoutVector2<TScale, TOffset>();
        public static readonly OLayoutVector2<TScale, TOffset> Eight = new OLayoutVector2<TScale, TOffset>(TScale.CreateChecked(0.125), TScale.CreateChecked(0.125));
        public static readonly OLayoutVector2<TScale, TOffset> Quarter = new OLayoutVector2<TScale, TOffset>(TScale.CreateChecked(0.25), TScale.CreateChecked(0.25));
        public static readonly OLayoutVector2<TScale, TOffset> Third = new OLayoutVector2<TScale, TOffset>(TScale.CreateChecked(0.333333333), TScale.CreateChecked(0.333333333));
        public static readonly OLayoutVector2<TScale, TOffset> Half = new OLayoutVector2<TScale, TOffset>(TScale.CreateChecked(0.5), TScale.CreateChecked(0.5));
        public static readonly OLayoutVector2<TScale, TOffset> ThreeQuarter = new OLayoutVector2<TScale, TOffset>(TScale.CreateChecked(0.75), TScale.CreateChecked(0.75));
        public static readonly OLayoutVector2<TScale, TOffset> One = new OLayoutVector2<TScale, TOffset>(OVector2<TScale>.PositiveOne);

        // Properties and Fields

        public OVector2<TScale> Scale { get; }
        public OVector2<TOffset> Offset { get; }

        // Methods and Functions

        public OLayoutVector2(OVector2<TScale>? scale = default, OVector2<TOffset>? offset = default)
        {
            Scale = scale ?? OVector2<TScale>.Zero;
            Offset = offset ?? OVector2<TOffset>.Zero;
        }

        public OLayoutVector2(TScale? x = default, TScale? y = default)
        {
            Scale = new OVector2<TScale>(x, y);
            Offset = OVector2<TOffset>.Zero;
        }

        public OLayoutVector2(TOffset? x = default, TOffset? y = default)
        {
            Scale = OVector2<TScale>.Zero;
            Offset = new OVector2<TOffset>(x, y);
        }

        public OVector2<TOffset> Resolve(OVector2<TOffset> size)
        {
            TOffset x = TOffset.CreateChecked(Scale.X * TScale.CreateChecked(size.X)) + Offset.X;
            TOffset y = TOffset.CreateChecked(Scale.Y * TScale.CreateChecked(size.Y)) + Offset.Y;
            
            return new OVector2<TOffset>(x, y);
        }

        public OLayoutVector2<TScale, TOffset> Absolute()
        {
            OVector2<TScale> scale = new OVector2<TScale>(TScale.Abs(Scale.X), TScale.Abs(Scale.Y));
            OVector2<TOffset> offset = new OVector2<TOffset>(TOffset.Abs(Offset.X), TOffset.Abs(Offset.Y));

            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector2<TScale, TOffset> Min(OLayoutVector2<TScale, TOffset> left, OLayoutVector2<TScale, TOffset> right)
        {
            OVector2<TScale> scale = new OVector2<TScale>(TScale.Min(left.Scale.X, right.Scale.X), TScale.Min(left.Scale.Y, right.Scale.Y));
            OVector2<TOffset> offset = new OVector2<TOffset>(TOffset.Min(left.Offset.X, right.Offset.X), TOffset.Min(left.Offset.Y, right.Offset.Y));

            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector2<TScale, TOffset> Max(OLayoutVector2<TScale, TOffset> left, OLayoutVector2<TScale, TOffset> right)
        {
            OVector2<TScale> scale = new OVector2<TScale>(TScale.Max(left.Scale.X, right.Scale.X), TScale.Max(left.Scale.Y, right.Scale.Y));
            OVector2<TOffset> offset = new OVector2<TOffset>(TOffset.Max(left.Offset.X, right.Offset.X), TOffset.Max(left.Offset.Y, right.Offset.Y));

            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector2<TScale, TOffset> Lerp(OLayoutVector2<TScale, TOffset> left, OLayoutVector2<TScale, TOffset> right, TScale tau)
        {
            OVector2<TScale> scale = left.Scale + (right.Scale - left.Scale) * tau;
            OVector2<TOffset> offset = left.Offset + (right.Offset - left.Offset) * TOffset.CreateChecked(tau);

            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }

        // To String

        public override string ToString()
        {
            return $"{Scale.X * TScale.CreateChecked(100.0)}% +{Offset.X}px, {Scale.Y * TScale.CreateChecked(100.0)}% +{Offset.Y}px";
        }

        // Equals and Hashing

        public override bool Equals(object? obj)
        {
            if (obj is OLayoutVector2<TScale, TOffset> other)
                return this == other;

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Scale, Offset);
        }

        // Binary Operators
        
        public static OLayoutVector2<TScale, TOffset> operator +(OLayoutVector2<TScale, TOffset> left, OLayoutVector2<TScale, TOffset> right)
        {
            OVector2<TScale> scale = left.Scale + right.Scale;
            OVector2<TOffset> offset = left.Offset + right.Offset;

            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector2<TScale, TOffset> operator -(OLayoutVector2<TScale, TOffset> left, OLayoutVector2<TScale, TOffset> right)
        {
            OVector2<TScale> scale = left.Scale - right.Scale;
            OVector2<TOffset> offset = left.Offset - right.Offset;

            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector2<TScale, TOffset> operator *(OLayoutVector2<TScale, TOffset> left, OLayoutVector2<TScale, TOffset> right)
        {
            OVector2<TScale> scale = left.Scale * right.Scale;
            OVector2<TOffset> offset = left.Offset * right.Offset;

            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector2<TScale, TOffset> operator /(OLayoutVector2<TScale, TOffset> left, OLayoutVector2<TScale, TOffset> right)
        {
            OVector2<TScale> scale = left.Scale / right.Scale;
            OVector2<TOffset> offset = left.Offset / right.Offset;

            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector2<TScale, TOffset> operator %(OLayoutVector2<TScale, TOffset> left, OLayoutVector2<TScale, TOffset> right)
        {
            OVector2<TScale> scale = left.Scale % right.Scale;
            OVector2<TOffset> offset = left.Offset % right.Offset;

            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }

        public static bool operator ==(OLayoutVector2<TScale, TOffset> left, OLayoutVector2<TScale, TOffset> right)
        {
            bool scale = left.Scale == right.Scale;
            bool offset = left.Offset == right.Offset;

            return scale && offset;
        }

        public static bool operator !=(OLayoutVector2<TScale, TOffset> left, OLayoutVector2<TScale, TOffset> right)
        {
            return !(left == right);
        }

        // Unary Operators

        public static OLayoutVector2<TScale, TOffset> operator +(OLayoutVector2<TScale, TOffset> vector)
        {
            OVector2<TScale> scale = +vector.Scale;
            OVector2<TOffset> offset = +vector.Offset;
            
            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }

        public static OLayoutVector2<TScale, TOffset> operator -(OLayoutVector2<TScale, TOffset> vector)
        {
            OVector2<TScale> scale = -vector.Scale;
            OVector2<TOffset> offset = -vector.Offset;
            
            return new OLayoutVector2<TScale, TOffset>(scale, offset);
        }
    }

    public struct ONumberSequence<TValue>
        where TValue : INumber<TValue>
    {
        // Static Properties

        public static readonly ONumberSequence<TValue> FadeIn = new ONumberSequence<TValue>(TValue.Zero, TValue.One, (0, TValue.Zero), (1, TValue.One));
        public static readonly ONumberSequence<TValue> FadeOut = new ONumberSequence<TValue>(TValue.Zero, TValue.One, (0, TValue.One), (1, TValue.Zero));
        
        // Properties and Fields
        
        public ReadOnlySet<(float Time, TValue Value)> Sequence { get; }
        public TValue Min { get; }
        public TValue Max { get; }
        
        // Methods and Functions

        public ONumberSequence(TValue? min = default, TValue? max = default, params (float Time, TValue Value)[] keypoints)
        {
            Min = min ?? TValue.Zero;
            Max = max ?? TValue.One;
            
            if (Min > Max)
                (Min, Max) = (Max, Min);

            Dictionary<float, TValue> timeToValue = new Dictionary<float, TValue>();

            foreach ((float time, TValue value) in keypoints)
            {
                float clampedTime = Math.Clamp(time, 0f, 1f);
                TValue clampedValue = TValue.Clamp(value, Min, Max);
                
                timeToValue[clampedTime] = clampedValue;
            }
            
            HashSet<(float Time, TValue Value)> sorted = timeToValue
                .OrderBy((KeyValuePair<float, TValue> pair) => pair.Key)
                .Select((KeyValuePair<float, TValue> pair) => (pair.Key, pair.Value))
                .ToHashSet();

            Sequence = new ReadOnlySet<(float Time, TValue Value)>(sorted);
        }

        public TValue GetValue(float time)
        {
            if (Sequence.Count == 0)
                return TValue.Zero;
            
            time = Math.Clamp(time, 0f, 1f);

            List<(float Time, TValue Value)> sorted = Sequence.OrderBy(((float Time, TValue Value) keypoint) => keypoint.Time).ToList();

            if (time <= sorted.First().Time)
                return sorted.First().Value;

            if (time >= sorted.Last().Time)
                return sorted.Last().Value;

            (float Time, TValue Value) a = sorted.First();
            (float Time, TValue Value) b = sorted.Last();

            for (int i = 0; i < sorted.Count - 1; i++)
            {
                if (time >= sorted[i].Time && time <= sorted[i + 1].Time)
                {
                    a = sorted[i];
                    b = sorted[i + 1];
                    
                    break;
                }
            }

            if (b.Time == a.Time)
                return a.Value;

            float t = (time - a.Time) / (b.Time - a.Time);
            double av = double.CreateChecked(a.Value);
            double bv = double.CreateChecked(b.Value);
            double result = av + (bv - av) * t;

            return TValue.CreateChecked(result);
        }
        
        // To String

        public override string ToString()
        {
            return $"Number sequence {GetValue(0)} - {GetValue(1)}";
        }
        
        // Equals and Hashing
        
        public override bool Equals(object? obj)
        {
            if (obj is ONumberSequence<TValue> other)
                return this == other;

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max, Sequence.Count);
        }
        
        // Binary Operators
        
        public static ONumberSequence<TValue> operator *(ONumberSequence<TValue> sequence, TValue scale)
        {
            (float Time, TValue Value)[] scaled = sequence.Sequence
                .Select(((float Time, TValue Value) keypoint) => (keypoint.Time, keypoint.Value * scale))
                .ToArray();

            return new ONumberSequence<TValue>(sequence.Min, sequence.Max, scaled);
        }

        public static ONumberSequence<TValue> operator /(ONumberSequence<TValue> sequence, TValue divisor)
        {
            (float Time, TValue Value)[] scaled = sequence.Sequence
                .Select(((float Time, TValue Value) keypoint) => (keypoint.Time, keypoint.Value / divisor))
                .ToArray();

            return new ONumberSequence<TValue>(sequence.Min, sequence.Max, scaled);
        }
        
        public static bool operator ==(ONumberSequence<TValue> left, ONumberSequence<TValue> right)
        {
            return left.Min == right.Min &&
                   left.Max == right.Max &&
                   left.Sequence.SequenceEqual(right.Sequence);
        }

        public static bool operator !=(ONumberSequence<TValue> left, ONumberSequence<TValue> right)
        {
            return !(left == right);
        }
    }

    public struct OColorSequence
    {
        // Static Properties

        public static readonly OColorSequence Cool = new OColorSequence(
            (0f, new OColor(OColor.OColorShades.Blue100)),
            (0.5f, new OColor(OColor.OColorShades.Blue500)),
            (1f, new OColor(OColor.OColorShades.Blue900))
        );

        public static readonly OColorSequence Hot = new OColorSequence(
            (0f, new OColor(OColor.OColorShades.Red100)),
            (0.5f, new OColor(OColor.OColorShades.Red500)),
            (1f, new OColor(OColor.OColorShades.Red900))
        );

        public static readonly OColorSequence Grayscale = new OColorSequence(
            (0f, OColor.Black),
            (1f, OColor.White)
        );

        public static readonly OColorSequence Nebula = new OColorSequence(
            (0f, new OColor(OColor.OColorShades.Blue300)),
            (0.5f, new OColor(OColor.OColorShades.Purple500)),
            (1f, new OColor(OColor.OColorShades.Red500))
        );

        public static readonly OColorSequence Spring = new OColorSequence(
            (0f, new OColor(OColor.OColors.Fuchsia)),
            (1f, new OColor(OColor.OColors.Yellow))
        );

        public static readonly OColorSequence Summer = new OColorSequence(
            (0f, new OColor(OColor.OColors.Green)),
            (1f, new OColor(OColor.OColors.LightYellow))
        );

        public static readonly OColorSequence Autumn = new OColorSequence(
            (0f, new OColor(OColor.OColors.OrangeRed)),
            (1f, new OColor(OColor.OColors.Yellow))
        );

        public static readonly OColorSequence Winter = new OColorSequence(
            (0f, new OColor(OColor.OColors.MediumBlue)),
            (1f, new OColor(OColor.OColors.SpringGreen))
        );

        public static readonly OColorSequence Sky = new OColorSequence(
            (0f, new OColor(OColor.OColors.LightSkyBlue)),
            (0.5f, new OColor(OColor.OColors.SkyBlue)),
            (1f, new OColor(OColor.OColors.DeepSkyBlue))
        );

        public static readonly OColorSequence Rainbow = new OColorSequence(
            (0f, new OColor(OColor.OColors.Red)),
            (0.17f, new OColor(OColor.OColors.Yellow)),
            (0.33f, new OColor(OColor.OColors.Lime)),
            (0.5f, new OColor(OColor.OColors.Aqua)),
            (0.67f, new OColor(OColor.OColors.Blue)),
            (0.83f, new OColor(OColor.OColors.Fuchsia)),
            (1f, new OColor(OColor.OColors.Red))
        );

        public static readonly OColorSequence Fire = new OColorSequence(
            (0f, new OColor(OColor.OColors.DarkRed)),
            (0.4f, new OColor(OColor.OColors.OrangeRed)),
            (0.7f, new OColor(OColor.OColors.Yellow)),
            (1f, new OColor(OColor.OColors.White))
        );

        public static readonly OColorSequence Water = new OColorSequence(
            (0f, new OColor(OColor.OColors.MidnightBlue)),
            (0.5f, new OColor(OColor.OColors.DeepSkyBlue)),
            (1f, new OColor(OColor.OColors.Aqua))
        );

        public static readonly OColorSequence Plasma = new OColorSequence(
            (0f, new OColor(OColor.OColors.DarkBlue)),
            (0.25f, new OColor(OColor.OColors.MediumPurple)),
            (0.5f, new OColor(OColor.OColors.OrangeRed)),
            (1f, new OColor(OColor.OColors.Yellow))
        );
        
        // Properties and Fields
        
        public ReadOnlySet<(float Time, OColor Color)> Sequence { get; }
        
        // Methods

        public OColorSequence(params (float Time, OColor Color)[] keypoints)
        {
            Dictionary<float, OColor> timeToValue = new Dictionary<float, OColor>();

            foreach ((float time, OColor color) in keypoints)
            {
                float clampedTime = Math.Clamp(time, 0f, 1f);

                timeToValue[clampedTime] = color;
            }
            
            HashSet<(float Time, OColor Color)> sorted = timeToValue
                .OrderBy((KeyValuePair<float, OColor> pair) => pair.Key)
                .Select((KeyValuePair<float, OColor> pair) => (pair.Key, pair.Value))
                .ToHashSet();

            Sequence = new ReadOnlySet<(float Time, OColor Color)>(sorted);
        }

        public OColor GetValue(float time)
        {
            if (Sequence.Count == 0)
                return OColor.Transparent;

            time = Math.Clamp(time, 0f, 1f);
            
            if (Sequence.Count == 1)
                return Sequence.First().Color;

            (float Time, OColor Color)[] sorted = Sequence.OrderBy(((float Time, OColor Color) keypoint) => keypoint.Time).ToArray();

            if (time <= sorted[0].Time)
                return sorted[0].Color;
            
            if (time >= sorted[^1].Time)
                return sorted[^1].Color;

            for (int i = 0; i < sorted.Length - 1; i++)
            {
                (float t0, OColor c0) = sorted[i];
                (float t1, OColor c1) = sorted[i + 1];

                if (time >= t0 && time <= t1)
                {
                    float alpha = (time - t0) / (t1 - t0);
                    byte a = (byte)(c0.Argb.Alpha + (c1.Argb.Alpha - c0.Argb.Alpha) * alpha);
                    byte r = (byte)(c0.Argb.Red   + (c1.Argb.Red   - c0.Argb.Red)   * alpha);
                    byte g = (byte)(c0.Argb.Green + (c1.Argb.Green - c0.Argb.Green) * alpha);
                    byte b = (byte)(c0.Argb.Blue  + (c1.Argb.Blue  - c0.Argb.Blue)  * alpha);

                    return OColor.FromArgb(a, r, g, b);
                }
            }

            return sorted[^1].Color;
        }
        
        // To String

        public override string ToString()
        {
            return $"Color sequence {GetValue(0)} - {GetValue(1)}";
        }
        
        // Equals and Hashing
        
        public override bool Equals(object? obj)
        {
            if (obj is OColorSequence other)
                return this == other;

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Sequence.Count);
        }
        
        // Binary Operators

        public static bool operator ==(OColorSequence left, OColorSequence right)
        {
            return left.Sequence.SequenceEqual(right.Sequence);
        }

        public static bool operator !=(OColorSequence left, OColorSequence right)
        {
            return !(left == right);
        }
    }
}