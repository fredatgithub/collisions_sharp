using System.Diagnostics;
using System.Globalization;

namespace Collisions_sharpNet8
{
  struct Vec3
  {
    float x, y, z;

    public Vec3(float x, float y, float z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public static Vec3 operator +(Vec3 a, Vec3 b)
    {
      return new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vec3 operator -(Vec3 a, Vec3 b)
    {
      return new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public float LenSq()
    {
      return this.x * this.x + this.y * this.y + this.z * this.z;
    }
  }

  class Sphere
  {
    Vec3 pos;
    float r;

    public Sphere(Vec3 pos, float r)
    {
      this.pos = pos;
      this.r = r;
    }

    public bool CollidesWith(Sphere other)
    {
      float radSum = this.r + other.r;
      return (other.pos - this.pos).LenSq() < radSum * radSum;
    }
  }

  class Program
  {
    static void Main()
    {
      var spheres = new List<Sphere>();
      using (var f = new StreamReader("spheres.dat"))
      {
        for (var i = 0; i < 50000; i++)
        {
          float x = float.Parse(f.ReadLine(), CultureInfo.InvariantCulture);
          float y = float.Parse(f.ReadLine(), CultureInfo.InvariantCulture);
          float z = float.Parse(f.ReadLine(), CultureInfo.InvariantCulture);
          float r = float.Parse(f.ReadLine(), CultureInfo.InvariantCulture);
          spheres.Add(new Sphere(new Vec3(x, y, z), r));
        }
      }

      var sw = new Stopwatch();
      sw.Start();

      for (var i = 0; i < spheres.Count; i++)
      {
        for (var j = i; j < spheres.Count; j++)
        {
          if (i != j && spheres[i].CollidesWith(spheres[j]))
          {
            Console.WriteLine("{0} collides with {1}", i, j);
          }
        }
      }

      sw.Stop();
      Console.WriteLine(sw.ElapsedMilliseconds);
      Console.WriteLine("press any key to exit:");
      Console.ReadKey();
    }
  }
}