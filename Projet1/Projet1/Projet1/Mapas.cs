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
            
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                   

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
                    adicionarObstaculos();  // Insere obstáculos após o preenchimento base
                    adicionarFragmentos("raig");  // Adiciona fragmentos coletáveis
                }
        }
        public void iniciarMapaEstatico2()  // Inicializa o cenário fixo do mapa 2
        {
           mapa = new char[largura.altura];  // Cria nova matriz do mapa
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
    }
}
