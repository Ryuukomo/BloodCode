using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    public class Personagem
    {



        public static int playerX = 1;  // Posição X inicial do jogador (coluna)
        public static int playerY = 1;  // Posição Y inicial do jogador (linha)

       static char person = '@';

        static char[,] mapa;


        public Personagem(char[,] mapa)
        {
            Personagem.mapa = mapa;  // Atribui o mapa recebido ao campo estático
            
        } // Construtor que recebe o mapa


            public static void atualizarPosicao(ConsoleKey tecla)  // Processa movimento do jogador
        {
            int tempX = playerX;
            int tempY = playerY;
              // Cria nova matriz do mapa
            switch (tecla)
            {
                case ConsoleKey.A: tempX--; break;
                case ConsoleKey.D: tempX++; break;
                case ConsoleKey.W: tempY--; break;
                case ConsoleKey.S: tempY++; break;
            }

            if (mapa[tempX, tempY] == ' ' || mapa[tempX, tempY] == '_')  // Verifica colisão
            {
                playerX = tempX;  // Atualiza coordenada X
                playerY = tempY;  // Atualiza coordenada Y
            }
        }
       public static void desenharPersonagem()
        {
        

                Console.Write(person);
   
                var tecla = Console.ReadKey(true).Key;  // Aguarda entrada do jogador sem exibir a tecla
            atualizarPosicao(tecla);  // Cria instância do personagem
        

        }
    


    }
}
