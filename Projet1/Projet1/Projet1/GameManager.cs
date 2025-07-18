using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
            {      tecla4 = Console.ReadKey(true).Key;
 
 switch (tecla4) {


                        case ConsoleKey.LeftArrow:

                        moveset();return;
                }
                if (tecla4 == ConsoleKey.UpArrow)

                {
                    ativo = (ativo - 1 + SeletorDeMapa.Length) % SeletorDeMapa.Length;  // Mapa anterior

                    MostrarMenumap(); }
                    if (tecla4 == ConsoleKey.DownArrow)
                    {
                        ativo = (ativo + 1) % SeletorDeMapa.Length;  // Próximo mapa

                        MostrarMenumap();
                    }
               

           
                


              
            }






            while (tecla4 != ConsoleKey.Enter);  // Confirma seleção

            Console.Clear();



            switch (ativo)
            {
   
               
                case 0: GamePlay.Instancia.jogar(); break;  // Inicia com mapa 1
                case 1:
                    GamePlay.Instancia.jogar2();



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





    }
}