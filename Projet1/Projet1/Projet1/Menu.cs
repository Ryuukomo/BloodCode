using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    public class Menu : MonoBehaviour
    {


        // Construtor privado para implementar o padrão Singleton

        private static Menu instancia { get; set; }  // Instância única da classe
        private Menu()

        {
            Run();  // Inicia o menu ao criar a instância
        }
        public static Menu Instancia => instancia ??= new Menu();  // Getter da instância (Singleton)


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


        public int menu = -1;  // Índice do menu atual (-1: tutorial, 0: principal, 1: mapa, 2: configurações, 3: controles)


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
            "/*,
            @" 
               ________      ________        ________      ________       ________     ________       ________       
              / ______ \    |   ____ \      |   _____|    |  _____ \     |__    __|   |__    __|     / ______ \     
             / /      \_\   |  |    \ \     |  |          | |     \ \       |  |         |  |       / /      \ \     
            / /             |  |____/ /     |  |____      | |      \ \      |  |         |  |      / /        \ \    
           | |              |  |___  /      |   ____|     | |       | |     |  |         |  |     | |          | |   
            \ \        __   |  |   \ \      |  |          | |      / /      |  |         |  |      \ \        / /    
             \ \______/ /   |  |    \ \     |  |_____     | |_____/ /     __|  |__       |  |       \ \______/ /     
              \________/    |__|     \_\    |________|    |________/     |________|      |__|        \________/      
            "*/
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
                                     _______________________________________ "
        };  // Arte em ASCII para seleção visual de mapas

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
        public override void Update()  // Método para iniciar o menu
        {
            if (!input) return;

            var tecla = Console.ReadKey(true).Key;

            switch (tecla)
            {
                case ConsoleKey.LeftArrow:
                    if (menu == 2) menu=0;
                    else menu--;
                    break;
                case ConsoleKey.Enter:
                    switch (menu)
                    {
                        case 0:
                            switch (selecionado)
                            {
                                case 0: menu = 1;  break;// Muda para o menu de seleção de mapa
                                case 1: menu = 2;  break; // Configurações
                            }
                            break;
                        case 1:
                            switch (selecionado)
                            {
                                case 0:
                                    visible = false;  // Torna o menu invisível
                                    input = false;    // Desativa a entrada do menu

                                    GameManager.Instancia.personagem = new Personagem();  // Inicia personagem com referência ao mapa
                                    GameManager.Instancia.personagem.visible = true;  // Torna o personagem visível
                                    GameManager.Instancia.personagem.input = true;    // Torna o personagem visível

                                    GameManager.Instancia.gameplay = GamePlay.Instancia;
                                    GameManager.Instancia.gameplay.visible = true;
                                    GameManager.Instancia.gameplay.input = true;
                                    
                                    break;  // Inicia com mapa 1
                                case 1:
                                    Console.Write("Fase Indefinida");
                                    break;  // Inicia com mapa 2
                            }
                            break;
                        case 2:
                            switch (selecionado)
                            {
                                case 0: menu = 3; break;// Muda para o menu de seleção de mapa
                                case 1: Console.Write("oii"); break; // Configurações
                            }
                            
                            break;
                    }
                    
                    break;
                case ConsoleKey.UpArrow:
                        selecionado = (selecionado - 1 + 2) % opcoes.Length;
                    break;
                // Sobe
                case ConsoleKey.DownArrow:
                        selecionado = (selecionado + 1) % 2;  // Desce
                    break;

                    
            }
            
        }

        public void Tutorial()
        {
            Console.Clear();


            Console.Write(@"                 
                   ________________
                  |                |
                  |      /|\       |                             >> precionar seta '\/' muda para o próximo icone abaixo dos menus
                  |     / | \      |
                  |    /  |  \     |                           
                  |       |        |                             >> precionar seta '>' ainda não faz nada
                  |       |        |                           
  ________________|________________|________________            
 |                |                |                |            >> precionar seta '/\' muda para o próximo icone acima dos menus
 |      /         |       |        |         \      |
 |     /          |       |        |          \     |            
 |    |-------    |    \  |  /     |    -------|    |            >> precionar seta '<' move o personagem para esquerda
 |     \          |     \ | /      |          /     |
 |      \         |      \|/       |         /      |
 |________________|________________|________________|                 
           ");

            Thread.Sleep(2000);  // Pausa para o usuário ler a arte
            menu = 0;  // Muda para o menu principal

        }

        public void MostrarMenu()  // Exibe o menu principal com arte
        {
            Console.Clear();
            string[] linhass = opcoes[selecionado].Split('\n');

            int top = 20;  // Posição vertical
            int esquerd = 10;  // Posição horizontal

            for (int i = 0; i < linhass.Length; i++)
            {
                Console.SetCursorPosition(esquerd, top + i);
                Console.WriteLine(linhass[i].TrimEnd());
            }
        }
        public void MostrarMenuConf()
        {
            Console.Clear();
            string[] linhass = Conf[selecionado].Split('\n');
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
            string[] linhas = SeletorDeMapa[selecionado].Split('\n');
            int topo = 13;
            int esquerda = 10;
            for (int i = 0; i < linhas.Length; i++)
            {
                Console.SetCursorPosition(esquerda, topo + i);
                Console.WriteLine(linhas[i].TrimEnd());
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

        public void MostrarMenuControles()
        {
            Console.Clear();
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
        }

        public override void Draw()
        {
            switch (menu)
            {
                case -1:
                    Tutorial();  // Exibe o tutorial
                    break;
                case 0:
                    MostrarMenu();
                    break;
                case 1:
                    MostrarMenumap();
                    break;
                case 2:
                    MostrarMenuConf();
                    break;
                case 3:
                    MostrarMenuControles();
                    break;
            }
        }




    }
}