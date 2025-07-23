using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    class GamePlay
    {


        private GamePlay() { }  // Construtor privado para implementar o padrão Singleton

        static private GamePlay instancia;  // Instância única da classe

        public static GamePlay Instancia => instancia ??= new GamePlay();  // Getter da instância (Singleton)



        public char[,] mapa;  // Matriz que representa o cenário fixo do mapa (paredes, chão, etc.)


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


                    GameManager.Instancia.enteFase2();
                    jogar();

                    break;




                    // Sai do loop se coletou todos os fragmentos
                }

            }



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
                    if (!desenhou && x == personagem.p.x && y == personagem.p.y)
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
            Objetos.Instancia.adicionarObstaculos();  // Insere obstáculos após o preenchimento base
            Objetos.Instancia.adicionarFragmentos("raig");  // Adiciona fragmentos coletáveis
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
            Objetos.Instancia.adicionarObstaculos2();  // Insere obstáculos do segundo tipo
            Objetos.Instancia.adicionarFragmentos("blood");  // Adiciona fragmentos coletáveis
        }


        public void aplicarGravidade()
        {


            if (personagem.pulando)
            {
                if (personagem.forcaDoPulo > 0)
                {
                    int cima = personagem.p.Up;
                    if (cima > 0 && mapa[personagem.p.x, cima] == ' ')
                    {
                        personagem.p.y--;
                        personagem.forcaDoPulo--;
                    }
                    {
                        cima = personagem.p.Down;  // Se não puder pular mais, d
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
                int abaixo = personagem.p.y;


                // Aqui é a correção principal: verifica se o bloco abaixo é espaço
                if (abaixo < altura && mapa[personagem.p.x, abaixo] == ' ')
                {
                    abaixo = personagem.p.Down;  // Continua caindo
                }
            }

            foreach (var f in fragmentos.ToList())

                if (f.x == personagem.p.x && f.y == personagem.p.y)
                {
                    fragmentos.Remove(f);
                    personagem.coletados.Add(f);
                    break;
                }


        }






    }
}
