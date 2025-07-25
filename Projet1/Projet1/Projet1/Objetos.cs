using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    public class Objetos : MonoBehaviour
    {

        private Objetos() { } // Construtor privado para implementar o padrão Singleton

        static private Objetos instancia;  // Instância única da classe

        public static Objetos Instancia => instancia ??= new Objetos();  // Getter da instância (Singleton)

        public char[,] obstaculos;  // Matriz auxiliar para armazenar obstáculos antes de aplicar no mapa

        public override void Draw()  // Adiciona obstáculos para o mapa 1
        {
            obstaculos = new char[GamePlay.Instancia.largura, GamePlay.Instancia.altura];  // Inicializa matriz de obstáculos
            for (int y = 0; y < GamePlay.Instancia.altura; y++)
                for (int x = 0; x < GamePlay.Instancia.largura; x++)
                    obstaculos[x, y] = ' ';  // Inicializa todos os espaços como vazios

            for (int x = 20; x < 30; x++)
                obstaculos[x, 15] = '#';  // Adiciona parede horizontal

            for (int y = 5; y < 10; y++)
                obstaculos[50, y] = '#';  // Adiciona parede vertical

            for (int y = 0; y < GamePlay.Instancia.altura; y++)  // Aplica obstáculos no mapa original
                for (int x = 0; x < GamePlay.Instancia.largura; x++)
                    if (obstaculos[x, y] != ' ')
                        GamePlay.Instancia.mapa[x, y] = obstaculos[x, y];
        }
        public void adicionarObstaculos2()  // Adiciona obstáculos para o mapa 2
        {
            obstaculos = new char[GamePlay.Instancia.largura, GamePlay.Instancia.altura];  // Inicializa matriz de obstáculos
            for (int y = 0; y < GamePlay.Instancia.altura; y++)
                for (int x = 0; x < GamePlay.Instancia.largura; x++)
                    obstaculos[x, y] = ' ';  // Inicializa com espaços vazios

            for (int x = 1; x < 9; x++)
                obstaculos[x, 10] = '_';  // Adiciona piso

            for (int y = 5; y < 10; y++)
                obstaculos[50, y] = '|';  // Adiciona coluna 

            for (int y = 0; y < GamePlay.Instancia.altura; y++)  // Aplica os obstáculos no mapa
                for (int x = 0; x < GamePlay.Instancia.largura; x++)
                    if (obstaculos[x, y] != ' ')
                        GamePlay.Instancia.mapa[x, y] = obstaculos[x, y];
        }
        public void adicionarFragmentos(string resposta)  // Adiciona fragmentos coletáveis no mapa
        {
            GamePlay.Instancia.fragmentos = new List<Fragmento>();  // Inicializa a lista de fragmentos

            for (int i = 0; i < resposta.Length; i++)  // Adiciona 5 fragmentos aleatórios
            {

                GamePlay.Instancia.fragmentos.Add(new Fragmento(resposta[i]));  // Cria novo fragmento com forma 'F'
            }
            Random random = new Random();  // Inicializa gerador de números aleatórios

            for (int i = 0; i < 4; i++)
            {
                int a = random.Next(26);
                char letra = (char)('a' + a);

                GamePlay.Instancia.fragmentos.Add(new Fragmento(letra));  // Adiciona fragmentos extras com forma 'F'
            }

        }

    }
}
