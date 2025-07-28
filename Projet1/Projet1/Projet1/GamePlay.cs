using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    public class GamePlay : MonoBehaviour
    {

        static private GamePlay instancia;  // Instância única da classe
 private GamePlay()
        {
            Run();

        }  // Construtor privado para implementar o padrão Singleton
        public static GamePlay Instancia => instancia ??= new GamePlay();  // Getter da instância (Singleton)

      

        public bool jogando = true;  // Indica se o jogo está em execução

      

        public List<Fragmento> fragmentos;  // Lista de fragmentos que podem ser coletados no jogo
       
       
        public override void Update()
        {  
            Draw();

            GameManager.Instancia.personagem = new Personagem(GameManager.Instancia.mapa.mapa);  // Inicia personagem com referência ao mapa
            while (GameManager.Instancia.personagem.coletados.Count < fragmentos.Count)
            {

                if (Console.KeyAvailable)
                {
                    var tecla = Console.ReadKey(true).Key;
                    GameManager.Instancia.personagem.atualizarPosicao(tecla);
                }
                aplicarGravidade();

                Thread.Sleep(50);

                if (VerificaV("raig"))
                {
                    Console.Write("Você coletou todos os fragmentos necessários para completar o mapa!");  // Mensagem de sucesso se coletou todos os fragmentos
                    break;  // Sai do loop se coletou todos os fragmentos
                }
            }
        }  // Método de atualização do jogo (pode ser usado para lógica de jogo, mas não está implementado aqui)

        public override void Start()
        {          
          Update();  // Chama o método de atualização para iniciar o jogo     
        }

      

      
        public override void Draw()  // Renderiza o mapa e o jogador
        {
            Console.SetCursorPosition(0, 0);  // Volta o cursor para o topo esquerdo 
            for (int y = 0; y < GameManager.Instancia.mapa.altura; y++)
            {
                for (int x = 0; x < GameManager.Instancia.mapa.largura; x++)
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
                    if (!desenhou && x == GameManager.Instancia.personagem.p.x && y == GameManager.Instancia.personagem.p.y)
                    {
                        GameManager.Instancia.personagem.Draw();
                        desenhou = true;
                    }

                    // Se ninguém desenhou ainda, desenha o mapa normalmente
                    else if (!desenhou)
                    {
                        Console.Write(GameManager.Instancia.mapa.mapa[x, y]);
                    }




                }


                Console.WriteLine();  // Pula para a próxima linha
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
                    if (cima > 0 && GameManager.Instancia.mapa.mapa[GameManager.Instancia.personagem.p.x, cima] == ' ')
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
                if (abaixo < GameManager.Instancia.mapa.altura && GameManager.Instancia.mapa.mapa[GameManager.Instancia.personagem.p.x, abaixo] == ' ')
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
