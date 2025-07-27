using System;

namespace JogR
{
    public class Vampire
    {
        public static void Main()
        {
            GameManager.Instancia.moveset();

            // Loop principal para atualizar menus e gameplay
            while (true)
            {
                if (GameManager.Instancia.menuPrincipal.visible)
                {
                    GameManager.Instancia.menuPrincipal.Draw();
                    GameManager.Instancia.menuPrincipal.Update();
                }
                else if (GameManager.Instancia.menuConf.visible)
                {
                    GameManager.Instancia.menuConf.Draw();
                    GameManager.Instancia.menuConf.Update();
                }
                else if (GameManager.Instancia.menu2.visible)
                {
                    GameManager.Instancia.menu2.Draw();
                    GameManager.Instancia.menu2.Update();
                }
                else if (GameManager.Instancia.personagem != null && GameManager.Instancia.personagem.visible)
                {
                    Mapas.Instancia.Draw();
                    GameManager.Instancia.personagem.Draw();
                    GameManager.Instancia.personagem.Update();
                }
            }
        }
    }
}
