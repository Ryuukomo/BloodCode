using System;
using System.Security;  // Importa a biblioteca padrão do C# (entrada, saída, etc.)

namespace JogR  // Define o namespace do seu jogo (um agrupamento de código)
{
    class Vampire  // Define a classe principal do jogo, chamada Vampire
    {
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
                                                       /|
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

        static int largura = 20;           // Define a largura do mapa
        static int altura = 29;            // Define a altura do mapa

        static int playerX = 1;            // Posição X inicial do jogador
        static int playerY = 10;            // Posição Y inicial do jogador
        
        static bool jogando = true;  // Controla se o jogo ainda está rodando
        static bool layout1 = true;
        static bool layout2 = true;

        static void Main()  // Método principal onde o jogo começa
        {
            jogointeiro();// Chama o método que inicia o jogo
        }
        static void jogar()  // Método principal da lógica do jogo
        {
            iniciarMapa();                // Inicializa o mapa com paredes e espaço vazio

            while (jogando)               // Loop principal do jogo: enquanto o jogador estiver jogando
            {
                Console.Clear();          // Limpa a tela a cada frame
                
                
                desenhaMapa();            // Redesenha o mapa com a nova posição do jogador

                var tecla = Console.ReadKey(true).Key;  // Espera o jogador pressionar uma tecla (sem mostrar no console)
                atualizarPosicao(tecla);  // Atualiza a posição do jogador com base na tecla pressionada

            }
        }
        static void iniciarMapa()  // Método para criar e configurar o mapa do jogo
        {
            mapa = new char[largura, altura];  // Cria uma nova matriz do tamanho especificado

            for (int y = 0; y < altura; y++)  // Loop para cada linha do mapa
            {
                for (int x = 0; x < largura; x++)  // Loop para cada coluna do mapa
                {
                    if (y == 0 || y == altura - 1) // Se estiver na borda sul ou norte do mapa (primeira ou última linha/coluna)

                    {
                        mapa[x, y] = '-';  // Define como parede
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

            mapa[playerX, playerY] = '@';  // Coloca o personagem '@' na posição inicial
        }
        static void desenhaMapa()  // Método para desenhar o mapa na tela
        {
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
            if (mapa[tempX, tempY] == ' ')
            {

                mapa[playerX, playerY] = ' ';     // Apaga a posição antiga do jogador
                mapa[tempX, tempY] = '@';         // Coloca o jogador na nova posição

                playerX = tempX;  // Atualiza X real
                playerY = tempY;  // Atualiza Y real


            }

        }
        static void jogointeiro()
        {

            /*
              ________       ________         __            ________       ________  
             / ______ \     |___  ___|       /  \          |   ____ \     |___  ___|
            / /      \_\       |  |         / /\ \         |  |    \ \       |  |
            \ \_______         |  |        / /__\ \        |  |____/ /       |  |
             \_______ \        |  |       / ______ \       |  |___  /        |  |
           __        \ \       |  |      / /      \ \      |  |   \ \        |  |
           \ \_______/ /       |  |     / /        \ \     |  |    \ \       |  |
            \_________/        |__|    /_/          \_\    |__|     \_\      |__|
      */

            moveset();
       

        }
      
        static void atualizetecla2(ConsoleKey tecla2)
        {
            switch (tecla2)  // Verifica qual tecla foi pressionada
            {
                case ConsoleKey.Enter: layout1 = false; break;
            }
        }
        static void dig2()
        {
            var tecla2 = Console.ReadKey(true).Key;  // Espera o jogador pressionar uma tecla (sem mostrar no console)
            atualizetecla2(tecla2);

        }

        static void atualizetecla5(ConsoleKey tecla5)
        {
            switch (tecla5)  // Verifica qual tecla foi pressionada
            {
                case ConsoleKey.Enter: layout2 = false; break;
            }
        }
        static void dig5()
        {
            var tecla5 = Console.ReadKey(true).Key;  // Espera o jogador pressionar uma tecla (sem mostrar no console)
            atualizetecla5(tecla5);

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
      

            if(selecionado == 0) {
                
                
                layout1 = false;

               

            }
 while (layout1)
                {

                    dig2();
                    escolhemapa();
                }
            if(selecionado == 1)
            {
                layout1 = false;
              
            }  while (layout1)
                {

                    dig2();
                    Console.Write("Alô");
                }

            if (selecionado == 2)
            {
                layout1 = false;
                

            }                while (layout1)
                {

                    dig2();
                    Console.Write("Hi");
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
                    selecionado = (ativo - 1 + opcoes.Length) % opcoes.Length;
                }
                else if (tecla4 == ConsoleKey.DownArrow)
                {
                    selecionado = (ativo + 1) % opcoes.Length;
                }
                MostrarMenumap();
            }
            while (tecla4 != ConsoleKey.Enter);
            Console.Clear();

            while (layout2) 
            { 

                if (ativo == 0)
                {


                    layout2 = false;


                }

dig5();
                    jogar();                


                if (ativo == 1)
                {
                    layout2 = false;

 
                }
dig5();

            Console.Write(" oi ");
               

            if (ativo == 2)
            {

                layout2 = false;

 
            }

dig5();
            Console.Write(" olá ");
           
        }
            


        }

    }
}

