// Modified from Microsoft.Xna.Framework.Vector2
// Type: Microsoft.Xna.Framework.Vector2I
// Changes: float to int throughout, removed methods that don't make sense with integers (like Normalize)

#nullable disable
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework
{
  /// <summary>Describes a 2D-vector using integers.</summary>
  //[TypeConverter(typeof (Vector2ITypeConverter))] // You'll need a custom TypeConverter if you need this.
  [DataContract]
  [DebuggerDisplay("{DebugDisplayString,nq}")]
  public struct Vector2I : IEquatable<Vector2I>
  {
    private static readonly Vector2I zeroVector = new Vector2I(0, 0);
    private static readonly Vector2I unitVector = new Vector2I(1, 1);
    private static readonly Vector2I unitXVector = new Vector2I(1, 0);
    private static readonly Vector2I unitYVector = new Vector2I(0, 1);

    /// <summary>
    /// The x coordinate of this <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    [DataMember] public int X;

    /// <summary>
    /// The y coordinate of this <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    [DataMember] public int Y;

    /// <summary>
    /// Returns a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> with components 0, 0.
    /// </summary>
    public static Vector2I Zero => Vector2I.zeroVector;

    /// <summary>
    /// Returns a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> with components 1, 1.
    /// </summary>
    public static Vector2I One => Vector2I.unitVector;

    /// <summary>
    /// Returns a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> with components 1, 0.
    /// </summary>
    public static Vector2I UnitX => Vector2I.unitXVector;

    /// <summary>
    /// Returns a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> with components 0, 1.
    /// </summary>
    public static Vector2I UnitY => Vector2I.unitYVector;

    internal string DebugDisplayString => this.X.ToString() + "  " + this.Y.ToString();

    /// <summary>Constructs a 2d vector with X and Y from two values.</summary>
    /// <param name="x">The x coordinate in 2d-space.</param>
    /// <param name="y">The y coordinate in 2d-space.</param>
    public Vector2I(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    /// <summary>
    /// Constructs a 2d vector with X and Y set to the same value.
    /// </summary>
    /// <param name="value">The x and y coordinates in 2d-space.</param>
    public Vector2I(int value)
    {
      this.X = value;
      this.Y = value;
    }

    /// <summary>
    /// Inverts values in the specified <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    /// <param name="value">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the right of the sub sign.</param>
    /// <returns>Result of the inversion.</returns>
    public static Vector2I operator -(Vector2I value)
    {
      value.X = -value.X;
      value.Y = -value.Y;
      return value;
    }

    /// <summary>Adds two vectors.</summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the left of the add sign.</param>
    /// <param name="value2">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the right of the add sign.</param>
    /// <returns>Sum of the vectors.</returns>
    public static Vector2I operator +(Vector2I value1, Vector2I value2)
    {
      value1.X += value2.X;
      value1.Y += value2.Y;
      return value1;
    }

    /// <summary>
    /// Subtracts a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> from a <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the left of the sub sign.</param>
    /// <param name="value2">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the right of the sub sign.</param>
    /// <returns>Result of the vector subtraction.</returns>
    public static Vector2I operator -(Vector2I value1, Vector2I value2)
    {
      value1.X -= value2.X;
      value1.Y -= value2.Y;
      return value1;
    }

    /// <summary>Multiplies the components of two vectors by each other.</summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the left of the mul sign.</param>
    /// <param name="value2">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the right of the mul sign.</param>
    /// <returns>Result of the vector multiplication.</returns>
    public static Vector2I operator *(Vector2I value1, Vector2I value2)
    {
      value1.X *= value2.X;
      value1.Y *= value2.Y;
      return value1;
    }

    /// <summary>Multiplies the components of vector by a scalar.</summary>
    /// <param name="value">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the left of the mul sign.</param>
    /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
    /// <returns>Result of the vector multiplication with a scalar.</returns>
    public static Vector2I operator *(Vector2I value, int scaleFactor)
    {
      value.X *= scaleFactor;
      value.Y *= scaleFactor;
      return value;
    }

    /// <summary>Multiplies the components of vector by a scalar.</summary>
    /// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
    /// <param name="value">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the right of the mul sign.</param>
    /// <returns>Result of the vector multiplication with a scalar.</returns>
    public static Vector2I operator *(int scaleFactor, Vector2I value)
    {
      value.X *= scaleFactor;
      value.Y *= scaleFactor;
      return value;
    }

    /// <summary>
    /// Divides the components of a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> by the components of another <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the left of the div sign.</param>
    /// <param name="value2">Divisor <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the right of the div sign.</param>
    /// <returns>The result of dividing the vectors.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2I operator /(Vector2I value1, Vector2I value2)
    {
      value1.X /= value2.X;
      value1.Y /= value2.Y;
      return value1;
    }

    /// <summary>
    /// Divides the components of a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> by a scalar.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" /> on the left of the div sign.</param>
    /// <param name="divider">Divisor scalar on the right of the div sign.</param>
    /// <returns>The result of dividing a vector by a scalar.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2I operator /(Vector2I value1, int divider)
    {
      value1.X /= divider;
      value1.Y /= divider;
      return value1;
    }

    /// <summary>
    /// Compares whether two <see cref="T:Microsoft.Xna.Framework.Vector2I" /> instances are equal.
    /// </summary>
    /// <param name="value1"><see cref="T:Microsoft.Xna.Framework.Vector2I" /> instance on the left of the equal sign.</param>
    /// <param name="value2"><see cref="T:Microsoft.Xna.Framework.Vector2I" /> instance on the right of the equal sign.</param>
    /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
    public static bool operator ==(Vector2I value1, Vector2I value2)
    {
      return value1.X == value2.X && value1.Y == value2.Y;
    }

    /// <summary>
    /// Compares whether two <see cref="T:Microsoft.Xna.Framework.Vector2I" /> instances are not equal.
    /// </summary>
    /// <param name="value1"><see cref="T:Microsoft.Xna.Framework.Vector2I" /> instance on the left of the not equal sign.</param>
    /// <param name="value2"><see cref="T:Microsoft.Xna.Framework.Vector2I" /> instance on the right of the not equal sign.</param>
    /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
    public static bool operator !=(Vector2I value1, Vector2I value2)
    {
      return value1.X != value2.X || value1.Y != value2.Y;
    }

    /// <summary>
    /// Performs vector addition on <paramref name="value1" /> and <paramref name="value2" />.
    /// </summary>
    /// <param name="value1">The first vector to add.</param>
    /// <param name="value2">The second vector to add.</param>
    /// <returns>The result of the vector addition.</returns>
    public static Vector2I Add(Vector2I value1, Vector2I value2)
    {
      value1.X += value2.X;
      value1.Y += value2.Y;
      return value1;
    }

    /// <summary>
    /// Performs vector addition on <paramref name="value1" /> and
    /// <paramref name="value2" />, storing the result of the
    /// addition in <paramref name="result" />.
    /// </summary>
    /// <param name="value1">The first vector to add.</param>
    /// <param name="value2">The second vector to add.</param>
    /// <param name="result">The result of the vector addition.</param>
    public static void Add(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
    }


    /// <summary>Clamps the specified value within a range.</summary>
    /// <param name="value1">The value to clamp.</param>
    /// <param name="min">The min value.</param>
    /// <param name="max">The max value.</param>
    /// <returns>The clamped value.</returns>
    public static Vector2I Clamp(Vector2I value1, Vector2I min, Vector2I max)
    {
      return new Vector2I(
        MathHelper.Clamp(value1.X, min.X, max.X),
        MathHelper.Clamp(value1.Y, min.Y, max.Y));
    }

    /// <summary>Clamps the specified value within a range.</summary>
    /// <param name="value1">The value to clamp.</param>
    /// <param name="min">The min value.</param>
    /// <param name="max">The max value.</param>
    /// <param name="result">The clamped value as an output parameter.</param>
    public static void Clamp(
      ref Vector2I value1,
      ref Vector2I min,
      ref Vector2I max,
      out Vector2I result)
    {
      result.X = MathHelper.Clamp(value1.X, min.X, max.X);
      result.Y = MathHelper.Clamp(value1.Y, min.Y, max.Y);
    }

    /// <summary>Returns the distance between two vectors.</summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The distance between two vectors.</returns>
    public static float Distance(Vector2I value1, Vector2I value2)
    {
      int num1 = value1.X - value2.X;
      int num2 = value1.Y - value2.Y;
      return MathF.Sqrt((float)(num1 * num1 + num2 * num2));
    }

    /// <summary>Returns the distance between two vectors.</summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="result">The distance between two vectors as an output parameter.</param>
    public static void Distance(ref Vector2I value1, ref Vector2I value2, out float result)
    {
      int num1 = value1.X - value2.X;
      int num2 = value1.Y - value2.Y;
      result = MathF.Sqrt((float)(num1 * num1 + num2 * num2));
    }

    /// <summary>Returns the squared distance between two vectors.</summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The squared distance between two vectors.</returns>
    public static int DistanceSquared(Vector2I value1, Vector2I value2)
    {
      int num1 = value1.X - value2.X;
      int num2 = value1.Y - value2.Y;
      return num1 * num1 + num2 * num2;
    }

    /// <summary>Returns the squared distance between two vectors.</summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="result">The squared distance between two vectors as an output parameter.</param>
    public static void DistanceSquared(ref Vector2I value1, ref Vector2I value2, out int result)
    {
      int num1 = value1.X - value2.X;
      int num2 = value1.Y - value2.Y;
      result = num1 * num1 + num2 * num2;
    }

    /// <summary>
    /// Divides the components of a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> by the components of another <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="value2">Divisor <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <returns>The result of dividing the vectors.</returns>
    public static Vector2I Divide(Vector2I value1, Vector2I value2)
    {
      value1.X /= value2.X;
      value1.Y /= value2.Y;
      return value1;
    }

    /// <summary>
    /// Divides the components of a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> by the components of another <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="value2">Divisor <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="result">The result of dividing the vectors as an output parameter.</param>
    public static void Divide(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
    }

    /// <summary>
    /// Divides the components of a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> by a scalar.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="divider">Divisor scalar.</param>
    /// <returns>The result of dividing a vector by a scalar.</returns>
    public static Vector2I Divide(Vector2I value1, int divider)
    {
      value1.X /= divider;
      value1.Y /= divider;
      return value1;
    }

    /// <summary>
    /// Divides the components of a <see cref="T:Microsoft.Xna.Framework.Vector2I" /> by a scalar.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="divider">Divisor scalar.</param>
    /// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
    public static void Divide(ref Vector2I value1, int divider, out Vector2I result)
    {
      result.X = value1.X / divider;
      result.Y = value1.Y / divider;
    }

    /// <summary>Returns a dot product of two vectors.</summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The dot product of two vectors.</returns>
    public static int Dot(Vector2I value1, Vector2I value2)
    {
      return value1.X * value2.X + value1.Y * value2.Y;
    }

    /// <summary>Returns a dot product of two vectors.</summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="result">The dot product of two vectors as an output parameter.</param>
    public static void Dot(ref Vector2I value1, ref Vector2I value2, out int result)
    {
      result = value1.X * value2.X + value1.Y * value2.Y;
    }

    /// <summary>
    /// Compares whether current instance is equal to specified <see cref="T:System.Object" />.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
    public override bool Equals(object obj) => obj is Vector2I other && this.Equals(other);

    /// <summary>
    /// Compares whether current instance is equal to specified <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    /// <param name="other">The <see cref="T:Microsoft.Xna.Framework.Vector2I" /> to compare.</param>
    /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
    public bool Equals(Vector2I other)
    {
      return this.X == other.X && this.Y == other.Y;
    }

    /// <summary>
    /// Gets the hash code of this <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    /// <returns>Hash code of this <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</returns>
    public override int GetHashCode() => this.X.GetHashCode() * 397 ^ this.Y.GetHashCode();


    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains a maximal values from the two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The <see cref="T:Microsoft.Xna.Framework.Vector2I" /> with maximal values from the two vectors.</returns>
    public static Vector2I Max(Vector2I value1, Vector2I value2)
    {
      return new Vector2I(value1.X > value2.X ? value1.X : value2.X, value1.Y > value2.Y ? value1.Y : value2.Y);
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains a maximal values from the two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="result">The <see cref="T:Microsoft.Xna.Framework.Vector2I" /> with maximal values from the two vectors as an output parameter.</param>
    public static void Max(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X > value2.X ? value1.X : value2.X;
      result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains a minimal values from the two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The <see cref="T:Microsoft.Xna.Framework.Vector2I" /> with minimal values from the two vectors.</returns>
    public static Vector2I Min(Vector2I value1, Vector2I value2)
    {
      return new Vector2I(value1.X < value2.X ? value1.X : value2.X, value1.Y < value2.Y ? value1.Y : value2.Y);
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains a minimal values from the two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="result">The <see cref="T:Microsoft.Xna.Framework.Vector2I" /> with minimal values from the two vectors as an output parameter.</param>
    public static void Min(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X < value2.X ? value1.X : value2.X;
      result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains a multiplication of two vectors.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="value2">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <returns>The result of the vector multiplication.</returns>
    public static Vector2I Multiply(Vector2I value1, Vector2I value2)
    {
      value1.X *= value2.X;
      value1.Y *= value2.Y;
      return value1;
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains a multiplication of two vectors.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="value2">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="result">The result of the vector multiplication as an output parameter.</param>
    public static void Multiply(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X * value2.X;
      result.Y = value1.Y * value2.Y;
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains a multiplication of <see cref="T:Microsoft.Xna.Framework.Vector2I" /> and a scalar.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="scaleFactor">Scalar value.</param>
    /// <returns>The result of the vector multiplication with a scalar.</returns>
    public static Vector2I Multiply(Vector2I value1, int scaleFactor)
    {
      value1.X *= scaleFactor;
      value1.Y *= scaleFactor;
      return value1;
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains a multiplication of <see cref="T:Microsoft.Xna.Framework.Vector2I" /> and a scalar.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="scaleFactor">Scalar value.</param>
    /// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
    public static void Multiply(ref Vector2I value1, int scaleFactor, out Vector2I result)
    {
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains the specified vector inversion.
    /// </summary>
    /// <param name="value">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <returns>The result of the vector inversion.</returns>
    public static Vector2I Negate(Vector2I value)
    {
      value.X = -value.X;
      value.Y = -value.Y;
      return value;
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains the specified vector inversion.
    /// </summary>
    /// <param name="value">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="result">The result of the vector inversion as an output parameter.</param>
    public static void Negate(ref Vector2I value, out Vector2I result)
    {
      result.X = -value.X;
      result.Y = -value.Y;
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains subtraction of on <see cref="T:Microsoft.Xna.Framework.Vector2I" /> from a another.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="value2">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <returns>The result of the vector subtraction.</returns>
    public static Vector2I Subtract(Vector2I value1, Vector2I value2)
    {
      value1.X -= value2.X;
      value1.Y -= value2.Y;
      return value1;
    }

    /// <summary>
    /// Creates a new <see cref="T:Microsoft.Xna.Framework.Vector2I" /> that contains subtraction of on <see cref="T:Microsoft.Xna.Framework.Vector2I" /> from a another.
    /// </summary>
    /// <param name="value1">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="value2">Source <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</param>
    /// <param name="result">The result of the vector subtraction as an output parameter.</param>
    public static void Subtract(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> representation of this <see cref="T:Microsoft.Xna.Framework.Vector2I" /> in the format:
    /// {X:[<see cref="F:Microsoft.Xna.Framework.Vector2I.X" />] Y:[<see cref="F:Microsoft.Xna.Framework.Vector2I.Y" />]}
    /// </summary>
    /// <returns>A <see cref="T:System.String" /> representation of this <see cref="T:Microsoft.Xna.Framework.Vector2I" />.</returns>
    public override string ToString()
    {
      return "{X:" + this.X.ToString() + " Y:" + this.Y.ToString() + "}";
    }

    /// <summary>
    /// Gets a <see cref="T:Microsoft.Xna.Framework.Point" /> representation for this object.
    /// </summary>
    /// <returns>A <see cref="T:Microsoft.Xna.Framework.Point" /> representation for this object.</returns>
    public Point ToPoint() => new Point(this.X, this.Y);





    /// <summary>
    /// Deconstruction method for <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Deconstruct(out int x, out int y)
    {
      x = this.X;
      y = this.Y;
    }

    //methods for converting Vector2I to Vector2 and vice versa
    public static implicit operator Vector2(Vector2I v) => new Vector2(v.X, v.Y);

    public static implicit operator System.Numerics.Vector2 (Vector2I v) => new System.Numerics.Vector2(v.X, v.Y);

  public static implicit operator Vector2I(Vector2 v) => new Vector2I((int)v.X, (int)v.Y);
    //how does this work? - implicit conversion operators allow you to convert between two types without explicitly calling a conversion method


  }
}