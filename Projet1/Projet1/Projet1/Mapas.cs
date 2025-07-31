using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    class Mapas : MonoBehaviour

    {

        private Mapas()

        {
            Run();  // Inicia o menu ao criar a instância
        }
        // Construtor privado para implementar o padrão Singleton

        private static Mapas instancia { get; set; }  // Instância única da classe

        public static Mapas Instancia => instancia ??= new Mapas();  // Getter da instância (Singleton)

        public char[,] mapa;  // Matriz que representa o cenário fixo do mapa (paredes, chão, etc.)


        public int largura = 100;  // Largura do mapa (quantidade de colunas)
        public int altura = 29;  // Altura do mapa (quantidade de linhas)
        public bool desenhou = false;

        public override void Draw()  // Renderiza o mapa e o jogador
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
            Objetos.Instancia.Draw();  // Insere obstáculos após o preenchimento base
            Objetos.Instancia.adicionarFragmentos("raig");  // Adiciona fragmentos coletáveis



        }
        public void iniciarMapaEstatico()  // Inicializa o cenário fixo do mapa 1
        {
                   }
   

        public override void Start()
        {
            iniciarMapaEstatico();  // Inicializa o mapa estático ao iniciar

        }

    }
}