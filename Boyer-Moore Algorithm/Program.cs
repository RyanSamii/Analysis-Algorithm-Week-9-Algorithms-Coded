// By: Ryan S
// Date: 2024-03-22
// Purpose: To implement the Boyer-Moore Algorithm in C#
// Note: This is a simple implementation of the Boyer-Moore Algorithm in C# to help understand the algorithm better.
// This is not the most efficient implementation of the algorithm and should not be used in production code.
// This is for educational purposes only.
// If you find issues or errors in the code, please let me know so I can correct them.
// ALSO NOTE: THIS IS NOT GOOD OBJECT ORIENTED PROGRAMMING PRACTICE. THIS IS JUST A SIMPLE IMPLEMENTATION OF THE ALGORITHM.


// Function to build the shift table
static Dictionary<char, int> buildShiftTable(string pattern)
{
	// create a shift table
	Dictionary<char, int> shiftTable = new Dictionary<char, int>();
	//for loop to fill the shift table based on the pattern
	for (int i = 0; i < pattern.Length - 1; i++)
	{
		// fill the shift table with the unique characters in the pattern
		if (!shiftTable.ContainsKey(pattern[i]))
		{
			shiftTable.Add(pattern[i], pattern.Length - i - 1);
		}
		else
		{
			shiftTable[pattern[i]] = pattern.Length - i - 1;
		}
	}
	return shiftTable;
}

// Function to build the suffix table
static Dictionary<int, int> buildSuffixTable(string pattern)
{
	// create a shift table
	Dictionary<int, int> suffixTable = new Dictionary<int, int>();

	int k = 1;
	// for each character in the pattern
	for (int i = pattern.Length - 1; i >=0; i--)
	{
		// start with blank key string
		string keyString = new string("");

		int location = 0;
		// add the last k characters to the key string
		for (int y = k; y > 0; y--)
		{
			// the math works out so were staring at postion k and moving to the right when adding to the key string
			keyString += pattern.ElementAt(pattern.Length - y);
		}
		
		// For debugging to see what the key string is
		//Console.WriteLine("Looking for: " + keyString);

		// for each character in the pattern search 
		for(int z = pattern.Length -1; z >= 0; z--)
		{
			// location is how much we push the word over to get to the next match
			
			// so  HEHE with k of 2 would push the word over 2 to get to the next match
			// key string would be HE
			// so push by 2 -->--> to move to the next HE

			// location starts at 1 to skip the last character in the pattern 
			location = 1;
			bool foundMatch = false;
			// for each character in the key string skip the last character and adjusting for array counting aka starting at 0
			for (int x = pattern.Length - 2; x >= 0; x--)
			{
				// start by assuming it is a match
				bool match = true;
				// for each character in the key string
				for (int w = keyString.Length -1; w >= 0 && match == true; w--)
				{
					// if the key string is longer than the pattern, then it is not a match
					if ((x - (keyString.Length - w - 1)) < 0)
					{
						foundMatch = match;
						break;
					}
					// if the characters do not match, then it is not a match
					if (keyString.ElementAt(w) != pattern.ElementAt(x - (keyString.Length - w - 1)))
					{
						match = false;
					}

					// for debugging to see what is being compared
					// Console.WriteLine(keyString.ElementAt(w) + " compare with " + pattern.ElementAt(x - (keyString.Length - w - 1)));
				}
				// if it is a match, then we found the location so we break out of the loop
				// cause we only need to find the first match from the right
				if (match == true)
				{
					foundMatch = true;
					break;
				}
				// if it is not a match, then we increment the location aka the push and start over
				location++;
			}
			if (foundMatch == true)
			{
				// Console.WriteLine("Found match at: " + location);
				break;
			}
		}
		suffixTable.Add(k, location);
		k++;
	}
	return suffixTable;
}

static int BoyerMooreStepByStep(String text, String pattern)
{
	int comparisonsCount = 0;

	// create a shift table
	Dictionary<char, int> shiftTable = buildShiftTable(pattern);
	Dictionary<int, int> suffixTable = buildSuffixTable(pattern);

	// if pattern is longer than text, then there is no match immediately return -1
	if (pattern.Length > text.Length)
	{
		return -1;
	}

	// parse through the text
	for (int i = 0; i < text.Length;)
	{
		// print out the text and pattern to see what is being compared
		{
			Console.WriteLine(text);
			for (int z = 0; z < i; z++)
			{
				Console.Write(" ");
			}
			Console.WriteLine(pattern);
		}

		// start by assuming that there is a match untill proven otherwise
		bool match = true;
		// k is the count of matching characters starting from the right
		int k = 0;
		// check if text at i matches pattern
		for (int j = pattern.Length - 1; j >= 0; j--)
		{
			// increment the comparison count for each comparison
			comparisonsCount++;
			// if the characters do not match, then it is not a match
			if (text[i + j] != pattern[j])
			{
				// break out of the loop because it is not a match
				match = false;
				break;
			}
			// counts match so we increment the match count
			else
			{
				// increment the count of matching characters
				k++;
			}
		}
		// if it is a match, then we found the pattern
		if (match == true)
		{
			// print the number of comparisons
			Console.WriteLine("# of Comparisions: " + comparisonsCount);
			// return the index of the pattern
			return i;
		}
		// if it is not a match, then we need to shift the pattern
		else
		{
			// this will store the ammount to shift the pattern
			int shiftAmmount = 0;
			
			// Bad-symbol shift
			int d1 = 0;
			// good-suffix shift
			int d2 = 0;

			// find if last element is in shift table
			if (shiftTable.ContainsKey(text[i + pattern.Length - 1]))
			{
				// if match, then shift by the value in the shift table
				 d1 = shiftTable[text[i + pattern.Length - 1]];
			}
			else
			{
				// if no match, then shift by the length of the pattern
				d1= pattern.Length;
			}

			// Console writing To see what is happening
			Console.WriteLine("d1 = " + d1 + " - " + k + " = " + (d1-k));

			// calculate d1 for this loop
			d1 = d1 - k;
			// if d1 is less than or equal to 0, then we shift by 1 as a default because we want to always be moving at least 1 to the right
			if (d1 <= 0)
				d1 = 1;
			
			// for debugging to see what d1 is without showing all the calculations
			//Console.WriteLine("d1 = " + d1);

			
			// find d2 for this loop
			// find if k element is in suffix table
			if (suffixTable.ContainsKey(k))
			{
				d2 = suffixTable[k];
			}
			else
			{
				// if no match, then set d2 to 0
				d2 = 0;
			}

			// Console writing To see what is happening with d2
			Console.WriteLine("d2 = " + d2);
			
			// Find the max of d1 and d2
			shiftAmmount = Math.Max(d1, d2);

			// change the index by the shift ammount
			i += shiftAmmount;

			// if there is less than pattern length left in the text return -1 because the pattern is not in the text
			if (i + pattern.Length > text.Length)
			{
				Console.WriteLine("# of Comparisions: " + comparisonsCount);
				Console.WriteLine("Pattern now exceeds text length stopping search");
				return -1;
			}
		}
	}
	// If you got here I don't know what happened
	// I assume you had a pattern length of 1 and it was not found but even then you should get caught by the length check above
	// *shrug* I don't know :/
	return -1;
}


// printing the starting information

// text to parse
String text = "FIND_THE_PATTERN_HERE";
Console.WriteLine("Text to search:" + text);
// pattern we're looking for
String pattern = "HERE";
Console.WriteLine("Pattern to Find:" + pattern + "\n");



// call the function
int patternLocated = BoyerMooreStepByStep(text, pattern);

// adding space for clarity
Console.WriteLine();


// set the string location of the pattern in the text or if it was not found
string locationString = patternLocated == -1 ? "Pattern Not Found" : "Pattern Found At: " + patternLocated;

//print to console what the location string is
Console.WriteLine(locationString);

// adding space for clarity
Console.WriteLine();

// print the suffix table and shift table to see what is happening
{
	Console.WriteLine("suffix table:");
	//display shift able in a nice formated 
	Dictionary<int, int> prefixtTable = buildSuffixTable(pattern);
	for (int i = 0; i < prefixtTable.Count; i++)
	{
		Console.WriteLine(prefixtTable.ElementAt(i));
	}
}
// print the shift table to see what is happening
// its here in case you want to see it but just remember this isn't d1 its just the shift table that is needed to calculate d1

{
	Dictionary<char, int> shiftTable = buildShiftTable(pattern);
	Console.WriteLine("\nShift table (remember to subract k(number of matches)):");
	//display shift able in a nice formated 
	for (int i = 0; i < shiftTable.Count; i++)
	{
		Console.WriteLine(shiftTable.ElementAt(i));
	}
}