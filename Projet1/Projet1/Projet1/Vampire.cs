using System;
using System.Security;  // Importa a biblioteca padrão do C# (entrada, saída, etc.)

namespace JogR  // Define o namespace do seu jogo (um agrupamento de código)
{
    class Vampire  // Define a classe principal do jogo, chamada Vampire
    {

        // mapa[x, y] = layer[x + y * largura];

        static char[,] mapa;// Declara uma matriz de caracteres que representa o mapa do jogo
        static char[,] layer;
        static string[] sobreposiçao = { @"_________________", "aaaaaaaaaa", "qqqqq" };
        static int mapa1 = 0;
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

        static int larg = 20;           // Define a largura do mapa
        static int alt = 12;
        static int playerX = 1;            // Posição X inicial do jogador
        static int playerY = 10;            // Posição Y inicial do jogador

        static bool jogando = true;  // Controla se o jogo ainda está rodando



        static void Main()  // Método principal onde o jogo começa
        {
            moveset();
        }
        static void jogar()
        {
            iniciarMapa();



            while (jogando)
            {
                desenhaMapa2();
                var tecla = Console.ReadKey(true).Key;
                atualizarPosicao(tecla);
            }
        }
        static void jogarEstatico()  // Método principal da lógica do jogo
        {
  
            iniciarMapaEstatico();// Inicializa o mapa com paredes e espaço vazio
      
 
            while (jogando)               // Loop principal do jogo: enquanto o jogador estiver jogando
            {


                desenhaMapa();
        
                var tecla = Console.ReadKey(true).Key;  // Espera o jogador pressionar uma tecla (sem mostrar no console)
                atualizarPosicao(tecla);  // Atualiza a posição do jogador com base na tecla pressionada


            }

        }

static void iniciarMapa()
        {
            layer = new char[larg, alt];
            for (int y = 1; y < alt; y++)  // Loop para cada linha do mapa
            {
                for (int x = 2; x < larg; x++)  // Loop para cada coluna do mapa
                {



                    if (y == 2 || y == alt - 1) // Se estiver na borda sul ou norte do mapa (primeira ou última linha/coluna)

                    {
                        layer[x, y] = '-';  // Define como chão
                    }

                    else if (x == 0 || x == larg - 1)
                    {
                        layer[x, y] = '|';  // Define como parede
                    }


                    else
                    {
                        layer[x, y] = ' ';  // Espaço vazio
                    }





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
                            mapa[x, y] = ' ';  // Espaço vazio
                        }





                    }

              

                }


              

         
            mapa[playerX, playerY] = '@';





        

        }
        static void desenhaMapa2()  // Método para desenhar o mapa na tela
        {
            Console.Clear();
            for (int y = 0; y < alt; y++)  // Para cada linha do mapa
            {
                for (int x = 0; x < larg; x++)  // Para cada coluna
                {
                    Console.Write(layer[x, y]);  // Escreve o caractere na tela
                }

                Console.WriteLine();  // Pula para a próxima linha

            }

        }
        static void desenhaMapa()  // Método para desenhar o mapa na tela
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


     
        static void atualizarPosicao(ConsoleKey tecla)  // Método para atualizar posição do jogador
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
            if (mapa[tempX, tempY] == ' ' || layer[tempX,tempY] == ' ' )
            {

                mapa[playerX, playerY] = ' ';     // Apaga a posição antiga do jogador
                mapa[tempX, tempY] = '@';         // Coloca o jogador na nova posição
                layer[tempX, tempY] = ' ';
                layer[tempX, tempY] = '@';

                playerX = tempX;  // Atualiza X real
                playerY = tempY;  // Atualiza Y real


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

            mapaDiferente();
         
         
        
        }
        static void mapaDiferente()
        {
            switch (ativo)
            {
                case 0:

                    mapa1 = 0;
                    break;
                case 1:
                    Console.Write("configuração");
                    break;
                case 2:
                    Console.Write("credito");
                    break;
            }
        }
        
    }
}

