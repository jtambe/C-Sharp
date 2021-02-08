
// https://www.youtube.com/watch?v=rw4s4M3hFfs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopper_item_budget_problem
{
    public class MinTravelApartment
    {

        public static List<Block> blocks;
        public static List<string> requirements;

        public static List<BlockDistance> blockDistances;

        public class Block
        {
            public bool Gym { get; set; }
            public bool School{ get; set; }
            public bool Office { get; set; }
            public bool Store { get; set; }
        }


        public class BlockDistance
        {
            public int BlockId { get; set; }
            public int Gym { get; set; }
            public int School { get; set; }
            public int Office { get; set; }
            public int Store { get; set; }
            //Max one has to travel to get all places
            public int MaxTravel { get; set; }
        }

        public static int MaxOfValues(int[] arr)
        {
            int maxValue = int.MinValue;
            foreach (var i in arr)
            {
                if (i > maxValue)
                {
                    maxValue = i;
                }
            }

            return maxValue;
        }


        public static int GetMinTravelAptBlock(List<Block> blocks, List<string> requirements)
        {

            blockDistances = new List<BlockDistance>();

            //forward pass
            for (int i = 0; i < blocks.Count; i++)
            {
                var blockDistance = new BlockDistance() 
                { BlockId= i, Gym = int.MaxValue, School = int.MaxValue, Office = int.MaxValue, Store = int.MaxValue, MaxTravel = int.MaxValue };

                if (blocks[i].Gym)
                { 
                    blockDistance.Gym = 0; 
                }
                else if (i > 0 && blockDistances[i - 1].Gym != int.MaxValue)
                {
                    blockDistance.Gym = Math.Min(blockDistances[i-1].Gym + 1, blockDistance.Gym);
                }
                if (blocks[i].School)
                {
                    blockDistance.School = 0;
                }
                else if (i > 0 && blockDistances[i - 1].School != int.MaxValue)
                {
                    blockDistance.School = Math.Min(blockDistances[i - 1].School + 1, blockDistance.School);
                }
                if (blocks[i].Office)
                {
                    blockDistance.Office = 0;
                }
                else if (i > 0 && blockDistances[i - 1].Office != int.MaxValue)
                {
                    blockDistance.Office = Math.Min(blockDistances[i - 1].Office + 1, blockDistance.Office);
                }
                if (blocks[i].Store)
                {
                    blockDistance.Store = 0;
                }
                else if (i > 0 && blockDistances[i - 1].Store != int.MaxValue)
                {
                    blockDistance.Store = Math.Min(blockDistances[i - 1].Store + 1, blockDistance.Store);
                }
                blockDistance.MaxTravel = MaxOfValues(new int[] { blockDistance.Gym, blockDistance.School, blockDistance.Office, blockDistance.Store });
                blockDistances.Add(blockDistance);

            }

            //backward pass
            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                var blockDistance = blockDistances[i];
                if (blocks[i].Gym)
                {
                    blockDistance.Gym = 0;
                }
                else if (i < blocks.Count - 1 && blockDistances[i + 1].Gym != int.MaxValue)
                {
                    blockDistance.Gym = Math.Min(blockDistances[i + 1].Gym + 1, blockDistance.Gym);
                }
                if (blocks[i].School)
                {
                    blockDistance.School = 0;
                }
                else if (i < blocks.Count - 1 && blockDistances[i + 1].School != int.MaxValue)
                {
                    blockDistance.School = Math.Min(blockDistances[i + 1].School + 1, blockDistance.School);
                }
                if (blocks[i].Office)
                {
                    blockDistance.Office = 0;
                }
                else if (i < blocks.Count - 1 && blockDistances[i + 1].Office != int.MaxValue)
                {
                    blockDistance.Office = Math.Min(blockDistances[i + 1].Office + 1, blockDistance.Office);
                }
                if (blocks[i].Store)
                {
                    blockDistance.Store = 0;
                }
                else if (i < blocks.Count - 1 && blockDistances[i + 1].Store != int.MaxValue)
                {
                    blockDistance.Store = Math.Min(blockDistances[i + 1].Store + 1, blockDistance.Store);
                }
                blockDistance.MaxTravel = MaxOfValues(new int[] { blockDistance.Gym, blockDistance.School, blockDistance.Office, blockDistance.Store });

            }


            Array.Sort(blockDistances.Select(b => b.MaxTravel).ToArray());

            return blockDistances[0].BlockId;

        }



        static void Main()
        {



            blocks = new List<Block>
            {
                new Block{ Gym = false, Office = true, School = true, Store = false},
                new Block{ Gym = false, Office = false, School = false, Store = true},
                new Block{ Gym = true, Office = false, School = true, Store = true},
                new Block{ Gym = true, Office = true, School = false, Store = false},
                new Block{ Gym = false, Office = false, School = false, Store = false},
                new Block{ Gym = true, Office = false, School = false, Store = false},
                new Block{ Gym = true, Office = true, School = true, Store = true},
                new Block{ Gym = false, Office = false, School = false, Store = true},
                new Block{ Gym = true, Office = true, School = true, Store = true},
                new Block{ Gym = false, Office = false, School = true, Store = false},
            };

            requirements = new List<string> { "Gym", "Office", "School", "Store" };


            var resultBlock = GetMinTravelAptBlock(blocks, requirements);

            foreach (var block in blockDistances)
            {
                Console.WriteLine("BlockID: " + block.BlockId);
                Console.WriteLine("Block max distance: " + block.MaxTravel);
                Console.WriteLine("Gym: " + block.Gym + " Office: " + block.Office + " School: " + block.School +  " Store: " + block.Store );
            }

            Console.WriteLine("BlockID: " + resultBlock);

            Console.ReadKey();

        }
    }
}
