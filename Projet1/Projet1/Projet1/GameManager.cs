using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace JogR
{
    class GameManager : MonoBehaviour
    {
        private GameManager() { }  // Construtor privado para implementar o padrão Singleton

        static private GameManager instancia;  // Instância única da classe

        public static GameManager Instancia => instancia ??= new GameManager();  // Getter da instância (Singleton)


        public string[] comande = { @" 
                     ________________
                    |                |
                    |                |
                    |   \        /   |          
                    |    \  /\  /    | 
                    |     \/  \/     |
                    |                |
    ________________|________________|________________
   |       __       |                |     ______     |
   |      /  \      |     /------    |    |      \    |
   |     /    \     |    /           |    |       \   |
   |    /------\    |    \------\    |    |        |  |         
   |    |      |    |           /    |    |       /   |
   |    |      |    |    ------/     |    |______/    |
   |________________|________________|________________| 
                                                                                                                                                                                                                                                                                                                                      
            ", @"                       



     >>   precionar 'S' move o personagem para baixo 
(precionado, mesmo estando no chão, o personagem descerá)", @"



     >>   precionar 'D' move o personagem para direita", @" 



     >>   precione 'W' para subir com o personagem 
(precionado várias vezes, o personagem subirá cada vez mais)", @"



     >>   precionar 'A' move o personagem para esquerda" };





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
        };



        // Arte em ASCII para o menu principal, com 3 opções visuais
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

        public string[] Conf = { @"   
     ________          ________        __      __     ________      ________         ________         __           ________
    / ______ \        / ______ \      |   \   |  |   |__    __|    |   ____ \       / ______ \       |  |         |   _____|
   / /      \_\      / /      \ \     |    \  |  |      |  |       |  |    \ \     / /      \ \      |  |         |  |
  / /               / /        \ \    |  |\ \ |  |      |  |       |  |____/ /    / /        \ \     |  |         |  |____
 | |               | |          | |   |  | \ \|  |      |  |       |  |___  /    | |          | |    |  |         |   ____|
  \ \        __     \ \        / /    |  |  \    |      |  |       |  |   \ \     \ \        / /     |  |         |  |
   \ \______/ /      \ \______/ /     |  |   \   |      |  |       |  |    \ \     \ \______/ /      |  |____     |  |_____
    \________/        \________/      |__|    \__|      |__|       |__|     \_\     \________/       |_______|    |________|

", @"
                       ________      ________        ________      ________       ________     ________       ________       
                      / ______ \    |   ____ \      |   _____|    |  _____ \     |__    __|   |__    __|     / ______ \     
                     / /      \_\   |  |    \ \     |  |          | |     \ \       |  |         |  |       / /      \ \     
                    / /             |  |____/ /     |  |____      | |      \ \      |  |         |  |      / /        \ \    
                   | |              |  |___  /      |   ____|     | |       | |     |  |         |  |     | |          | |   
                    \ \        __   |  |   \ \      |  |          | |      / /      |  |         |  |      \ \        / /    
                     \ \______/ /   |  |    \ \     |  |_____     | |_____/ /     __|  |__       |  |       \ \______/ /     
                      \________/    |__|     \_\    |________|    |________/     |________|      |__|        \________/   

", " " };

        public int sist = 0;  // Índice do mapa atualmente selecionado



        public override void Awake()
        { }
      


        public void MostrarMenu()  // Exibe o menu principal com arte
        {
            Console.Clear();
            string[] linhass = opcoes[selecionado].Split('\n','\n');




            int top = 20;  // Posição vertical
            int esquerd = 10;  // Posição horizontal
       


  Console.SetCursorPosition(0, 0);
                Console.Write(@"                 
                                 ________________
                                |                |
                                |      /|\       |
                                |     / | \      |
                                |    /  |  \     |
                                |       |        |
                                |       |        |
                ________________|________________|________________
               |                |                |                |
               |      /         |       |        |         \      |
               |     /          |       |        |          \     |
               |    |-------    |    \  |  /     |    -------|    |
               |     \          |     \ | /      |          /     |
               |      \         |      \|/       |         /      |
               |________________|________________|________________|                 
           ".TrimEnd());

     Console.WriteLine(@"
        >> precionar seta '\/' muda para o próximo icone abaixo dos menus
        >> precionar seta '>' ainda não faz nada
        >> precionar seta '/\' muda para o próximo icone acima dos menus 
         (precionado várias vezes, o personagem subirá cada vez mais)
        >> precionar seta '<' move o personagem para esquerda".TrimEnd());
            for (int i = 1; i < linhass.Length; i++)
            {
              

                Console.SetCursorPosition(esquerd, top + i);
                Console.WriteLine(linhass[i].TrimEnd());
            }
          







        }
        public void MostrarMenuConf()

        {
            Console.Clear();
            string[] linhass = Conf[sist].Split('\n');
            int top = 13;
            int esquerd = 10;

            for (int i = 0; i < linhass.Length; i++)
            {
                Console.SetCursorPosition(esquerd, top + i);
                Console.WriteLine(linhass[i].TrimEnd());
            }

        }
        public void MostrarMenumap()  // Exibe o menu de seleção de mapa
        {
            Console.Clear();
            string[] linhas = SeletorDeMapa[ativo].Split('\n');
            int topo = 13;
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
                    selecionado = (selecionado - 1 + opcoes.Length) % opcoes.Length;

                // Sobe
                else if (tecla3 == ConsoleKey.DownArrow)
                    selecionado = (selecionado + 1) % opcoes.Length;  // Desce
                MostrarMenu();

            }
            while (tecla3 != ConsoleKey.Enter);  // Aguarda confirmação

            Console.Clear();
            switch (selecionado)
            {
                case 0: escolhemapa(); break;  // Inicia jogo
                case 1: Configuracao(); break;  // Configurações
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

                switch (tecla4)
                {


                    case ConsoleKey.LeftArrow:

                        moveset(); return;
                }
                if (tecla4 == ConsoleKey.UpArrow)

                {
                    ativo = (ativo - 1 + SeletorDeMapa.Length) % SeletorDeMapa.Length;  // Mapa anterior

                    MostrarMenumap();
                }
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

        public void Configuracao()
        {
            Console.CursorVisible = false;
            Console.Clear();
            MostrarMenuConf();
            ConsoleKey tecla5;
            do
            {
                tecla5 = Console.ReadKey(true).Key;

                switch (tecla5)
                {


                    case ConsoleKey.LeftArrow:

                        moveset(); return;
                }
                if (tecla5 == ConsoleKey.UpArrow)

                {
                    sist = (sist - 1 + Conf.Length) % Conf.Length;  // Mapa anterior

                    MostrarMenuConf();
                }
                if (tecla5 == ConsoleKey.DownArrow)
                {
                    sist = (sist + 1) % Conf.Length;  // Próximo mapa

                    MostrarMenuConf();
                }







            }






            while (tecla5 != ConsoleKey.Enter);  // Confirma seleção

            Console.Clear();



            switch (sist)
            {


                case 0:


                    Tecle(); break;
                case 1:
                    Console.Write("oii");
                    break;  // Inicia com mapa 2


            }
        }
        public void enteFase2()
        {

            ConsoleKey tecla5;
            tecla5 = Console.ReadKey(true).Key;

            while (tecla5 != ConsoleKey.Enter)
            {

                Console.Clear();
                Console.WriteLine("Você completou o mapa 2! Pressione Enter para continuar.");
                tecla5 = Console.ReadKey(true).Key;
            }




        }
        public void Tecle()
        {


            string[] linhas = comande[0].Split('\n');
            string[] linhas2 = comande[1].Split('\n');
            string[] linhas3 = comande[2].Split('\n');
            string[] linhas4 = comande[3].Split('\n');
            string[] linhas5 = comande[4].Split('\n');



            int topo = 0;
            int topo2 = 15;
            int topo3 = 20;
            int topo4 = 24;
            int topo5 = 29;
            int esquerda = 0;




            for (int i = 0; i < linhas.Length; i++)
            {
                Console.SetCursorPosition(esquerda, topo + i);

                Console.WriteLine(linhas[i].TrimEnd());

            }
            for (int i = 0; i < linhas2.Length; i++)
            {
                Console.SetCursorPosition(esquerda, topo2 + i);
                Console.WriteLine(linhas2[i].TrimEnd());
            }
            for (int i = 0; i < linhas3.Length; i++)
            {
                Console.SetCursorPosition(esquerda, topo3 + i);
                Console.WriteLine(linhas3[i].TrimEnd());
            }
            for (int i = 0; i < linhas4.Length; i++)
            {
                Console.SetCursorPosition(esquerda, topo4 + i);
                Console.WriteLine(linhas4[i].TrimEnd());
            }
            for (int i = 0; i < linhas5.Length; i++)
            {
                Console.SetCursorPosition(esquerda, topo5 + i);
                Console.WriteLine(linhas5[i].TrimEnd());
            }

            ConsoleKey tecla5;
            tecla5 = Console.ReadKey(true).Key;

            switch (tecla5)
            {


                case ConsoleKey.LeftArrow:

                    Configuracao(); return;
            }



        }

    }

        
    }

