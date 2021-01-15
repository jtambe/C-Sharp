using System;
using System.Collections.Generic;

namespace CuttingRod_MaxProfit
{
    class Program
    {

		// Given rod of length n units, and price of all pieces smaller than n 
		// find most profitable way of cutting the rod
		// https://www.youtube.com/watch?v=zFe-SX7jzDs


		public class memoCell
		{
			public int val;
			public int parent;
		}

		public static int getMaxProfitUsingObjectArray(Dictionary<int, int> rodPrices, int rodLength, out List<int> pieces)
		{
			int maxNumberOfPiecesCanBeCut = 0;
			foreach (var pair in rodPrices)
			{
				if (pair.Key > maxNumberOfPiecesCanBeCut)
				{
					maxNumberOfPiecesCanBeCut = pair.Key;
				}
			}

			memoCell[,] memoOfObj = new memoCell[maxNumberOfPiecesCanBeCut + 1, rodLength + 1];

			//populate zeros
			// there is no rod of length zero physically possible
			// and no rod can be cut into pieces of size 0
			for (var i = 0; i <= maxNumberOfPiecesCanBeCut; i++)
			{
				memoOfObj[i, 0] = new memoCell() { val = 0, parent = 0 };
			}
			for (var j = 0; j <= rodLength; j++)
			{
				memoOfObj[0, j] = new memoCell() { val = 0, parent = 0 };
			}

			// populate row for cut size of 1
			// i = rod can be cut into pieces of size i
			// j = length of rod for current interation
			for (var j = 1; j <= rodLength; j++)
			{
				// A rod of length 5 can only be cut in 5 pieces, each of size 1
				// max profit we can have by cutting a rod of length 5 in 5 pieces is 1*5, 
				// similary for rodlength = 6, it can be 1*6
				memoOfObj[1, j] = new memoCell() { val = rodPrices[1] * j, parent = 0 };
			}

			// i = rod can be cut into pieces of size i
			// j = length of rod for current interation
			for (var i = 2; i <= maxNumberOfPiecesCanBeCut; i++)
			{
				for (var j = 1; j <= rodLength; j++)
				{
					if (j < i) 
					{
						// length of rod 1 cannot be cut into pieces of size 2 

						// likewise rod of length 2 cannot be cut into pieces of size 3 ...
						// In such case, we can only make a sell with max profit for Length of rod 2 by cutting it into {0, 1, 2} size of pieces
						// and the value of that profit is already calculated in the row with size = 2 and column with total rodLength = 2

						// likewise rod of length 1 cannot be cut into pieces of size 3 ...
						// In such case, we can only make a sell with max profit for Length of rod 1 by cutting it into {0, 1} size of pieces
						// and the value of that profit is already calculated in the row with size = 1 and column with total rodLength = 1

						// in Such cases, parent defines from where max profit can be drawn
						// parent shows previous row with same column,
						// for example, maxProfit using rodLength = 2, and pieces of sizes upto 3 is same as maxProfit that can be drawn by cutting rodLength of 2 in pieces of upto 2 {0,1,2}
						// for example, maxProfit using rodLength = 1, and pieces of sizes upto 3 is same as maxProfit that can be drawn by cutting rodLength of 1 in pieces of upto 1 {0,1}

						memoOfObj[i, j] = new memoCell() { val = memoOfObj[i - 1, j].val, parent = i - 1 };
					}
					else
					{
						// max profit that can be drawn can be calulated as max sell price of
						// sell price of length of rod by including new allowed cutSize
						// and sell price of length of rod without including new cutSize

						// for example,
						// when calculating max profit values for row of 3, which means rod can be cut in sizes of {0,1,2,3}
						// max profit can either be when rod is cut into pieces of size 3 and others or 
						// when rod is never cut into pieces of size 3. which means only use maxprofit data when rod was cut into sizes {0,1,2}
						// that explains long variable names PriceIncludingCurrentIterationSizeOfRod and PriceExcludingCurrentIterationSizeOfRod

						var PriceIncludingCurrentIterationSizeOfRod = rodPrices[i] + memoOfObj[i, j - i].val;
						var PriceExcludingCurrentIterationSizeOfRod = memoOfObj[i - 1, j].val;
						
						int parent = 0;
						if (PriceExcludingCurrentIterationSizeOfRod > PriceIncludingCurrentIterationSizeOfRod)
						{
							// same logic as above if case
							parent = i - 1;
						}
						memoOfObj[i, j] = new memoCell()
						{
							val = Math.Max(PriceIncludingCurrentIterationSizeOfRod, PriceExcludingCurrentIterationSizeOfRod),
							parent = parent, // otherwise parent is 0
						};
					}
				}
			}

			Console.WriteLine();
			for (var i = 0; i <= maxNumberOfPiecesCanBeCut; i++)
			{
				Console.Write("[ ");
				for (var j = 0; j <= rodLength; j++)
				{
					Console.Write(memoOfObj[i, j].val + "," + memoOfObj[i, j].parent + " ");
				}
				Console.Write("]");
				Console.WriteLine();
			}
			Console.WriteLine();
			Console.WriteLine("Max profit: " + memoOfObj[maxNumberOfPiecesCanBeCut, rodLength].val);

			int a = maxNumberOfPiecesCanBeCut; int b = rodLength;
			int c = rodLength;
			List<int> pieceSizes = new List<int>();

			while (c > 0)
			{
				if (memoOfObj[a, b].parent == 0)
				{
					pieceSizes.Add(a);
					c = c - a;
					//a = a;
					b = b - a;
				}
				else
				{
					a = memoOfObj[a, b].parent;
					//b = b;
				}
			}

			Console.Write("cut the rod in pieces of { ");
			foreach (var p in pieceSizes)
			{
				Console.Write(p + " ");
			}
			Console.Write("}");
			Console.WriteLine(" ");


			pieces = pieceSizes;
			return memoOfObj[maxNumberOfPiecesCanBeCut, rodLength].val;
		}

		public static int getMaxProfitUsingintArray(Dictionary<int, int> rodPrices, int rodLength)
		{

			int maxNumberOfPiecesCanBeCut = 0;
			foreach (var pair in rodPrices)
			{
				if (pair.Key > maxNumberOfPiecesCanBeCut)
				{
					maxNumberOfPiecesCanBeCut = pair.Key;
				}
			}

			int[,] memo = new int[maxNumberOfPiecesCanBeCut + 1, rodLength + 1];

			//populate zeros
			for (var i = 0; i <= maxNumberOfPiecesCanBeCut; i++)
			{
				memo[i, 0] = 0;
			}
			for (var j = 0; j <= rodLength; j++)
			{
				memo[0, j] = 0;
			}

			for (var j = 1; j <= rodLength; j++)
			{
				memo[1, j] = rodPrices[1] * j;
			}

			// i = rod can be cut into pieces of size i
			// j = length of rod for current interation
			for (var i = 2; i <= maxNumberOfPiecesCanBeCut; i++)
			{
				for (var j = 1; j <= rodLength; j++)
				{
					if (j < i) // length of rod 1 cannot be cut into pieces of size 2
					{
						memo[i, j] = memo[i - 1, j];
					}
					else
					{
						var PriceIncludingCurrentIterationSizeOfRod = rodPrices[i] + memo[i, j - i];
						var PriceExcludingCurrentIterationSizeOfRod = memo[i - 1, j];
						memo[i, j] = Math.Max(PriceIncludingCurrentIterationSizeOfRod, PriceExcludingCurrentIterationSizeOfRod);
					}
				}
			}

			for (var i = 0; i <= maxNumberOfPiecesCanBeCut; i++)
			{
				Console.Write("[ ");
				for (var j = 0; j <= rodLength; j++)
				{
					Console.Write(memo[i, j] + " ");
				}
				Console.Write("]");
				Console.WriteLine();
			}

			Console.WriteLine();
			Console.WriteLine("Max profit: " + memo[maxNumberOfPiecesCanBeCut, rodLength]);

			return memo[maxNumberOfPiecesCanBeCut, rodLength];

		}


		static void Main(string[] args)
        {

			//length of piece , price
			Dictionary<int, int> rodPrices = new Dictionary<int, int>();
			
			//rodPrices.Add(1, 2);
			//rodPrices.Add(2, 5);
			//rodPrices.Add(3, 9);
			//rodPrices.Add(4, 6);
			//int rodLength = 5;

			rodPrices.Add(1, 1);
			rodPrices.Add(2, 5);
			rodPrices.Add(3, 8);
			rodPrices.Add(4, 9);
			rodPrices.Add(5, 10);
			rodPrices.Add(6, 17);
			rodPrices.Add(7, 17);
			rodPrices.Add(8, 20);
			int rodLength = 4;

			getMaxProfitUsingObjectArray(rodPrices, rodLength, out List<int> pieces);
			//getMaxProfitUsingintArray(rodPrices, rodLength);

		}

    }
}
