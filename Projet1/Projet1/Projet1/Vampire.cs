using System;
using System.Security;  // Importa a biblioteca padrão do C# (entrada, saída, etc.)

namespace JogR  // Define o namespace do seu jogo (um agrupamento de código)
{
    class Vampire  // Define a classe principal do jogo, chamada Vampire
    {

        // mapa[x, y] = layer[x + y * largura];

        static char[,] mapa;// Declara uma matriz de caracteres que representa o mapa do jogo
       
        static string[] opcoes = { @" 
                         ________       ________         __            ________       ________  
                        / ______ \     |__    __|       /  \          |   ____ \     |__    __|
                       / /      \_\       |  |         / /\ \         |  |    \ \       |  |
                       \ \_______         |  |        / /__\ \        |  |____/ /       |  |
                        \_______ \        |  |       / ______ \       |  |___  /        |  |
                      __        \ \       |  |      / /      \ \      |  |   \ \        |  |
                      \ \_______/ /       |  |     / /        \ \     |  |    \ \       |  |
                       \_________/        |__|    /_/          \_\    |__|     \_\      |__| 
 ", @" 
                 ________          ________        __      __     ________    ________        ________
                / ______ \        / ______ \      |   \   |  |   |   _____|  |__    __|      / ______ \
               / /      \_\      / /      \ \     |    \  |  |   |  |           |  |        / /      \_\
              / /               / /        \ \    |  |\ \ |  |   |  |____       |  |       / /   _______
             | |               | |          | |   |  | \ \|  |   |   ____|      |  |      | |   |_____  |
              \ \        __     \ \        / /    |  |  \    |   |  |           |  |       \ \        | |
               \ \______/ /      \ \______/ /     |  |   \   |   |  |         __|  |__      \ \______/  |
                \________/        \________/      |__|    \__|   |__|        |________|      \________| |
                             ", @" 
            ________      ________        ________      ________       ________     ________       ________       
           / ______ \    |   ____ \      |   _____|    |  _____ \     |__    __|   |__    __|     / ______ \     
          / /      \_\   |  |    \ \     |  |          | |     \ \       |  |         |  |       / /      \ \     
         / /             |  |____/ /     |  |____      | |      \ \      |  |         |  |      / /        \ \    
        | |              |  |___  /      |   ____|     | |       | |     |  |         |  |     | |          | |   
         \ \        __   |  |   \ \      |  |          | |      / /      |  |         |  |      \ \        / /    
          \ \______/ /   |  |    \ \     |  |_____     | |_____/ /     __|  |__       |  |       \ \______/ /     
           \________/    |__|     \_\    |________|    |________/     |________|      |__|        \________/      
" };
        static int selecionado = 0;
     

        static string[] SeletorDeMapa = { @"
                                     ---------------------------------------
                                                       __
                                                      / |
                                                        |
                                                        |
                                                        |
                                                      __|__
                                     _______________________________________
                                                      ", @"
                                     ---------------------------------------
                                                       __                                                       
                                                      /  \
                                                         /
                                                        /
                                                       /
                                                      /____
                                     _______________________________________ ", @"
                                     ---------------------------------------
                                                       __
                                                      /  \
                                                         /
                                                        |
                                                         \
                                                      \__/
                                     _______________________________________ " };
        static int ativo = 0;




         static string[,] postes = new string[30, 100];





        static int largura = 100;
        static int altura = 30;
        static int playerX = 1;
        static int playerY = 10;

        //obstaculos
        mapa[playerX, playerY] = '@';

       
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



   
            public void mapa1()
            {






                // Cria uma parede horizontal de obstáculos na linha 5
                for (int x = 0; x < 10; x++)
                {
                    postes[5, x] = "#";
                }

                postes[3, 4] = "p1";
            }




        static bool jogando = true;  // Controla se o jogo ainda está rodando



        static void Main()  // Método principal onde o jogo começa
        {
            moveset();

        }

        static void jogarEstatico()  // Método principal da lógica do jogo
        {
            
            
            
           
    /* mapas certo = new mapas();

            certo.mapa1(); 
           
             MapStatic fase1 = new MapStatic();
        

            
                    fase1.MapaStatic();
    */
       
        
            while (jogando)               // Loop principal do jogo: enquanto o jogador estiver jogando
            {

               /* MapStatic desenhafase1 = new MapStatic();



                desenhafase1.desenhaMapa();
                */




        
                var tecla = Console.ReadKey(true).Key;  // Espera o jogador pressionar uma tecla (sem mostrar no console)

               /* MapStatic atualpos = new MapStatic();


                atualpos.atualizarPosicao(tecla);   Atualiza a posição do jogador com base na tecla pressionada
                    */

            }

        }

       

        

       



     
        static void MostrarMenu()
        {

            Console.Clear();
            string[] linhas = opcoes[selecionado].Split('\n');

            int topo = 5; // ou centralizado
            int esquerda = 10;

            for (int i = 0; i < linhas.Length; i++)
            {
                Console.SetCursorPosition(esquerda, topo + i);
                Console.WriteLine(linhas[i].TrimEnd());
            }
        }
        static void MostrarMenumap()
        {

            Console.Clear();
            string[] linhas = SeletorDeMapa[ativo].Split('\n');

            int topo = 5; // ou centralizado
            int esquerda = 10;

            for (int i = 0; i < linhas.Length; i++)
            {
                Console.SetCursorPosition(esquerda, topo + i);
                Console.WriteLine(linhas[i].TrimEnd());
            }
        }

        
        static void moveset()
        {
            Console.CursorVisible = false;
             
            Console.Clear();
            MostrarMenu();
            ConsoleKey tecla3;

            do
            {
                tecla3 = Console.ReadKey(true).Key;

                if (tecla3 == ConsoleKey.UpArrow)
                {
                    selecionado = (selecionado - 1 + opcoes.Length) % opcoes.Length;
                }
                else if (tecla3 == ConsoleKey.DownArrow)
                {
                    selecionado = (selecionado + 1) % opcoes.Length;
                }
                MostrarMenu();
            }
            while (tecla3 != ConsoleKey.Enter);
            Console.Clear();
            switch (selecionado) {
                case 0:
                    escolhemapa();

                    break;
                case 1: 
                    Console.Write("configuração");
                    break;
                case 2:
                    Console.Write("credito");
                    break;
            }
        }

        static void escolhemapa()
        {

            Console.CursorVisible = false;

            Console.Clear();
            MostrarMenumap();
            ConsoleKey tecla4;

            do
            {


                tecla4 = Console.ReadKey(true).Key;

                if (tecla4 == ConsoleKey.UpArrow)
                {
                    ativo = (ativo - 1 + opcoes.Length) % opcoes.Length;
                   
                    
                }
                else if (tecla4 == ConsoleKey.DownArrow)
                {
                    ativo = (ativo + 1) % opcoes.Length;
                }
                MostrarMenumap();
            }
            while (tecla4 != ConsoleKey.Enter) ;
            Console.Clear();

            jogarEstatico();




        }
       
        
    }
}

