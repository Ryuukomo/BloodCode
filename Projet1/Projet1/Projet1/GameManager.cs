using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace JogR
{
    class GameManager : MonoBehaviour
    {
        private GameManager()
        {

            Run();  // Chama o método Run para iniciar o MonoBehaviour, que gerencia o ciclo de vida do jogo    

        }  // Construtor privado para implementar o padrão Singleton

        private static GameManager instancia;  // Instância única da classe

        public static GameManager Instancia => instancia ??= new GameManager();  // Getter da instância (Singleton)


        public GamePlay gameplay;
        public Personagem personagem;
        public Menu menu;


        public override void Update()
        {
            Draw();  // Chama o método de renderização a cada atualização do jogo

        }

        public override void OnDestroy()
        {
            Console.Clear();
            Console.WriteLine("Obrigado por jogar!");
            Console.WriteLine("Pressione qualquer tecla para sair...");
            var tecla = Console.ReadKey(true);
            Environment.Exit(0);
        }

        public override void Start()
        {
            menu = Menu.Instancia;

            menu.visible = true;
            menu.input = true;
        }

        public override void Draw()
        {
            if (menu != null && menu.visible) menu.Draw();  // Verifica se o menu está visível e o renderiza
            if (gameplay != null && gameplay.visible) gameplay.Draw();  // Verifica se o mapa está visível e o renderiza
            if (personagem != null && personagem.visible) personagem.Draw();  // Verifica se o personagem está visível e o renderiza
        }  // Renderiza o mapa e o jogador

    }


}