using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{

   public class Fragmento
  {
  
        public char forma { get; set; }
        public int x { get; set; }
        public int y { get; set; }
     
        public Fragmento(char forma)
        {
            this.forma = forma;
            Random random = new Random();

            this.x = random.Next(1, GamePlay.Instancia.largura - 1);
            this.y = random.Next(1, GamePlay.Instancia.altura - 1);

        }
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(forma);

        }
   }
}
