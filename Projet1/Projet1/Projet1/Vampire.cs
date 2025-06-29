using System;
using System.Security;  // Importa a biblioteca padrão do C# (entrada, saída, etc.)

namespace JogR  // Define o namespace do seu jogo (um agrupamento de código)
{
    class Vampire  // Define a classe principal do jogo, chamada Vampire
    {

        // mapa[x, y] = layer[x + y * largura];

        static char[,] mapa;// Declara uma matriz de caracteres que representa o mapa do jogo

        static char[,] obstaculos;



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

      

        static int largura = 100;           // Define a largura do mapa
        static int altura = 29;            // Define a altura do mapa

        static int playerX = 1;            // Posição X inicial do jogador
        static int playerY = 10;            // Posição Y inicial do jogador

     

        static bool jogando = true;  // Controla se o jogo ainda está rodando
       


        static void Main()  // Método principal onde o jogo começa
        {
            moveset();
        }
        static void jogar()  // Método principal da lógica do jogo
        {

            iniciarMapaEstatico();


            while (jogando)               // Loop principal do jogo: enquanto o jogador estiver jogando
            {


                desenhaMapa();

                var tecla = Console.ReadKey(true).Key;  // Espera o jogador pressionar uma tecla (sem mostrar no console)
                atualizarPosicao(tecla);  // Atualiza a posição do jogador com base na tecla pressionada


            }

        }
        static void jogar2()  // Método principal da lógica do jogo
        {

            iniciarMapaEstatico2();

     while (jogando)               // Loop principal do jogo: enquanto o jogador estiver jogando
            {

           

                desenhaMapa();



                var tecla = Console.ReadKey(true).Key;  // Espera o jogador pressionar uma tecla (sem mostrar no console)
                atualizarPosicao(tecla);  // Atualiza a posição do jogador com base na tecla pressionada


            }

            
       

        }


        static void adicionarObstaculos()
        {
            obstaculos = new char[largura, altura];
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    obstaculos[x, y] = ' '; // vazio por padrão
                }
            }

            // Exemplo de obstáculos
            for (int x = 20; x < 30; x++)
                obstaculos[x, 15] = '#';

            for (int y = 5; y < 10; y++)
                obstaculos[50, y] = '#';

            // Mescla os obstáculos no mapa
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    if (obstaculos[x, y] != ' ')
                        mapa[x, y] = obstaculos[x, y];
                }
            }
        }



        static void adicionarObstaculos2()
        {
            obstaculos = new char[largura, altura];


            for (int y = 0; y < altura; y++)
            {
                for (int x = 0 ; x < largura; x++)
                {
                    obstaculos[x, y] = ' '; // vazio por padrão
                }
            }

            // Exemplo de obstáculos
            for (int x = 1; x < 9; x++) // equivale a distancia de 20 até 22
                obstaculos[x, 10] = '_';

            for (int y = 5; y < 10; y++)
                obstaculos[50, y] = 'a';

            // Mescla os obstáculos no mapa
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    if (obstaculos[x, y] != ' ')
                        mapa[x, y] = obstaculos[x, y];
                }
            }
        }

        static void iniciarMapaEstatico()  // Método para criar e configurar o mapa do jogo
        {



            mapa = new char[largura, altura];  // Cria uma nova matriz do tamanho especificado

          

            for (int y = 0; y < altura; y++)  // Loop para cada linha do mapa
            {
                for (int x = 0; x < largura; x++)  // Loop para cada coluna do mapa
                {


              
                        if (y == 0 || y == altura - 1) // Se estiver na borda sul ou norte do mapa (primeira ou última linha/coluna)

                    {
                        mapa[x, y] = '-';  // Define como chão
                    }

                    else if (x == 0 || x == largura - 1)
                    {
                        mapa[x, y] = '|';  // Define como parede
                    }

                    else
                    { 
                        adicionarObstaculos();
                        mapa[x, y] = ' ';  // Espaço vazio
                    }





                }



            }


    






        }


        static void iniciarMapaEstatico2()  // Método para criar e configurar o mapa do jogo
        {



            mapa = new char[largura, altura];  // Cria uma nova matriz do tamanho especificado



            for (int y = 0; y < altura; y++)  // Loop para cada linha do mapa
            {
                for (int x = 0; x < largura; x++)  // Loop para cada coluna do mapa
                {



                    if (y == 0 || y == altura - 1) // Se estiver na borda sul ou norte do mapa (primeira ou última linha/coluna)

                    {
                        mapa[x, y] = '-';  // Define como chão
                    }

                    else if (x == 0 || x == largura - 1)
                    {
                        mapa[x, y] = '|';  // Define como parede
                    }

                    else
                    {
                        adicionarObstaculos2();
                        mapa[x, y] = ' ';  // Espaço vazio
                    }





                }



            }









        }
        static void desenhaMapa()
        {
            Console.Clear();
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    if (x == playerX && y == playerY)
                        Console.Write('@');  // Desenha o personagem por cima
                    else
                        Console.Write(mapa[x, y]);  // Desenha o cenário original
                }
                Console.WriteLine();
            }
        }




        static void atualizarPosicao(ConsoleKey tecla)
        {
            int tempX = playerX;
            int tempY = playerY;

            switch (tecla)
            {
                case ConsoleKey.A: tempX--; break;
                case ConsoleKey.D: tempX++; break;
                case ConsoleKey.W: tempY--; break;
                case ConsoleKey.S: tempY++; break;
            }

            // Permite mover apenas se o novo local for espaço vazio ou chão
            if (mapa[tempX, tempY] == ' ' || mapa[tempX, tempY] == '_')
            {
                playerX = tempX;
                playerY = tempY;
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
            switch (selecionado)
            {
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
                    ativo = (ativo - 1 + SeletorDeMapa.Length) % SeletorDeMapa.Length;
                }
                else if (tecla4 == ConsoleKey.DownArrow)
                {
                    ativo = (ativo + 1) % SeletorDeMapa.Length;
                }
                MostrarMenumap();
            }
            while (tecla4 != ConsoleKey.Enter);
               Console.Clear();



 
                
                switch (ativo)

                {
                // Inicializa o mapa com paredes e espaço vazio

                    case 0:
                   
                          
                        jogar();
                   
                        break;

                    case 1:


                    jogar2();

                        break;
                }
               

                 



            


        }
    }
}

