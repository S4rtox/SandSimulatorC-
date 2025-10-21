// Modificado desde Microsoft.Xna.Framework.Vector2
// Tipo: Microsoft.Xna.Framework.Vector2I
// Cambios: float a int en todo el archivo, se eliminaron métodos que no tienen sentido con enteros (como Normalize)

#nullable disable
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework
{
  /// <summary>Describe un vector 2D utilizando enteros.</summary>
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
    /// La coordenada X de este <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    [DataMember] public int X;

    /// <summary>
    /// La coordenada Y de este <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    [DataMember] public int Y;

    /// <summary>
    /// Devuelve un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> con componentes 0, 0.
    /// </summary>
    public static Vector2I Zero => zeroVector;

    /// <summary>
    /// Devuelve un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> con componentes 1, 1.
    /// </summary>
    public static Vector2I One => unitVector;

    /// <summary>
    /// Devuelve un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> con componentes 1, 0.
    /// </summary>
    public static Vector2I UnitX => unitXVector;

    /// <summary>
    /// Devuelve un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> con componentes 0, 1.
    /// </summary>
    public static Vector2I UnitY => unitYVector;

    internal string DebugDisplayString => this.X.ToString() + "  " + this.Y.ToString();

    /// <summary>Construye un vector 2D con X e Y a partir de dos valores.</summary>
    /// <param name="x">La coordenada X en espacio 2D.</param>
    /// <param name="y">La coordenada Y en espacio 2D.</param>
    public Vector2I(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    /// <summary>
    /// Construye un vector 2D con X e Y establecidos al mismo valor.
    /// </summary>
    /// <param name="value">Las coordenadas X e Y en espacio 2D.</param>
    public Vector2I(int value)
    {
      this.X = value;
      this.Y = value;
    }

    /// <summary>
    /// Invierte los signos de los componentes del <see cref="T:Microsoft.Xna.Framework.Vector2I" /> especificado.
    /// </summary>
    /// <param name="value">Vector origen a la derecha del signo menos.</param>
    /// <returns>Resultado de la inversión.</returns>
    public static Vector2I operator -(Vector2I value)
    {
      value.X = -value.X;
      value.Y = -value.Y;
      return value;
    }

    /// <summary>Suma dos vectores.</summary>
    /// <param name="value1">Vector origen a la izquierda del signo de suma.</param>
    /// <param name="value2">Vector origen a la derecha del signo de suma.</param>
    /// <returns>Suma de los vectores.</returns>
    public static Vector2I operator +(Vector2I value1, Vector2I value2)
    {
      value1.X += value2.X;
      value1.Y += value2.Y;
      return value1;
    }

    /// <summary>
    /// Resta un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> de otro.
    /// </summary>
    /// <param name="value1">Vector a la izquierda del signo de resta.</param>
    /// <param name="value2">Vector a la derecha del signo de resta.</param>
    /// <returns>Resultado de la resta de vectores.</returns>
    public static Vector2I operator -(Vector2I value1, Vector2I value2)
    {
      value1.X -= value2.X;
      value1.Y -= value2.Y;
      return value1;
    }

    /// <summary>Multiplica componente a componente dos vectores.</summary>
    /// <param name="value1">Vector a la izquierda del signo de multiplicación.</param>
    /// <param name="value2">Vector a la derecha del signo de multiplicación.</param>
    /// <returns>Resultado de la multiplicación de vectores.</returns>
    public static Vector2I operator *(Vector2I value1, Vector2I value2)
    {
      value1.X *= value2.X;
      value1.Y *= value2.Y;
      return value1;
    }

    /// <summary>Multiplica los componentes del vector por un escalar.</summary>
    /// <param name="value">Vector a la izquierda del signo de multiplicación.</param>
    /// <param name="scaleFactor">Valor escalar a la derecha del signo de multiplicación.</param>
    /// <returns>Resultado de la multiplicación por escalar.</returns>
    public static Vector2I operator *(Vector2I value, int scaleFactor)
    {
      value.X *= scaleFactor;
      value.Y *= scaleFactor;
      return value;
    }

    /// <summary>Multiplica los componentes del vector por un escalar.</summary>
    /// <param name="scaleFactor">Valor escalar a la izquierda del signo de multiplicación.</param>
    /// <param name="value">Vector a la derecha del signo de multiplicación.</param>
    /// <returns>Resultado de la multiplicación por escalar.</returns>
    public static Vector2I operator *(int scaleFactor, Vector2I value)
    {
      value.X *= scaleFactor;
      value.Y *= scaleFactor;
      return value;
    }

    /// <summary>
    /// Divide los componentes de un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> por los de otro.
    /// </summary>
    /// <param name="value1">Vector a la izquierda del signo de división.</param>
    /// <param name="value2">Vector divisor a la derecha del signo de división.</param>
    /// <returns>Resultado de la división de vectores.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2I operator /(Vector2I value1, Vector2I value2)
    {
      value1.X /= value2.X;
      value1.Y /= value2.Y;
      return value1;
    }

    /// <summary>
    /// Divide los componentes de un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> por un escalar.
    /// </summary>
    /// <param name="value1">Vector a la izquierda del signo de división.</param>
    /// <param name="divider">Divisor escalar a la derecha del signo de división.</param>
    /// <returns>Resultado de la división por escalar.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2I operator /(Vector2I value1, int divider)
    {
      value1.X /= divider;
      value1.Y /= divider;
      return value1;
    }

    /// <summary>
    /// Compara si dos instancias de <see cref="T:Microsoft.Xna.Framework.Vector2I" /> son iguales.
    /// </summary>
    /// <param name="value1">Instancia a la izquierda del signo de igualdad.</param>
    /// <param name="value2">Instancia a la derecha del signo de igualdad.</param>
    /// <returns><c>true</c> si las instancias son iguales; en caso contrario, <c>false</c>.</returns>
    public static bool operator ==(Vector2I value1, Vector2I value2)
    {
      return value1.X == value2.X && value1.Y == value2.Y;
    }

    /// <summary>
    /// Compara si dos instancias de <see cref="T:Microsoft.Xna.Framework.Vector2I" /> son distintas.
    /// </summary>
    /// <param name="value1">Instancia a la izquierda del signo de desigualdad.</param>
    /// <param name="value2">Instancia a la derecha del signo de desigualdad.</param>
    /// <returns><c>true</c> si las instancias son distintas; en caso contrario, <c>false</c>.</returns>
    public static bool operator !=(Vector2I value1, Vector2I value2)
    {
      return value1.X != value2.X || value1.Y != value2.Y;
    }

    /// <summary>
    /// Realiza la suma vectorial de <paramref name="value1" /> y <paramref name="value2" />.
    /// </summary>
    /// <param name="value1">Primer vector a sumar.</param>
    /// <param name="value2">Segundo vector a sumar.</param>
    /// <returns>Resultado de la suma vectorial.</returns>
    public static Vector2I Add(Vector2I value1, Vector2I value2)
    {
      value1.X += value2.X;
      value1.Y += value2.Y;
      return value1;
    }

    /// <summary>
    /// Suma <paramref name="value1" /> y <paramref name="value2" /> y almacena el resultado en <paramref name="result" />.
    /// </summary>
    /// <param name="value1">Primer vector a sumar.</param>
    /// <param name="value2">Segundo vector a sumar.</param>
    /// <param name="result">Resultado de la suma vectorial.</param>
    public static void Add(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X + value2.X;
      result.Y = value1.Y + value2.Y;
    }


    /// <summary>Restringe el valor especificado dentro de un rango.</summary>
    /// <param name="value1">El valor a limitar.</param>
    /// <param name="min">El valor mínimo.</param>
    /// <param name="max">El valor máximo.</param>
    /// <returns>El valor limitado.</returns>
    public static Vector2I Clamp(Vector2I value1, Vector2I min, Vector2I max)
    {
      return new Vector2I(
        MathHelper.Clamp(value1.X, min.X, max.X),
        MathHelper.Clamp(value1.Y, min.Y, max.Y));
    }

    /// <summary>Restringe el valor especificado dentro de un rango.</summary>
    /// <param name="value1">El valor a limitar.</param>
    /// <param name="min">El valor mínimo.</param>
    /// <param name="max">El valor máximo.</param>
    /// <param name="result">El valor limitado como parámetro de salida.</param>
    public static void Clamp(
      ref Vector2I value1,
      ref Vector2I min,
      ref Vector2I max,
      out Vector2I result)
    {
      result.X = MathHelper.Clamp(value1.X, min.X, max.X);
      result.Y = MathHelper.Clamp(value1.Y, min.Y, max.Y);
    }

    /// <summary>Devuelve la distancia entre dos vectores.</summary>
    /// <param name="value1">El primer vector.</param>
    /// <param name="value2">El segundo vector.</param>
    /// <returns>La distancia entre dos vectores.</returns>
    public static float Distance(Vector2I value1, Vector2I value2)
    {
      int num1 = value1.X - value2.X;
      int num2 = value1.Y - value2.Y;
      return MathF.Sqrt((float)(num1 * num1 + num2 * num2));
    }

    /// <summary>Devuelve la distancia entre dos vectores.</summary>
    /// <param name="value1">El primer vector.</param>
    /// <param name="value2">El segundo vector.</param>
    /// <param name="result">La distancia como parámetro de salida.</param>
    public static void Distance(ref Vector2I value1, ref Vector2I value2, out float result)
    {
      int num1 = value1.X - value2.X;
      int num2 = value1.Y - value2.Y;
      result = MathF.Sqrt((float)(num1 * num1 + num2 * num2));
    }

    /// <summary>Devuelve la distancia al cuadrado entre dos vectores.</summary>
    /// <param name="value1">El primer vector.</param>
    /// <param name="value2">El segundo vector.</param>
    /// <returns>La distancia al cuadrado entre dos vectores.</returns>
    public static int DistanceSquared(Vector2I value1, Vector2I value2)
    {
      int num1 = value1.X - value2.X;
      int num2 = value1.Y - value2.Y;
      return num1 * num1 + num2 * num2;
    }

    /// <summary>Devuelve la distancia al cuadrado entre dos vectores.</summary>
    /// <param name="value1">El primer vector.</param>
    /// <param name="value2">El segundo vector.</param>
    /// <param name="result">La distancia al cuadrado como parámetro de salida.</param>
    public static void DistanceSquared(ref Vector2I value1, ref Vector2I value2, out int result)
    {
      int num1 = value1.X - value2.X;
      int num2 = value1.Y - value2.Y;
      result = num1 * num1 + num2 * num2;
    }

    /// <summary>
    /// Divide los componentes de un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> por los de otro.
    /// </summary>
    /// <param name="value1">Vector origen.</param>
    /// <param name="value2">Vector divisor.</param>
    /// <returns>Resultado de dividir los vectores.</returns>
    public static Vector2I Divide(Vector2I value1, Vector2I value2)
    {
      value1.X /= value2.X;
      value1.Y /= value2.Y;
      return value1;
    }

    /// <summary>
    /// Divide los componentes de un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> por los de otro.
    /// </summary>
    /// <param name="value1">Vector origen.</param>
    /// <param name="value2">Vector divisor.</param>
    /// <param name="result">Resultado de dividir los vectores como salida.</param>
    public static void Divide(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X / value2.X;
      result.Y = value1.Y / value2.Y;
    }

    /// <summary>
    /// Divide los componentes de un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> por un escalar.
    /// </summary>
    /// <param name="value1">Vector origen.</param>
    /// <param name="divider">Divisor escalar.</param>
    /// <returns>Resultado de dividir el vector por un escalar.</returns>
    public static Vector2I Divide(Vector2I value1, int divider)
    {
      value1.X /= divider;
      value1.Y /= divider;
      return value1;
    }

    /// <summary>
    /// Divide los componentes de un <see cref="T:Microsoft.Xna.Framework.Vector2I" /> por un escalar.
    /// </summary>
    /// <param name="value1">Vector origen.</param>
    /// <param name="divider">Divisor escalar.</param>
    /// <param name="result">Resultado de la división como salida.</param>
    public static void Divide(ref Vector2I value1, int divider, out Vector2I result)
    {
      result.X = value1.X / divider;
      result.Y = value1.Y / divider;
    }

    /// <summary>Devuelve el producto punto de dos vectores.</summary>
    /// <param name="value1">El primer vector.</param>
    /// <param name="value2">El segundo vector.</param>
    /// <returns>El producto punto de ambos vectores.</returns>
    public static int Dot(Vector2I value1, Vector2I value2)
    {
      return value1.X * value2.X + value1.Y * value2.Y;
    }

    /// <summary>Devuelve el producto punto de dos vectores.</summary>
    /// <param name="value1">El primer vector.</param>
    /// <param name="value2">El segundo vector.</param>
    /// <param name="result">El producto punto como salida.</param>
    public static void Dot(ref Vector2I value1, ref Vector2I value2, out int result)
    {
      result = value1.X * value2.X + value1.Y * value2.Y;
    }

    /// <summary>
    /// Compara si la instancia actual es igual al <see cref="T:System.Object" /> especificado.
    /// </summary>
    /// <param name="obj">El objeto con el que comparar.</param>
    /// <returns><c>true</c> si son iguales; en caso contrario, <c>false</c>.</returns>
    public override bool Equals(object obj) => obj is Vector2I other && this.Equals(other);

    /// <summary>
    /// Compara si la instancia actual es igual al <see cref="T:Microsoft.Xna.Framework.Vector2I" /> especificado.
    /// </summary>
    /// <param name="other">El <see cref="T:Microsoft.Xna.Framework.Vector2I" /> con el que comparar.</param>
    /// <returns><c>true</c> si son iguales; en caso contrario, <c>false</c>.</returns>
    public bool Equals(Vector2I other)
    {
      return this.X == other.X && this.Y == other.Y;
    }

    /// <summary>
    /// Obtiene el código hash de este <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    /// <returns>Código hash de esta instancia.</returns>
    public override int GetHashCode() => this.X.GetHashCode() * 397 ^ this.Y.GetHashCode();


    /// <summary>
    /// Crea un nuevo <see cref="T:Microsoft.Xna.Framework.Vector2I" /> que contiene los valores máximos entre dos vectores.
    /// </summary>
    /// <param name="value1">El primer vector.</param>
    /// <param name="value2">El segundo vector.</param>
    /// <returns>El <see cref="T:Microsoft.Xna.Framework.Vector2I" /> con los valores máximos.</returns>
    public static Vector2I Max(Vector2I value1, Vector2I value2)
    {
      return new Vector2I(value1.X > value2.X ? value1.X : value2.X, value1.Y > value2.Y ? value1.Y : value2.Y);
    }

    /// <summary>
    /// Crea un nuevo <see cref="T:Microsoft.Xna.Framework.Vector2I" /> que contiene los valores máximos entre dos vectores.
    /// </summary>
    /// <param name="value1">El primer vector.</param>
    /// <param name="value2">El segundo vector.</param>
    /// <param name="result">El vector resultante como salida.</param>
    public static void Max(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X > value2.X ? value1.X : value2.X;
      result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
    }

    /// <summary>
    /// Crea un nuevo <see cref="T:Microsoft.Xna.Framework.Vector2I" /> que contiene los valores mínimos entre dos vectores.
    /// </summary>
    /// <param name="value1">El primer vector.</param>
    /// <param name="value2">El segundo vector.</param>
    /// <returns>El <see cref="T:Microsoft.Xna.Framework.Vector2I" /> con los valores mínimos.</returns>
    public static Vector2I Min(Vector2I value1, Vector2I value2)
    {
      return new Vector2I(value1.X < value2.X ? value1.X : value2.X, value1.Y < value2.Y ? value1.Y : value2.Y);
    }

    /// <summary>
    /// Crea un nuevo <see cref="T:Microsoft.Xna.Framework.Vector2I" /> que contiene los valores mínimos entre dos vectores.
    /// </summary>
    /// <param name="value1">El primer vector.</param>
    /// <param name="value2">El segundo vector.</param>
    /// <param name="result">El vector resultante como salida.</param>
    public static void Min(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X < value2.X ? value1.X : value2.X;
      result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
    }

    /// <summary>
    /// Crea un nuevo <see cref="T:Microsoft.Xna.Framework.Vector2I" /> que contiene la multiplicación componente a componente de dos vectores.
    /// </summary>
    /// <param name="value1">Vector origen.</param>
    /// <param name="value2">Vector origen.</param>
    /// <returns>Resultado de la multiplicación de vectores.</returns>
    public static Vector2I Multiply(Vector2I value1, Vector2I value2)
    {
      value1.X *= value2.X;
      value1.Y *= value2.Y;
      return value1;
    }

    /// <summary>
    /// Crea un nuevo <see cref="T:Microsoft.Xna.Framework.Vector2I" /> que contiene la multiplicación de un vector por un escalar.
    /// </summary>
    /// <param name="value1">Vector origen.</param>
    /// <param name="scaleFactor">Valor escalar.</param>
    /// <param name="result">Resultado de la multiplicación como salida.</param>
    public static void Multiply(ref Vector2I value1, int scaleFactor, out Vector2I result)
    {
      result.X = value1.X * scaleFactor;
      result.Y = value1.Y * scaleFactor;
    }

    /// <summary>
    /// Crea un nuevo <see cref="T:Microsoft.Xna.Framework.Vector2I" /> que contiene la inversión de los signos del vector especificado.
    /// </summary>
    /// <param name="value">Vector origen.</param>
    /// <returns>Resultado de la inversión.</returns>
    public static Vector2I Negate(Vector2I value)
    {
      value.X = -value.X;
      value.Y = -value.Y;
      return value;
    }

    /// <summary>
    /// Crea un nuevo <see cref="T:Microsoft.Xna.Framework.Vector2I" /> que contiene la inversión de los signos del vector especificado.
    /// </summary>
    /// <param name="value">Vector origen.</param>
    /// <param name="result">Resultado de la inversión como salida.</param>
    public static void Negate(ref Vector2I value, out Vector2I result)
    {
      result.X = -value.X;
      result.Y = -value.Y;
    }

    /// <summary>
    /// Crea un nuevo <see cref="T:Microsoft.Xna.Framework.Vector2I" /> que contiene la resta de un vector respecto a otro.
    /// </summary>
    /// <param name="value1">Vector origen.</param>
    /// <param name="value2">Vector a restar.</param>
    /// <returns>Resultado de la resta de vectores.</returns>
    public static Vector2I Subtract(Vector2I value1, Vector2I value2)
    {
      value1.X -= value2.X;
      value1.Y -= value2.Y;
      return value1;
    }

    /// <summary>
    /// Crea un nuevo <see cref="T:Microsoft.Xna.Framework.Vector2I" /> que contiene la resta de un vector respecto a otro.
    /// </summary>
    /// <param name="value1">Vector origen.</param>
    /// <param name="value2">Vector a restar.</param>
    /// <param name="result">Resultado de la resta como salida.</param>
    public static void Subtract(ref Vector2I value1, ref Vector2I value2, out Vector2I result)
    {
      result.X = value1.X - value2.X;
      result.Y = value1.Y - value2.Y;
    }

    /// <summary>
    /// Devuelve una representación <see cref="T:System.String" /> de este <see cref="T:Microsoft.Xna.Framework.Vector2I" /> con el formato:
    /// {X:[<see cref="F:Microsoft.Xna.Framework.Vector2I.X" />] Y:[<see cref="F:Microsoft.Xna.Framework.Vector2I.Y" />]}
    /// </summary>
    /// <returns>Una cadena que representa este vector.</returns>
    public override string ToString()
    {
      return "{X:" + this.X.ToString() + " Y:" + this.Y.ToString() + "}";
    }

    /// <summary>
    /// Obtiene una representación <see cref="T:Microsoft.Xna.Framework.Point" /> para este objeto.
    /// </summary>
    /// <returns>Una instancia de <see cref="T:Microsoft.Xna.Framework.Point" /> equivalente.</returns>
    public Point ToPoint() => new Point(this.X, this.Y);





    /// <summary>
    /// Método de deconstrucción para <see cref="T:Microsoft.Xna.Framework.Vector2I" />.
    /// </summary>
    /// <param name="x">Componente X resultante.</param>
    /// <param name="y">Componente Y resultante.</param>
    public void Deconstruct(out int x, out int y)
    {
      x = this.X;
      y = this.Y;
    }

    // métodos para convertir Vector2I a Vector2 y viceversa
    public static implicit operator Vector2(Vector2I v) => new Vector2(v.X, v.Y);

    public static implicit operator System.Numerics.Vector2 (Vector2I v) => new System.Numerics.Vector2(v.X, v.Y);

  public static implicit operator Vector2I(Vector2 v) => new Vector2I((int)v.X, (int)v.Y);
    // ¿Cómo funciona esto? Los operadores de conversión implícita permiten convertir entre dos tipos sin llamar explícitamente a un método de conversión


  }
}