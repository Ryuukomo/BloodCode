using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    public class GamePlay : MonoBehaviour
    {
        static private GamePlay instancia;// Instância única da classe
        private GamePlay()
        {
            Run();
        }  // Construtor privado para implementar o padrão Singleton

        public static GamePlay Instancia => instancia ??= new GamePlay();  // Getter da instância (Singleton)

        public char[,] mapa;  // Matriz que representa o cenário fixo do mapa (paredes, chão, etc.)
        public char[,] obstaculos;  // Matriz auxiliar para armazenar obstáculos antes de aplicar no mapa

        public static int largura = 100;  // Largura do mapa (quantidade de colunas)
        public static int altura = 29;  // Altura do mapa (quantidade de linhas)
        public bool desenhou = false;

        public bool jogando = true;  // Indica se o jogo está em execução

        public List<Fragmento> fragmentos;  // Lista de fragmentos que podem ser coletados no jogo

        public void addObjetos()  // Adiciona obstáculos para o mapa 1
        {
            obstaculos = new char[GamePlay.largura, GamePlay.altura];  // Inicializa matriz de obstáculos
            for (int y = 0; y < GamePlay.altura; y++)
                for (int x = 0; x < GamePlay.largura; x++)
                    obstaculos[x, y] = ' ';  // Inicializa todos os espaços como vazios

            for (int x = 20; x < 30; x++)
                obstaculos[x, 15] = '#';  // Adiciona parede horizontal

            for (int y = 5; y < 10; y++)
                obstaculos[50, y] = '#';  // Adiciona parede vertical

            for (int y = 0; y < GamePlay.altura; y++)  // Aplica obstáculos no mapa original
                for (int x = 0; x < GamePlay.largura; x++)
                    if (obstaculos[x, y] != ' ')
                        mapa[x, y] = obstaculos[x, y];
        }

        public void adicionarFragmentos(string resposta)  // Adiciona fragmentos coletáveis no mapa
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

        public override void Update()
        {
            if (!input) return;
            
            if (GameManager.Instancia.personagem.coletados.Count < fragmentos.Count)
            {
                
                var tecla = Console.ReadKey(true).Key;
                GameManager.Instancia.personagem.atualizarPosicao(tecla);

                if (VerificaV("raig"))
                {
                    Console.Write("Você coletou todos os fragmentos necessários para completar o mapa!");  // Mensagem de sucesso se coletou todos os fragmentos
                   
                }
            }
        }  // Método de atualização do jogo (pode ser usado para lógica de jogo, mas não está implementado aqui)

        public override void Start()
        {
            iniciarMapaEstatico();  // Inicializa o mapa estático
        }

        public void iniciarMapaEstatico()  // Inicializa o cenário fixo do mapa 1
        {  
            mapa = new char[largura, altura];  // Cria nova matriz do mapa
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    if (y == 0 || y == altura - 1)
                        mapa[x, y] = '_';  // Adiciona chão ou teto
                    else if (x == 0 || x == largura - 1)
                        mapa[x, y] = '|';  // Adiciona paredes laterais
                    else

                        mapa[x, y] = ' ';  // Espaço vazio

                }
            }
            addObjetos();  // Insere obstáculos após o preenchimento base
            adicionarFragmentos("raig");  // Adiciona fragmentos coletáveis
        }

        public override void Draw()  // Renderiza o mapa e o jogador
        {
            aplicarGravidade();
            Console.SetCursorPosition(0, 0);  // Volta o cursor para o topo esquerdo 
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                        Console.Write(mapa[x, y]);
                }

                Console.WriteLine();  // Pula para a próxima linha
            }

            /*Console.SetCursorPosition(0, 0);
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    Console.Write(obstaculos[x, y]);
                }

                Console.WriteLine();  // Pula para a próxima linha
            }*/
            

            // Se for o jogador E não desenhou fragmento, desenha o personagem
            foreach (var fragmento in fragmentos)  // Desenha todos os fragmentos coletáveis
            {
                if(mapa[fragmento.x, fragmento.y] == ' ' && obstaculos[fragmento.x, fragmento.y] == ' ')
                {
                    fragmento.Draw();
                }
                else
                {
                    fragmento.x = fragmento.x - 1;
                }
            }

        }

        public bool VerificaV(string certo)
        {
            if (GameManager.Instancia.personagem.coletados.Count != certo.Length)  // Verifica se o personagem coletou diferente fragmentos que o necessário
            {
                return false;  // Retorna falso se não tiver coletado todos
            }
            for (int x = 0; x < certo.Length; x++)
            {

                if (certo[x] != GameManager.Instancia.personagem.coletados[x].forma)
                {
                    return false;
                }


            }
            return true;
        }





        public void aplicarGravidade()
        {
            if (GameManager.Instancia.personagem.pulando)
            {
                if (GameManager.Instancia.personagem.forcaDoPulo > 0)
                {
                    int cima = GameManager.Instancia.personagem.p.Up;
                    if (cima > 0 && GameManager.Instancia.gameplay.mapa[GameManager.Instancia.personagem.p.x, cima] == ' ')
                    {
                        GameManager.Instancia.personagem.p.y--;
                        GameManager.Instancia.personagem.forcaDoPulo--;
                    }
                    {
                        cima = GameManager.Instancia.personagem.p.Down;  // Se não puder pular mais, d
                        GameManager.Instancia.personagem.pulando = false;
                        GameManager.Instancia.personagem.forcaDoPulo = 0;
                    }
                }
                else
                {
                    GameManager.Instancia.personagem.pulando = false;
                }
            }
            else
            {
                // Gravidade atuando
                int abaixo = GameManager.Instancia.personagem.p.y;

                // Aqui é a correção principal: verifica se o bloco abaixo é espaço
                if (abaixo < GamePlay.altura && GameManager.Instancia.gameplay.mapa[GameManager.Instancia.personagem.p.x, abaixo] == ' ')
                {
                    abaixo = GameManager.Instancia.personagem.p.Down;  // Continua caindo
                }
            }

            foreach (var f in fragmentos.ToList())
                if (f.x == GameManager.Instancia.personagem.p.x && f.y == GameManager.Instancia.personagem.p.y)
                {
                    fragmentos.Remove(f);
                    GameManager.Instancia.personagem.coletados.Add(f);
                    break;
                }
        }
    }
}