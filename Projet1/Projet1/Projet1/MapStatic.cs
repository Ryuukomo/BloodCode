using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    public class MapStatic
    {
        static char[,] mapa;
        static int largura = 100;
        static int altura = 30;
        static int playerX = 1;
        static int playerY = 10;

        //obstaculos

        public void MapaStatic()
        {


            mapa = new char[largura, altura];  // Cria uma nova matriz do tamanho especificado

            for (int y = 0; y < altura; y++)  // Loop para cada linha do mapa
            {
                for (int x = 0; x < largura; x++)  // Loop para cada coluna do mapa
                {

                    if (y == 0 || y == altura - 1) // Se estiver na borda sul ou norte do mapa (primeira ou última linha/coluna
                    {
                        mapa[x, y] = '-';  // Define como chão
                    }
                    else if (x == 0 || x == largura - 1)
                    {
                        mapa[x, y] = '|';  // Define como parede
                    }
                    else
                    {
                        mapa[x, y] = ' ';  // Espaço vazio
                    }

                }

            }
 mapa[playerX, playerY] = '@';
   
    public void desenhaMapa()  // Método para desenhar o mapa na tela
        {
            Console.Clear();
            for (int y = 0; y < altura; y++)  // Para cada linha do mapa
            {
                for (int x = 0; x < largura; x++)  // Para cada coluna
                {
                    Console.Write(mapa[x, y]);  // Escreve o caractere na tela
                }

                Console.WriteLine();  // Pula para a próxima linha

            }

        }



        

  


    





        }



        public void atualizarPosicao(ConsoleKey tecla)  // Método para atualizar posição do jogador
        {
            int tempX = playerX;  // Guarda posição temporária X
            int tempY = playerY;  // Guarda posição temporária Y

            switch (tecla)  // Verifica qual tecla foi pressionada
            {

                case ConsoleKey.A: tempX--; break;  // A = esquerda
                case ConsoleKey.D: tempX++; break;  // D = direita
                case ConsoleKey.W: tempY--; break;  // W = cima
                case ConsoleKey.S: tempY++; break;  // S = baixo

            }

            // Se a nova posição não for parede
            if (mapa[tempX, tempY] == ' ')
            {

                mapa[playerX, playerY] = ' ';     // Apaga a posição antiga do jogador
                mapa[tempX, tempY] = '@';         // Coloca o jogador na nova posição

                playerX = tempX;  // Atualiza X real
                playerY = tempY;  // Atualiza Y real


            }

        }





    }
}
