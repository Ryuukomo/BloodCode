using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
    public class Personagem
    {


        public int playerX = 1;
        public int playerY = 1;

        public char person = '@';
        public char[,] mapa;

        public bool pulando = false;
        public int forcaDoPulo = 0;

        public List<Fragmento> coletados;


        public Personagem(char[,] mapa) // Construtor que recebe o mapa
        {
            this.mapa = mapa;
            coletados = new List<Fragmento>();
        }

        Vector2 p = new Vector2(1, 1); // Usado para armazenar a posição do personagem
        public void atualizarPosicao(ConsoleKey tecla)
        {
            int tempX = p.x;
            int tempY = p.y;
            int x = p.x;
            int y = p.y;

            switch (tecla)
            {
                case ConsoleKey.A: x = p.Left ; break;
                case ConsoleKey.D: x = p.Right; break;
                case ConsoleKey.W: y = p.Up; break;
                case ConsoleKey.S: y = p.Down; break;
                    /*  if (!pulando)
                      {
                          pulando = true;
                          forcaDoPulo = 3;
                      }
                      break;*/
            }

            if (mapa[p.x, p.y] == ' ' || mapa[p.x, p.y] == '_')
            {
                p.x = tempX;
                p.y = tempY;
            }

            foreach (Fragmento fragmento in GamePlay.Instancia.fragmentos)
            {
                if (fragmento.x == p.x && fragmento.y == p.y)
                {
                    GamePlay.Instancia.fragmentos.Remove(fragmento);
                    coletados.Add(fragmento);
                    break;
                }
            }


            foreach (var f in GamePlay.Instancia.fragmentos.ToList())

                if (f.x == p.x && f.y == p.y)
                {
                    GamePlay.Instancia.fragmentos.Remove(f);
                    coletados.Add(f);
                    break;
                }


        }

        public void desenharPersonagem()
        {
            Console.Write(person);
        }
    }
}