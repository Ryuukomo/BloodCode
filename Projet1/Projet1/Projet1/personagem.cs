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

      



        public void atualizarPosicao(ConsoleKey tecla)
        {
            int tempX = playerX;
            int tempY = playerY;

            switch (tecla)
            {
                case ConsoleKey.A: tempX--; break;
                case ConsoleKey.D: tempX++; break;
                case ConsoleKey.W:
                    if (!pulando)
                    {
                        pulando = true;
                        forcaDoPulo = 3;
                    }
                    break;

                case ConsoleKey.S: tempY++; break;
            }

            if (mapa[tempX, tempY] == ' ' || mapa[tempX, tempY] == '_')
            {
                playerX = tempX;
                playerY = tempY;
            }

            foreach (Fragmento fragmento in GameManager.Instancia.fragmentos)
            {
                if (fragmento.x == playerX && fragmento.y == playerY)
                {
                    GameManager.Instancia.fragmentos.Remove(fragmento);
                    coletados.Add(fragmento);
                    break;
                }
            }

           
            foreach (var f in GameManager.Instancia.fragmentos.ToList())
               
                if (f.x == playerX && f.y == playerY)
                {
                    GameManager.Instancia.fragmentos.Remove(f);
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