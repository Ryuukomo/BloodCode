using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    public class Mapas : MonoBehaviour

    {

        private Mapas()

        {
            Run();  // Inicia o menu ao criar a instância
        }
        // Construtor privado para implementar o padrão Singleton

        static private Mapas instancia { get; set; }  // Instância única da classe

        public static Mapas Instancia => instancia ??= new Mapas();  // Getter da instância (Singleton)

      
        public bool desenhou = false;

        public override void Draw()  // Renderiza o mapa e o jogador
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
                    if (!desenhou && x == GamePlay.Instancia.personagem.p.x && y == GamePlay.Instancia.personagem.p.y)
                    {

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

                    Objetos.Instancia.Draw();  // Insere obstáculos após o preenchimento base
                    Objetos.Instancia.adicionarFragmentos("raig");  // Adiciona fragmentos coletáveis
                }
        }
   

        public override void Start()
        {
            iniciarMapaEstatico();  // Inicializa o mapa estático ao iniciar

        }

    }
}
