using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dots_and_boxes
{
    class Game
    {
        private int len;
        private int score;
        private int scorePlayer1;
        private int scorePlayer2;
        private bool player ;
        private string[,] m;
        private bool isGameStopped;
        public Game() {
            isGameStopped = false;
            while (isGameStopped == false) {
                initialize();
                play();
            }
        }
        private void initialize() {
            Console.WriteLine("Enter game length 3-12");
            int len = int.Parse(Console.ReadLine());
            while (len > 12 || len < 3)
            {
                Console.WriteLine("invalid length enter between 3 and 12");
                len = int.Parse(Console.ReadLine());
            }
            this.len = len;
            m = new string[len * 2 - 1, len * 2 - 1];
            for (int i = 0, number = 0; i < m.GetLength(0); i += 2)
                for (int r = 0; r < m.GetLength(1); r += 2, number++)
                     m[i, r] = int_to_string(number);
            for (int i = 1; i < m.GetLength(0) - 1; i += 2)
                for (int r = 1; r < m.GetLength(1)-1; r += 2)
                    m[i, r] = " ";
            for(int i=0;i<m.GetLength(0);i++)
                for(int r=0;r<m.GetLength(1);r++)
                    if(m[i,r]==null&&(i!=0||r!=0) )
                        m[i,r]="*";
            isGameStopped = false;
            player = true;
            score = 0;
            scorePlayer1 = 0;
            scorePlayer2 = 0;
        }
        private void print() {
            string len = int_to_string(this.len * this.len - 1);
            for(int i=0;i<m.GetLength(0);i++){
                for (int r = 0; r < m.GetLength(1); r++){
                    for (int j = 0; j < len.Length - m[i, r].Length; j++)
                       Console.Write(' ');
                     Console.Write(m[i, r]);
                }
                Console.WriteLine();
            }
        }
        private string int_to_string(int n) {
            string res="";
            if (n > 9){
                res += int_to_string(n / 10);
                return res+(char)(n % 10 + 48);
            }else
                return "" + (char)(n  + 48);
            return res;
        }
        private void play() {
            print();
            while (this.score < (len - 1) * (len - 1)) {
                if (player)
                    Console.WriteLine("Player 1 play");
                else
                    Console.WriteLine("Player 2 play");
                move();
                int s = score_(player);
                if (s > score) {
                    if (player)
                        scorePlayer1 += s - this.score;
                    else
                        scorePlayer2 += s - this.score;
                    player = !player;
                    this.score = s;
                }
                print();
                player = !player;
            }
            if (scorePlayer1 > scorePlayer2)
                Console.WriteLine("Player 1 win");
            else if(scorePlayer1 < scorePlayer2)
                Console.WriteLine("Player 2 win");
            else
                Console.WriteLine("Draw");
            string playAgain ;
            Console.WriteLine("Do you want to play again enter yes");
            playAgain = Console.ReadLine();
            if (playAgain.Equals("yes") || playAgain.Equals("Yes") || playAgain.Equals("YES"))
                isGameStopped = false;
            else
                isGameStopped = true;
        }
        private void move() {
            int a, b;
            Console.Write("Enter first number ");
            a = int.Parse(Console.ReadLine());
            Console.Write("Enter second number ");
            b = int.Parse(Console.ReadLine());
            while (is_legal_move(a, b) == false) {
                Console.WriteLine("Ilegal Move");
                Console.Write("Enter first number ");
                a = int.Parse(Console.ReadLine());
                Console.Write("Enter second number ");
                b = int.Parse(Console.ReadLine());
            }
            int row1 = (a / this.len)*2;
            int row2 = (b / this.len)*2;
            int col1 = (a % this.len)*2;
            int col2 = (b % this.len)*2;
            if(row1==row2)
                m[row1,(col1+col2)/2]="-";
            else if(col1==col2)
                m[(row1+row2)/2,col1]="|";
        }
        private bool is_legal_move(int a, int b) {
            int row1 = (a / this.len)*2;
            int row2 = (b / this.len)*2;
            int col1 = (a % this.len)*2;
            int col2 = (b % this.len)*2;
            if (col1 != col2 && row1 != row2)
                return false;
            if (a > this.len * this.len - 1 || b > this.len * this.len - 1 || a < 0 || b < 0)
                return false;
            if (row1 > row2 && row1 - 2 != row2 || row2 > row1 && row2 - 2 != row1)
                return false;
            if (col1 > col2 && col1 - 2 != col2 || col2 > col1 && col2 - 2 != col1)
                return false;
            return true;
        }
        private int score_(bool player) {
            int res=0;
            for(int i=0;i<m.GetLength(0)-2;i+=2)
                for(int r=1;r<m.GetLength(1)-1;r+=2)
                    if(m[i,r].Equals("*")==false&&m[i+1,r-1].Equals("*")==false&&m[i+1,r+1].Equals("*")==false&&m[i+2,r].Equals("*")==false){
                        res++;
                        if(m[i+1,r].Equals(" ")){
                            if (player)
                                m[i + 1, r] = "X";
                            else
                                m[i + 1, r] = "O";
                        }
                    }
            return res;
        }
   }
}
