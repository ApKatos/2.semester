using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualBasic;

namespace ConsoleApp1
{
    class ShortestKingsPath
    {
        public static int[,] board = new int[8, 8];
        public static int[] start = new int[2];
        public static int[] finish = new int[2];
        static Queue<(int, int)> fronta = new Queue<(int, int)>(8*8+1);

        public static void Main(string[] args)
        {
            PrepareBoard();
            fronta.Enqueue((start[0], start[1]));
            if (start[0] == finish[0] && start[1] == finish[1])     //pripad, ze zaciatocna pozicia je na rovnakom mieste ako konecna
            {
                fronta.Dequeue();
                board[finish[0], finish[1]] = 0;
            }
            
            int position_depth;
            while (fronta.Count > 0)
            {
                (int,int) coords = fronta.Dequeue();  //koordinaty rodicovskej pozicie z ktorych hladam suseov a zapisujem ich hlbku (parent+1) do "board"
                position_depth = board[coords.Item1, coords.Item2];
                find_and_add_neighbours(position_depth, coords);
            }

            int num_of_kings_moves = board[finish[0], finish[1]];
            Console.WriteLine(num_of_kings_moves);
            //boardbackwards.print_board();

        }
        static void find_and_add_neighbours(int depth_parent_coords, (int,int) parent_coords)
        {
            (int, int)[] moves = new (int, int)[8] {(1,1), (1, 0), (1, -1), (0, -1), (0, 1), (-1, -1), (-1, 0), (-1, 1)};
            foreach ((int,int) move in moves)
            {
                (int,int) child_coords= (parent_coords.Item1 + move.Item1, parent_coords.Item2 + move.Item2); //novy child coordinat

                if (((0 <= child_coords.Item1 && child_coords.Item1 <= 7) &&
                     (0 <= child_coords.Item2 && child_coords.Item2 <= 7))
                    && (board[child_coords.Item1, child_coords.Item2] == 0 || board[child_coords.Item1, child_coords.Item2]==-1))
                    //dlha kontrola ci su dane suradnice v rozmedzi sachovnice a ci je dane miesto este neohodnotene (0-nevidene, -1 - finalny)
                {
                    board[child_coords.Item1, child_coords.Item2] = depth_parent_coords + 1; //ocislujem poziciu bunky
                    fronta.Enqueue((child_coords.Item1, child_coords.Item2));
                }
            }
        }
        public static void PrepareBoard()
        {
            int vstup1 = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < vstup1; i++)
            {
                //prekazky (oznacene -2)
                string[] prekazka =Console.ReadLine().Trim().Split(' ');
                board[Convert.ToInt32(prekazka[0])-1, Convert.ToInt32(prekazka[1])-1] = -2;
            }
            //start pozicia
            string[] input_start = Console.ReadLine().Trim().Split(' ');
            start[0] = Convert.ToInt32(input_start[0])-1;
            start[1] = Convert.ToInt32(input_start[1])-1;
            //finish pozicia (oznacena -1)
            string[] input_finish = Console.ReadLine().Trim().Split(' ');
            finish[0] = Convert.ToInt32(input_finish[0])-1;
            finish[1] = Convert.ToInt32(input_finish[1])-1;
            board[finish[0], finish[1]] = -1;
        }
    }
}

