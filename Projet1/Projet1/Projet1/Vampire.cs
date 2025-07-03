using System;  // Importa funcionalidades básicas do sistema
using System.Security;  // (Não está sendo usado neste código) Importa funcionalidades de segurança
using System.Threading;  // Usado para controlar o tempo entre quedas


namespace JogR  // Define o agrupamento do código sob o namespace 'JogR'
{
    public class Vampire  // Define a classe principal do jogo
    {
        static void Main()  // Ponto de entrada do programa
        {
            GameManager.Instancia.moveset();  // Inicia o menu principal
        }

    }
}