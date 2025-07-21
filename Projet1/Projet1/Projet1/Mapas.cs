using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    public class Mapas
    {public bool desenhou = false;  

        public Personagem personagem;  // Referência ao personagem do jogo
        public void desenhaMapa()  // Renderiza o mapa e o jogador
        {
            Console.SetCursorPosition(0, 0);  // Volta o cursor para o topo esquerdo 
            
            for (int y = 0; y < GamePlay.Instancia.altura; y++)
            {
                for (int x = 0; x < GamePlay.Instancia.largura; x++)
                {
                   

                    // Se for o jogador E não desenhou fragmento, desenha o personagem
                    foreach (var fragmento in GamePlay.Instancia.fragmentos)  // Desenha todos os fragmentos coletáveis
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
                        Console.Write(GamePlay.Instancia.mapa[x, y]);
                    }




                }


                Console.WriteLine();  // Pula para a próxima linha
            }

        }
        public void iniciarMapaEstatico()  // Inicializa o cenário fixo do mapa 1
        {
            GamePlay.Instancia.mapa = new char[GamePlay.Instancia.largura, GamePlay.Instancia.altura];  // Cria nova matriz do mapa
            for (int y = 0; y < GamePlay.Instancia.altura; y++)
                for (int x = 0; x < GamePlay.Instancia.largura; x++)
                {
                    if (y == 0 || y == GamePlay.Instancia.altura - 1)
                        GamePlay.Instancia.mapa[x, y] = '_';  // Adiciona chão ou teto
                    else if (x == 0 || x == GamePlay.Instancia.largura - 1)
                        GamePlay.Instancia.mapa[x, y] = '|';  // Adiciona paredes laterais
                    else
                   
                    GamePlay.Instancia.mapa[x, y] = ' ';  // Espaço vazio
                   
                    Objetos.Instancia.adicionarObstaculos();  // Insere obstáculos após o preenchimento base
                    Objetos.Instancia.adicionarFragmentos("raig");  // Adiciona fragmentos coletáveis
                }
        }
        public void iniciarMapaEstatico2()  // Inicializa o cenário fixo do mapa 2
        {
           GamePlay.Instancia.mapa = new char[GamePlay.Instancia.largura,GamePlay.Instancia.altura];  // Cria nova matriz do mapa
            for (int y = 0; y < GamePlay.Instancia.altura; y++)
                for (int x = 0; x < GamePlay.Instancia.largura; x++)
                {
                    if (y == 0 || y == GamePlay.Instancia.altura - 1)
                       GamePlay.Instancia.mapa[x, y] = '_';  // Chão ou teto
                    else if (x == 0 || x == GamePlay.Instancia.largura - 1)
                        GamePlay.Instancia.mapa[x, y] = '|';  // Paredes
                    else
                        GamePlay.Instancia.mapa[x, y] = ' ';  // Espaço vazio
                }
            Objetos.Instancia.adicionarObstaculos2();  // Insere obstáculos do segundo tipo
            Objetos.Instancia.adicionarFragmentos("blood");  // Adiciona fragmentos coletáveis
        }
    }
}
