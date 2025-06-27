using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogR
{




    //obstaculos
   

    public class mapas
    {

 static string[,] postes = new string[30, 100];


        public void mapa1()
        {



       
        

            // Cria uma parede horizontal de obstáculos na linha 5
            for (int x = 0; x< 10; x++)
            {
                postes[5, x] = "#";
            }

    postes[3, 4] = "p1";
        }


}
}
