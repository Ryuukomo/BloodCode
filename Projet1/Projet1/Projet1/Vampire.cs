using System;  // Importa funcionalidades básicas do sistema
using System.Security;  // (Não está sendo usado neste código) Importa funcionalidades de segurança
using System.Threading;  // Usado para controlar o tempo entre quedas


namespace JogR  // Define o agrupamento do código sob o namespace 'JogR'
{
    class Vampire  // Define a classe principal do jogo
    {
        static char[,] mapa;  // Matriz que representa o cenário fixo do mapa (paredes, chão, etc.)
        static char[,] obstaculos;  // Matriz auxiliar para armazenar obstáculos antes de aplicar no mapa

        static string[] opcoes = {
            @" 
                         ________       ________         __            ________       ________  
                        / ______ \     |__    __|       /  \          |   ____ \     |__    __|
                       / /      \_\       |  |         / /\ \         |  |    \ \       |  |
                       \ \_______         |  |        / /__\ \        |  |____/ /       |  |
                        \_______ \        |  |       / ______ \       |  |___  /        |  |
                      __        \ \       |  |      / /      \ \      |  |   \ \        |  |
                      \ \_______/ /       |  |     / /        \ \     |  |    \ \       |  |
                       \_________/        |__|    /_/          \_\    |__|     \_\      |__| 
            ",
            @" 
                 ________          ________        __      __     ________    ________        ________
                / ______ \        / ______ \      |   \   |  |   |   _____|  |__    __|      / ______ \
               / /      \_\      / /      \ \     |    \  |  |   |  |           |  |        / /      \_\
              / /               / /        \ \    |  |\ \ |  |   |  |____       |  |       / /   _______
             | |               | |          | |   |  | \ \|  |   |   ____|      |  |      | |   |_____  |
              \ \        __     \ \        / /    |  |  \    |   |  |           |  |       \ \        | |
               \ \______/ /      \ \______/ /     |  |   \   |   |  |         __|  |__      \ \______/  |
                \________/        \________/      |__|    \__|   |__|        |________|      \________| |
            ",
            @" 
            ________      ________        ________      ________       ________     ________       ________       
           / ______ \    |   ____ \      |   _____|    |  _____ \     |__    __|   |__    __|     / ______ \     
          / /      \_\   |  |    \ \     |  |          | |     \ \       |  |         |  |       / /      \ \     
         / /             |  |____/ /     |  |____      | |      \ \      |  |         |  |      / /        \ \    
        | |              |  |___  /      |   ____|     | |       | |     |  |         |  |     | |          | |   
         \ \        __   |  |   \ \      |  |          | |      / /      |  |         |  |      \ \        / /    
          \ \______/ /   |  |    \ \     |  |_____     | |_____/ /     __|  |__       |  |       \ \______/ /     
           \________/    |__|     \_\    |________|    |________/     |________|      |__|        \________/      
            "
        };  // Arte em ASCII para o menu principal, com 3 opções visuais
        static int selecionado = 0;  // Índice da opção selecionada no menu principal

        static string[] SeletorDeMapa = {@"                      
                                     ---------------------------------------
                                                       __
                                                      / |
                                                        |
                                                        |
                                                        |
                                                      __|__
                                     _______________________________________
            ",@"                    
                                     ---------------------------------------
                                                       __                                                       
                                                      /  \
                                                         /
                                                        /
                                                       /
                                                      /____
                                     _______________________________________ ",
            @"          
                                     ---------------------------------------
                                                       __
                                                      /  \
                                                         /
                                                        |
                                                         \
                                                      \__/
                                     _______________________________________ "
        };  // Arte em ASCII para seleção visual de mapas
       
        
        static int ativo = 0;  // Índice do mapa atualmente selecionado

        static int largura = 100;  // Largura do mapa (quantidade de colunas)
        static int altura = 29;  // Altura do mapa (quantidade de linhas)

        static int playerX = 1;  // Posição X inicial do jogador (coluna)
        static int playerY = 1;  // Posição Y inicial do jogador (linha)

        static bool jogando = true;  // Indica se o jogo está em execução




        static void Main()  // Ponto de entrada do programa
        {
            moveset();  // Inicia o menu principal
        }

        static void jogar()  // Lógica do jogo para o primeiro mapa
        {
            iniciarMapaEstatico();  // Inicializa o mapa com seus elementos
            while (jogando)  // Laço principal do jogo
            {

                aplicarGravidade();
                desenhaMapa();  // Exibe o mapa atualizado na tela
                var tecla = Console.ReadKey(true).Key;  // Aguarda entrada do jogador sem exibir a tecla
                atualizarPosicao(tecla);  // Atualiza a posição do jogador com base na entrada
            }
        }

        static void jogar2()  // Lógica do jogo para o segundo mapa
        {
            iniciarMapaEstatico2();  // Inicializa o segundo tipo de mapa
            while (jogando)  // Laço principal do jogo
            {
                aplicarGravidade();
                desenhaMapa();  // Exibe o mapa atualizado
                var tecla = Console.ReadKey(true).Key;  // Captura tecla pressionada
                atualizarPosicao(tecla);  // Atualiza a posição do jogador
            }
        }

        static void adicionarObstaculos()  // Adiciona obstáculos para o mapa 1
        {
            obstaculos = new char[largura, altura];  // Inicializa matriz de obstáculos
            for (int y = 0; y < altura; y++)
                for (int x = 0; x < largura; x++)
                    obstaculos[x, y] = ' ';  // Inicializa todos os espaços como vazios

            for (int x = 20; x < 30; x++)
                obstaculos[x, 15] = '#';  // Adiciona parede horizontal

            for (int y = 5; y < 10; y++)
                obstaculos[50, y] = '#';  // Adiciona parede vertical

            for (int y = 0; y < altura; y++)  // Aplica obstáculos no mapa original
                for (int x = 0; x < largura; x++)
                    if (obstaculos[x, y] != ' ')
                        mapa[x, y] = obstaculos[x, y];
        }

        static void adicionarObstaculos2()  // Adiciona obstáculos para o mapa 2
        {
            obstaculos = new char[largura, altura];  // Inicializa matriz de obstáculos
            for (int y = 0; y < altura; y++)
                for (int x = 0; x < largura; x++)
                    obstaculos[x, y] = ' ';  // Inicializa com espaços vazios

            for (int x = 1; x < 9; x++)
                obstaculos[x, 10] = '_';  // Adiciona piso

            for (int y = 5; y < 10; y++)
                obstaculos[50, y] = 'a';  // Adiciona coluna com 'a'

            for (int y = 0; y < altura; y++)  // Aplica os obstáculos no mapa
                for (int x = 0; x < largura; x++)
                    if (obstaculos[x, y] != ' ')
                        mapa[x, y] = obstaculos[x, y];
        }

        static void iniciarMapaEstatico()  // Inicializa o cenário fixo do mapa 1
        {
            mapa = new char[largura, altura];  // Cria nova matriz do mapa
            for (int y = 0; y < altura; y++)
                for (int x = 0; x < largura; x++)
                {
                    if (y == 0 || y == altura - 1)
                        mapa[x, y] = '_';  // Adiciona chão ou teto
                    else if (x == 0 || x == largura - 1)
                        mapa[x, y] = '|';  // Adiciona paredes laterais
                    else
                        mapa[x, y] = ' ';  // Espaço vazio
                }
            adicionarObstaculos();  // Insere obstáculos após o preenchimento base
        }

        static void iniciarMapaEstatico2()  // Inicializa o cenário fixo do mapa 2
        {
            mapa = new char[largura, altura];  // Cria nova matriz do mapa
            for (int y = 0; y < altura; y++)
                for (int x = 0; x < largura; x++)
                {
                    if (y == 0 || y == altura - 1)
                        mapa[x, y] = '_';  // Chão ou teto
                    else if (x == 0 || x == largura - 1)
                        mapa[x, y] = '|';  // Paredes
                    else
                        mapa[x, y] = ' ';  // Espaço vazio
                }
            adicionarObstaculos2();  // Insere obstáculos do segundo tipo
        }

        static void desenhaMapa()  // Renderiza o mapa e o jogador
        {
            Console.SetCursorPosition(0, 0);  // Volta o cursor para o topo esquerdo 
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    if (x == playerX && y == playerY)
                        Console.Write('@');  // Desenha o jogador
                    else
                        Console.Write(mapa[x, y]);  // Desenha o elemento do mapa
                }
                Console.WriteLine();  // Pula para a próxima linha
            }
        }

        static void atualizarPosicao(ConsoleKey tecla)  // Processa movimento do jogador
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

            if (mapa[tempX, tempY] == ' ' || mapa[tempX, tempY] == '_')  // Verifica colisão
            {
                playerX = tempX;  // Atualiza coordenada X
                playerY = tempY;  // Atualiza coordenada Y
            }
        }

        static void MostrarMenu()  // Exibe o menu principal com arte
        {
            Console.Clear();
            string[] linhas = opcoes[selecionado].Split('\n');
            int topo = 5;  // Posição vertical
            int esquerda = 10;  // Posição horizontal
            for (int i = 0; i < linhas.Length; i++)
            {
                Console.SetCursorPosition(esquerda, topo + i);
                Console.WriteLine(linhas[i].TrimEnd());
            }
        }

        static void MostrarMenumap()  // Exibe o menu de seleção de mapa
        {
            Console.Clear();
            string[] linhas = SeletorDeMapa[ativo].Split('\n');
            int topo = 5;
            int esquerda = 10;
            for (int i = 0; i < linhas.Length; i++)
            {
                Console.SetCursorPosition(esquerda, topo + i);
                Console.WriteLine(linhas[i].TrimEnd());
            }
        }

        static void moveset()  // Controle de seleção do menu principal
        {
            Console.CursorVisible = false;
            Console.Clear();
            MostrarMenu();
            ConsoleKey tecla3;
            do
            {
                tecla3 = Console.ReadKey(true).Key;
                if (tecla3 == ConsoleKey.UpArrow)
                    selecionado = (selecionado - 1 + opcoes.Length) % opcoes.Length;  // Sobe
                else if (tecla3 == ConsoleKey.DownArrow)
                    selecionado = (selecionado + 1) % opcoes.Length;  // Desce
                MostrarMenu();
            }
            while (tecla3 != ConsoleKey.Enter);  // Aguarda confirmação

            Console.Clear();
            switch (selecionado)
            {
                case 0: escolhemapa(); break;  // Inicia jogo
                case 1: Console.Write("configuração"); break;  // Configurações
                case 2: Console.Write("credito"); break;  // Créditos
            }
        }

        static void escolhemapa()  // Controle da escolha do mapa
        {
            Console.CursorVisible = false;
            Console.Clear();
            MostrarMenumap();
            ConsoleKey tecla4;
            do
            {
                tecla4 = Console.ReadKey(true).Key;
                if (tecla4 == ConsoleKey.UpArrow)
                    ativo = (ativo - 1 + SeletorDeMapa.Length) % SeletorDeMapa.Length;  // Mapa anterior
                else if (tecla4 == ConsoleKey.DownArrow)
                    ativo = (ativo + 1) % SeletorDeMapa.Length;  // Próximo mapa
                MostrarMenumap();
            }
            while (tecla4 != ConsoleKey.Enter);  // Confirma seleção

            Console.Clear();

            

            switch (ativo)
            {
                case 0:  jogar(); break;  // Inicia com mapa 1
                case 1:  jogar2(); break;  // Inicia com mapa 2
            }
        }

        static void aplicarGravidade()
        {
            while (playerY + 1 < altura - 1 && (mapa[playerX, playerY ] != '_'))
            {
                playerY++;
                desenhaMapa();
                Thread.Sleep(120);  // Tempo entre quedas para simular animação
            }
        }
    }
}


