using System;
using System.Collections.Generic;
					
public class Program
{
	public class memoCell
	{
		public int val;
		public int parent;
	}
	
	// Given rod of length n units, and price of all pieces smaller than n 
	// find most profitable way of cutting the rod
	// https://www.youtube.com/watch?v=zFe-SX7jzDs
	public static void Main()
	{
		//length of piece , price
		Dictionary<int,int> rodPrices = new Dictionary<int,int>();
		//rodPrices.Add(1, 2);
		//rodPrices.Add(2, 5);
		//rodPrices.Add(3, 9);
		//rodPrices.Add(4, 6);
		
		rodPrices.Add(1, 1);
		rodPrices.Add(2, 5);
		rodPrices.Add(3, 8);
		rodPrices.Add(4, 9);
		rodPrices.Add(5, 10);
		rodPrices.Add(6, 17);
		rodPrices.Add(7, 17);
		rodPrices.Add(8, 20);
		
		
		
		int maxNumberOfPiecesCanBeCut = 0;
		foreach(var pair in rodPrices)
		{
			if (pair.Key > maxNumberOfPiecesCanBeCut)
			{
				maxNumberOfPiecesCanBeCut = pair.Key;
			}
		}
	
		//int rodLength = 5;
		int rodLength = 4;
		
		memoCell [,] memoOfObj = new memoCell[maxNumberOfPiecesCanBeCut+1, rodLength+1];
		int [,] memo = new int [maxNumberOfPiecesCanBeCut+1, rodLength+1];
		
		//populate zeros
		for(var i = 0; i <= maxNumberOfPiecesCanBeCut; i++)
		{
			memo[i,0] = 0;
		}
		for(var j = 0; j <= rodLength; j++)
		{
			memo[0,j] = 0;
		}

		for(var i = 0; i <= maxNumberOfPiecesCanBeCut; i++)
		{
			memoOfObj[i,0] = new memoCell(){ val = 0, parent = 0 };
		}
		for(var j = 0; j <= rodLength; j++)
		{
			memoOfObj[0,j] = new memoCell(){ val = 0, parent = 0 };
		}

		
		for(var j = 1; j <= rodLength; j++)
		{
			memo[1,j] = rodPrices[1] * j;
		}
		for(var j = 1; j <= rodLength; j++)
		{
			memoOfObj[1,j] = new memoCell(){ val = rodPrices[1] * j, parent = 0 };
		}

		//for(var j = 1; j <= rodLength; j++)
		//{
		//	Console.WriteLine(memoOfObj[1,j].val);
		//}

		
		// i = rod can be cut into pieces of size i
		// j = length of rod for current interation
		for(var i = 2; i <= maxNumberOfPiecesCanBeCut; i++)
		{
			for(var j = 1; j <= rodLength; j++)
			{
				if(j < i) // length of rod 1 cannot be cut into pieces of size 2
				{
					memo[i,j] = memo[i -1, j];
				}
				else
				{
					var PriceIncludingCurrentIterationSizeOfRod = rodPrices[i] + memo[i, j - i];
					var PriceExcludingCurrentIterationSizeOfRod = memo[i-1, j];
					memo[i,j] = Math.Max(PriceIncludingCurrentIterationSizeOfRod , PriceExcludingCurrentIterationSizeOfRod);
				}
			}
		}
		
		for(var i = 2; i <= maxNumberOfPiecesCanBeCut; i++)
		{
			for(var j = 1; j <= rodLength; j++)
			{
				if(j < i) // length of rod 1 cannot be cut into pieces of size 2
				{
					memoOfObj[i,j] = new memoCell(){ val = memo[i -1, j], parent = i-1}; 
				}
				else
				{
					var PriceIncludingCurrentIterationSizeOfRod = rodPrices[i] + memoOfObj[i, j - i].val;
					var PriceExcludingCurrentIterationSizeOfRod = memoOfObj[i-1, j].val;
					int parent = 0;
					if(PriceExcludingCurrentIterationSizeOfRod > PriceIncludingCurrentIterationSizeOfRod)
					{
						parent = i-1;
					}
					memoOfObj[i,j] = new memoCell() {
						val = Math.Max(PriceIncludingCurrentIterationSizeOfRod , PriceExcludingCurrentIterationSizeOfRod),
						parent = parent,
					};
				}
			}
		}
		
		
		for(var i = 0; i <= maxNumberOfPiecesCanBeCut; i++)
		{
			Console.Write("[ ");
			for(var j = 0; j <= rodLength; j++)
			{
				Console.Write( memo[i,j] + " ");
			}
			Console.Write("]");
			Console.WriteLine();
		}
		
		Console.WriteLine();
		
		for(var i = 0; i <= maxNumberOfPiecesCanBeCut; i++)
		{
			Console.Write("[ ");
			for(var j = 0; j <= rodLength; j++)
			{
				Console.Write( memoOfObj[i,j].val + "," + memoOfObj[i,j].parent + " ");
			}
			Console.Write("]");
			Console.WriteLine();
		}

		Console.WriteLine();
		Console.WriteLine("Max profit: " + memo[maxNumberOfPiecesCanBeCut, rodLength]);		
		
		
		int a = maxNumberOfPiecesCanBeCut; int b = rodLength; 
		int c = rodLength;
		List<int> pieceSizes = new List<int>(); 
		
		while(c > 0)
		{
			if(memoOfObj[a,b].parent == 0)
			{
				pieceSizes.Add(a);
				c = c - a;
				//a = a;
				b = b - a;
			}
			else
			{
				a = memoOfObj[a,b].parent;
				//b = b;
			}
		}
		
		Console.WriteLine();
		Console.Write("{ ");
		foreach(var p in pieceSizes)
		{
			Console.Write(p + " ");
		}
		Console.Write("}");
		
	}
}
