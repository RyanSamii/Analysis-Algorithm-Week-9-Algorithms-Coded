// By: Ryan S
// Date: 2024-03-22
// Purpose: To implement the Horspool Algorithm in C#
// Note: This is a simple implementation of the Horspool Algorithm in C# to help understand the algorithm better.
// This is not the most efficient implementation of the algorithm and should not be used in production code.
// This is for educational purposes only.
// If you find issues or errors in the code, please let me know so I can correct them.
// ALSO NOTE: THIS IS NOT GOOD OBJECT ORIENTED PROGRAMMING PRACTICE. THIS IS JUST A SIMPLE IMPLEMENTATION OF THE ALGORITHM.

// text to parse
String text = "TEXT TO SEARCH HERE";

// pattern we're looking for
String pattern = "PATTERN TO SEARCH HERE";

// IGNORE THIS JUST A RANDOM DNA SEQUENCE TO USE FOR TESTING: atgtcgctgacctaccgttctctcggaagagaccgcttacatccacctgcaatcctctatgcaggaaagacgcctgtacgatggtcggttacgtcgtgcaattcagctggttccttgcgtaacgcaatacaatcgaatgacggacactcccattagtaaccttcggcattgcgttgctgaactcgaatcgagcgttatctgtagcttgtgttggtcggtcaacctagtgatggttgtttctccatgactccttttcggtgtactctgcgtggtgcttagaatactaggtcgagggccccagatcctcccaagagtgacgatagctacttcttccctttggctctagaatttagttatgttggcgctgccggaggtctgccgatcgatctgagatccaactggcctcgttatgacgccacggtgttttgggtttcagtcatgactattcgacgccatagtgcacaccttcacccctaaacggttgtgtgcgctcgcaataatcggttcttttgacaactaatcagaacaggcttccgctggaatgccactttgcaaatgattcagcgactagactgtagttagatctgtcctccacatgaccggcctttgagcatgagagacttagacacttggtcttaggtgggatacggcaagcaacctccgaaagttgccctcacagcgaggcgccccggttcaccatatgggggggcgtgttttctacgacaagaaactccgccggagcttaaaaagtagtaattctgcagttcatacctgtttagaccctatcgtagcccacgctccctcgtatatcttgacacccagtacgcaggcagattccacgaattaagaaacggacgttcggccgatgcctgggcaactatgacgacttatgatatacaagtgaggcacaccaacgggcgttcagatatttaggaccatgtatgataaccgtatttgggctcaattcacgtgaacgggagtacgatagatgcaaaatattgtataa


// Horspool algorithm but without priniting the steps
//static int Horspool(String text, String pattern)
//{
//	// create a shift table
//	Dictionary<char, int> shiftTable = new Dictionary<char, int>();
//	//for loop to fill the shift table based on the pattern
//	for (int i = 0; i < pattern.Length - 1; i++)
//	{
//		// fill the shift table with the unique characters in the pattern
//		if (!shiftTable.ContainsKey(pattern[i]))
//		{
//			shiftTable.Add(pattern[i], pattern.Length - i - 1);
//		}
//		else
//		{
//			shiftTable[pattern[i]] = pattern.Length - i - 1;
//		}
//	}

//	// parse through the text
//	for (int i = 0; i < text.Length;)
//	{
//		bool match = true;
//		// check if text at i matches pattern
//		for(int j = 0; j < pattern.Length && match == true; j++)
//		{
//			if (text[i+j] != pattern[j])
//			{
//				match = false;
//			}
//		}
//		if (match == true)
//		{
//			return i;
//		}
//		else
//		{
//			// find if last element is in shift table
//			if (shiftTable.ContainsKey(text[i + pattern.Length - 1 ]))
//			{
//				i += shiftTable[text[i + pattern.Length - 1]];
//			}
//			else
//			{
//				i += pattern.Length;
//			}
//		}
//	}

//	return -1;
//}


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

static int HorspoolStepByStep(String text, String pattern)
{
	// if pattern is longer than text, then there is no match immediately return -1
	if (pattern.Length > text.Length)
	{
		return -1;
	}

	int comparisonsCount = 0;
	// create a shift table
	Dictionary<char, int> shiftTable = new Dictionary<char, int>();
	shiftTable = buildShiftTable(pattern);

	// parse through the text
	for (int i = 0; i < text.Length;)
	{
		// print the text and pattern
		Console.WriteLine(text);
		for (int z = 0; z < i; z++) 
		{
			Console.Write(" ");
		}

		Console.WriteLine(pattern);
		bool match = true;
		// check if text at i matches pattern and count the number of comparisons
		for (int j = pattern.Length - 1; j >= 0 && match == true; j--)
		{
			comparisonsCount++;
			if (text[i + j] != pattern[j])
			{
				// if there is a mismatch set match to false
				match = false;
			}
		}
		// if match is true return the index
		if (match == true)
		{

			Console.WriteLine("# of Comparisions: " + comparisonsCount);
			return i;
		}
		else
		{

			// find if last element is in shift table
			if (shiftTable.ContainsKey(text[i + pattern.Length - 1]))
			{
				i += shiftTable[text[i + pattern.Length - 1]];
			}
			else
			{
				i += pattern.Length;
			}
			// if there is less than pattern length left in the text return -1
			if (i + pattern.Length > text.Length)
			{
				Console.WriteLine("# of Comparisions: " + comparisonsCount);
				return -1;
			}
		}
	}

	return -1;
}



// display the text and pattern
Console.WriteLine("Text to search:" + text);
Console.WriteLine("Pattern to Find:" + pattern);

// call the function
int patternLocated = HorspoolStepByStep(text, pattern);
string locationString = patternLocated == -1 ? "Pattern Not Found" : "Pattern Found At: " + patternLocated;
// print to console (if not found return -1)
Console.WriteLine(locationString);

// create a shift table
Dictionary<char, int> shiftTable = new Dictionary<char, int>();
shiftTable = buildShiftTable(pattern);

//display shift able in a nice formated 
for (int i = 0; i < shiftTable.Count; i++)
{
	Console.WriteLine(shiftTable.ElementAt(i));
}
Console.WriteLine("[NotFound," + pattern.Length + "]");