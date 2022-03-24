using System;
using System.Collections.Generic;
using System.Linq;

namespace EqualHashCodeStringGenerator
{
	// Solution to the problem statement: https://gitlab.com/exalt-it-dojo/katas-c-sharp/-/tree/main/optim-SameHashCode
	public class StringGenerator
	{
		static Random r = new Random();
		public HashSet<string> GenerateThreeStrings()
		{
			var lookup = new Dictionary<int, HashSet<string>>();
			HashSet<string> resultStrings;
			while (true) //TODO: if three strings are not found, memory problem can occur, so limit number of iterations.
			{
				var s = GenerateRandomString(6);
				var currentHash = s.GetHashCode();
				if (lookup.TryGetValue(currentHash, out resultStrings))
				{
					// minor optimization
					if (resultStrings.Contains(s))
						continue;

					resultStrings.Add(s);
					lookup[currentHash] = resultStrings;
					if (resultStrings.Count >= 3)
					{
						var count = string.Join(", ", resultStrings);
						Console.WriteLine("Found three non equal strings with same hashcode: " + count);
						break;
					}
				}
				else
				{
					// Heurestic to stop adding in lookup dict after 100K entries, otherwise memory saturation can occur.
					if (lookup.Count < 100000)
					{
						var newSet = new HashSet<string>();
						newSet.Add(s);
						lookup[currentHash] = newSet;
					}
				}
			}

			return resultStrings;
		}

		string GenerateRandomString(int length)
		{
			string s = "";
			for(int i=0;i<length; ++i)
			{
				s +=((char)r.Next((int)'a', ((int)'z') + 1)).ToString();
			}
			return s;
		}

}

	public class StringGeneratorTests
	{
		// Poor man's Unit test suite :)
		// This is coded in .Net Fiddle IDE so MSTests are not available.
		private const int numberOfStringsExpected = 3;
		public void RunTest()
		{
			StringGenerator sg = new StringGenerator();
			HashSet<string> generatedSet = sg.GenerateThreeStrings();
			if (generatedSet.Count == numberOfStringsExpected)
				Console.WriteLine("TEST PASSED!!!");
			else
				Console.WriteLine("TEST FAILED!!!");
			if (generatedSet.Count == numberOfStringsExpected)
			{
				List<string> hList = generatedSet.ToList();
				string stringA = hList[0];
				string stringB = hList[1];
				string stringC = hList[2];
				if (stringA.GetHashCode() == stringB.GetHashCode() && !String.Equals(stringA, stringB) && stringB.GetHashCode() == stringC.GetHashCode() && !String.Equals(stringB, stringC) && !String.Equals(stringA, stringC))
					Console.WriteLine("TEST PASSED!!!");
				else
					Console.WriteLine("TEST FAILED!!!");
			}
		}

		public static void Main()
		{
			new StringGeneratorTests().RunTest();
		}
	}
}