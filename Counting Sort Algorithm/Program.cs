// By: Ryan S
// Date: 2024-03-22
// Purpose: To implement the Counting Sort Algorithm in C#
// Note: This is a simple implementation of the Counting Sort Algorithm in C# to help understand the algorithm better.
// This is not the most efficient implementation of the algorithm and should not be used in production code.
// This is for educational purposes only.
// If you find issues or errors in the code, please let me know so I can correct them.
// ALSO NOTE: THIS IS NOT GOOD OBJECT ORIENTED PROGRAMMING PRACTICE. THIS IS JUST A SIMPLE IMPLEMENTATION OF THE ALGORITHM.


// build a frequency for counting sour algorithm
static Dictionary<char, int> buildFrequencyTable(char[] ArrayToSort)
{
	// to store new clean dictonary
	Dictionary<char, int> frequencyTable = new Dictionary<char, int>();

	//build frequency dictionary for each uniqe char in char array
	foreach (char c in ArrayToSort)
	{
		// if key in diconary increment frequency by 1
		if (frequencyTable.ContainsKey(c))
		{
			frequencyTable[c]++;
		}
		// if key not in dictionary add with base value of 1
		else
		{
			frequencyTable.Add(c, 1);
		}
	}
	//sort frequency table by key alphabetical because you have to?
	frequencyTable = frequencyTable.Keys.OrderBy(k => k).ToDictionary(k => k, k => frequencyTable[k]);

	// return frequency table
	return frequencyTable;
}


// build a ditribution dictionary for Counting Sort Algorithm
static Dictionary<char,int> buildDistributionTable(Dictionary<char, int>frequencyTable)
{
	// new clean dictonary for storage
	Dictionary<char,int> distributionTable = new Dictionary<char, int>();
	
	// calculate distribution for each value in frequency table (already sorted because frequency table was sorted)

	// unique first case
	distributionTable.Add(frequencyTable.ElementAt(0).Key, frequencyTable.ElementAt(0).Value);
	
	// start at 1 because unique case was handled prior to for loop
	for (int i = 1; i < frequencyTable.Count; i++)
	{
		// other cases beside
		distributionTable.Add(frequencyTable.ElementAt(i).Key, (distributionTable.ElementAt(i-1).Value + frequencyTable.ElementAt(i).Value));
	}
	// return distribution table
	return distributionTable;
}

// starting with a string to make it easier to change
string staringString = "STARTING STRING HERE";

// Char Array to sort
char[] container;
container = staringString.ToArray();
Console.WriteLine(" Starting string: " + staringString + "\n");

// Created Frequency and Distribution table
Dictionary<char, int> frequencyTable = new Dictionary<char, int>();
frequencyTable = buildFrequencyTable(container);
Dictionary<char, int> distributionTable = new Dictionary<char, int>();
distributionTable = buildDistributionTable(frequencyTable);

// printing for clarity
{
	Console.WriteLine(" Table of Frequency and Distributions");
	Console.Write(" Unique Character    : ");
	for (int i = 0; i < frequencyTable.Count; i++)
	{
		Console.Write(frequencyTable.ElementAt(i).Key + " ");
	}
	Console.WriteLine();
	Console.Write(" Frequency Values    : ");
	for (int i = 0; i < frequencyTable.Count; i++)
	{
		Console.Write(frequencyTable.ElementAt(i).Value + " ");
	}
	Console.WriteLine();
	Console.Write(" Distirbutions Value : ");
	for (int i = 0; i < distributionTable.Count; i++)
	{
		Console.Write(distributionTable.ElementAt(i).Value + " ");
	}
	Console.WriteLine();
	Console.WriteLine();
}

// place to store sorted array so we dont overwrite container
char[] sortedArray = new char[container.Length];

// start sorting based on table values
for (int i = container.Length-1; i >= 0; i--)
{
	// subtract 1 to get correct postion in array
	distributionTable[container[i]]--;
	
	// for debunggin can uncomment to clarify whats happening
	//Console.WriteLine("Sorting: " + container[i] + " From Postion: "+ i + " To Postion: " + distributionTable[container[i]]);
	
	
	// put the element at i into the sorted arrary at position decided by the distribution value table
	sortedArray[distributionTable[container[i]]] = container[i];

	// print sorted array to see whats happening
	{
		Console.Write(" Sorted Array: ");
		Console.Write("|");
		for (int j = 0; j < sortedArray.Length; j++)
		{
			if (sortedArray[j] == 0)
			{
				Console.Write(" ");
			}
			else
			{
				Console.Write(sortedArray[j]);
			}
			Console.Write("|");
		}
		Console.WriteLine();
	}

	// printing distribution table to see whats happening
	{
		Console.Write(" Unique Character    : ");
		for (int x = 0; x < frequencyTable.Count; x++)
		{
			Console.Write(frequencyTable.ElementAt(x).Key + " ");
		}
		Console.WriteLine();
		Console.Write(" Distirbutions Value : ");
		for (int x = 0; x < distributionTable.Count; x++)
		{
			Console.Write(distributionTable.ElementAt(x).Value + " ");
		}
		Console.WriteLine();
	}

	Console.WriteLine();
}