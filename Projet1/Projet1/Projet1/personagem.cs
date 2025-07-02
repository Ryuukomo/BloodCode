using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    public class Personagem
    {
        public static int playerX = 1;
        public static int playerY = 1;

        static char person = '@';
        static char[,] mapa;

        public static bool pulando = false;
        public static int forcaDoPulo = 0;

        public Personagem(char[,] mapa)
        {
            Personagem.mapa = mapa;
        }

        public static void atualizarPosicao(ConsoleKey tecla)
        {
            int tempX = playerX;
            int tempY = playerY;

            switch (tecla)
            {
                case ConsoleKey.A: tempX--; break;
                case ConsoleKey.D: tempX++; break;
                case ConsoleKey.W:
                    if (!pulando)
                    {
                        pulando = true;
                        forcaDoPulo = 3;
                    }
                    break;

                case ConsoleKey.S: tempY++; break;
            }

            if (mapa[tempX, tempY] == ' ' || mapa[tempX, tempY] == '_')
            {
                playerX = tempX;
                playerY = tempY;
            }
        }

        public static void desenharPersonagem()
        {
            Console.Write(person);
        }
    }
}