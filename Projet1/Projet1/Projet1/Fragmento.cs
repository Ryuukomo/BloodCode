using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{
<<<<<<< HEAD
   public class Fragmento
  {
=======
    public class Fragmento
    {
>>>>>>> mudança de fase pronta
        public char forma { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public Fragmento(char forma)
        {
            this.forma = forma;
            Random random = new Random();
<<<<<<< HEAD
            this.x = random.Next(1, GameManager.Instancia.largura);
            this.y = random.Next(1, GameManager.Instancia.altura);
=======
            this.x = random.Next(1, GameManager.Instancia.largura - 1);
            this.y = random.Next(1, GameManager.Instancia.altura - 1);
>>>>>>> mudança de fase pronta
        }
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(forma);
<<<<<<< HEAD
        }
            
            
            
=======

        }



>>>>>>> mudança de fase pronta

    }
}
