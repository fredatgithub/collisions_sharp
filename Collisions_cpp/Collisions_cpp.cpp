#include <iostream>
#include <vector>
#include <fstream>
#include <chrono>

struct vec3
{
  float x, y, z;

  vec3 operator+(const vec3& other) const noexcept
  {
    return vec3{ this->x + other.x, this->y + other.y, this->z + other.z };
  }

  vec3 operator-(const vec3& other) const noexcept
  {
    return vec3{ this->x - other.x, this->y - other.y, this->z - other.z };
  }

  float lenSq() const noexcept
  {
    return this->x * this->x + this->y * this->y + this->z * this->z;
  }
};

struct sphere
{
  vec3 pos;
  float r;

  bool collidesWith(const sphere& other) const noexcept
  {
    float radSum = this->r + other.r;
    return (other.pos - this->pos).lenSq() < radSum * radSum;
  }
};

int main()
{
  std::vector<sphere> spheres;

  std::ifstream f("spheres.dat");

  for (int i = 0; i < 50000; i++)
  {
    float x, y, z, r;
    f >> x >> y >> z >> r;
    spheres.push_back(sphere{ x, y, z, r });
  }

  auto start = std::chrono::steady_clock::now();

  for (int i = 0; i < spheres.size(); i++)
  {
    for (int j = i; j < spheres.size(); j++)
    {
      if (i != j && spheres[i].collidesWith(spheres[j]))
      {
        std::cout << i << " collides with " << j << "\n";
      }
    }
  }

  auto end = std::chrono::steady_clock::now();

  std::cout << std::chrono::duration_cast<std::chrono::milliseconds>(end - start).count();
  std::cin.get();

  return 0;
}