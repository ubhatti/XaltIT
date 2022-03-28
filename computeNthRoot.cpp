#include <iostream>
#include <math.h>		//Used for testing purposes only
#include <stdexcept>


// Inspiration from: https://en.wikipedia.org/wiki/Exponentiation_by_squaring
// and some Stackoverflow pages :)
double exponention_by_squaring (double base, int exp)
{
  if (exp < 0)
    throw std::logic_error ("input not managed, exponent is negative");
  if (exp == 1)
    return base;
  else if (exp == 0)
    return 1.0;
  else if (exp % 2 == 0)
    return exponention_by_squaring (base * base, exp / 2);
  else
    return base * exponention_by_squaring (base * base, exp / 2);
}

// This is an implementation of Newton rapson method for computing nth root.
double getNthRoot (double number, int n)
{
    if (n < 0 || number < 0)
    throw
      std::logic_error ("input not managed, exponent or number is negative");

  else if (n == 1)
    return number;
  else if (n == 0 || number == 1)
    return 1;
  else if (number == 0)
    return 0;

  else
    {
      double delta, x = number / n;
      do
        {
          delta = (number / exponention_by_squaring (x, n - 1) - x) / n;
          x += delta;
        }
      while (fabs (delta) >= 1e-5);
      return x;
    }
}


void test ()
{
  // Delta for comparing two results.
  float epsilon = 0.00001f;
  for (double i = 0; i < 100.1; i += 0.3)
    {
      for (int j = 0; j < 10; ++j)
        {
          double computedPow = pow (i, j);
          double rootValue = j;
          double expectedResult = pow (computedPow, 1 / rootValue);
          double rootResult = getNthRoot (computedPow, rootValue);

          if (fabs (expectedResult - rootResult) < epsilon)
            std::cout << "TEST Passed!!" << std::endl;
          else
            {
              std::cout << "Input base: " << i << " exponent: " << j <<
                std::endl;
              std::cout << "TEST Failed!!" << std::endl << " Expected:" <<
                expectedResult << " Computed: " << rootResult << std::endl;
            }
        }
    }

    //check negative values not managed:
    try{
    getNthRoot (-1, -1);
    }
    catch(std::logic_error &e)
    {
        std::cout << "Exception seen for negative values";
    }

}

int main ()
{
  test ();
  return 0;
}
