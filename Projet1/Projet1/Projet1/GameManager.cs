using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    class GameManager
    {
        private GameManager() { }  // Construtor privado para implementar o padrão Singleton

        static private GameManager instancia;  // Instância única da classe

        public static GameManager Instancia => instancia ??= new GameManager();  // Getter da instância (Singleton)




        public char[,] mapa;  // Matriz que representa o cenário fixo do mapa (paredes, chão, etc.)
        public char[,] obstaculos;  // Matriz auxiliar para armazenar obstáculos antes de aplicar no mapa

        public string[] opcoes = {
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
        public int selecionado = 0;  // Índice da opção selecionada no menu principal

        public string[] SeletorDeMapa = {@"                      
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



        public int ativo = 0;  // Índice do mapa atualmente selecionado

        public int largura = 100;  // Largura do mapa (quantidade de colunas)
        public int altura = 29;  // Altura do mapa (quantidade de linhas)

        public bool jogando = true;  // Indica se o jogo está em execução

        public Personagem personagem;  // Referência ao personagem do jogo

        public List<Fragmento> fragmentos;  // Lista de fragmentos que podem ser coletados no jogo



        public void jogar()
        {
            iniciarMapaEstatico();
            personagem = new Personagem(mapa);  // Inicia personagem com referência ao mapa

            while (personagem.coletados.Count < fragmentos.Count)
            {

                if (Console.KeyAvailable)
                {
                    var tecla = Console.ReadKey(true).Key;
                    personagem.atualizarPosicao(tecla);
                }
                aplicarGravidade();
                desenhaMapa();
                Thread.Sleep(50);

                if (VerificaV("blood"))
                {
                    Console.Write("Você coletou todos os fragmentos necessários para completar o mapa!");  // Mensagem de sucesso se coletou todos os fragmentos
                    break;  // Sai do loop se coletou todos os fragmentos
                }
            }

        }

        public void jogar2()
        {
            iniciarMapaEstatico2();
            personagem = new Personagem(mapa);  // Inicia personagem com referência ao mapa

            while (personagem.coletados.Count < fragmentos.Count)
            {

                if (Console.KeyAvailable)
                {
                    var tecla = Console.ReadKey(true).Key;
                    personagem.atualizarPosicao(tecla);
                }
                aplicarGravidade();

                desenhaMapa();
                Thread.Sleep(60);

                if (VerificaV("blood"))
                {
                    Console.Clear();
                    Console.Write("Você coletou todos os fragmentos necessários para completar o mapa!");  // Mensagem de sucesso se coletou todos os fragmentos

                  
                    enteFase2();
                    jogar();

                    break;




                    // Sai do loop se coletou todos os fragmentos
                }

            }



        }
        public void adicionarObstaculos()  // Adiciona obstáculos para o mapa 1
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

        public bool VerificaV(string certo)
        {
            if (personagem.coletados.Count != certo.Length)  // Verifica se o personagem coletou diferente fragmentos que o necessário
            {
                return false;  // Retorna falso se não tiver coletado todos
            }
            for (int x = 0; x < certo.Length; x++)
            {

                if (certo[x] != personagem.coletados[x].forma)
                {
                    return false;
                }


            }
            return true;
        }
        public void adicionarObstaculos2()  // Adiciona obstáculos para o mapa 2
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

        public void iniciarMapaEstatico()  // Inicializa o cenário fixo do mapa 1
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
            adicionarFragmentos("raig");  // Adiciona fragmentos coletáveis
        }

        private void adicionarFragmentos(string resposta)  // Adiciona fragmentos coletáveis no mapa
        {
            fragmentos = new List<Fragmento>();  // Inicializa a lista de fragmentos

            for (int i = 0; i < resposta.Length; i++)  // Adiciona 5 fragmentos aleatórios
            {

                fragmentos.Add(new Fragmento(resposta[i]));  // Cria novo fragmento com forma 'F'
            }
            Random random = new Random();  // Inicializa gerador de números aleatórios

            for (int i = 0; i < 4; i++)
            {
                int a = random.Next(26);
                char letra = (char)('a' + a);

                fragmentos.Add(new Fragmento(letra));  // Adiciona fragmentos extras com forma 'F'
            }

        }

        public void iniciarMapaEstatico2()  // Inicializa o cenário fixo do mapa 2
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
            adicionarFragmentos("blood");  // Adiciona fragmentos coletáveis
        }

        public void desenhaMapa()  // Renderiza o mapa e o jogador
        {
            Console.SetCursorPosition(0, 0);  // Volta o cursor para o topo esquerdo 
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    bool desenhou = false;


                    // Se for o jogador E não desenhou fragmento, desenha o personagem
                    foreach (var fragmento in fragmentos)  // Desenha todos os fragmentos coletáveis
                    {
                        if (fragmento.x == x && fragmento.y == y)
                        {
                            fragmento.Draw();
                            desenhou = true;
                            break;

                        }

                    }
                    if (!desenhou && x == personagem.playerX && y == personagem.playerY)
                    {
                        personagem.desenharPersonagem();
                        desenhou = true;
                    }

                    // Se ninguém desenhou ainda, desenha o mapa normalmente
                    else if (!desenhou)
                    {
                        Console.Write(mapa[x, y]);
                    }




                }


                Console.WriteLine();  // Pula para a próxima linha
            }

        }

        public void MostrarMenu()  // Exibe o menu principal com arte
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

        public void MostrarMenumap()  // Exibe o menu de seleção de mapa
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

        public void moveset()  // Controle de seleção do menu principal
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

        public void escolhemapa()  // Controle da escolha do mapa
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
                case 0: jogar(); break;  // Inicia com mapa 1
                case 1:
                    jogar2();



                    break;  // Inicia com mapa 2


            }
        }

        public void enteFase2()
        {
           
            ConsoleKey tecla5;
            tecla5 = Console.ReadKey(true).Key;

            while (tecla5 != ConsoleKey.Enter) {        
                
                Console.Clear();
                Console.WriteLine("Você completou o mapa 2! Pressione Enter para continuar.");
                tecla5 = Console.ReadKey(true).Key;
            }

           

           
        }




        public void aplicarGravidade()
        {


            if (personagem.pulando)
            {
                if (personagem.forcaDoPulo > 0)
                {
                    int cima = personagem.playerY - 1;
                    if (cima > 0 && mapa[personagem.playerX, cima] == ' ')
                    {
                        personagem.playerY--;
                        personagem.forcaDoPulo--;
                    }
                    {
                        personagem.pulando = false;
                        personagem.forcaDoPulo = 0;
                    }
                }
                else
                {
                    personagem.pulando = false;
                }
            }
            else
            {
                // Gravidade atuando
                int abaixo = personagem.playerY;


                // Aqui é a correção principal: verifica se o bloco abaixo é espaço
                if (abaixo < altura && mapa[personagem.playerX, abaixo] == ' ')
                {
                    personagem.playerY++;  // Continua caindo
                }
            }

            foreach (var f in GameManager.Instancia.fragmentos.ToList())

                if (f.x == personagem.playerX && f.y == personagem.playerY)
                {
                    GameManager.Instancia.fragmentos.Remove(f);
                    personagem.coletados.Add(f);
                    break;
                }


        }
    }
}