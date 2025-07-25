using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  // Importa funcionalidades de controle de tempo

namespace JogR
{
    public abstract class MonoBehaviour
    {
        private Thread t;
        private bool ativo = true;
        public bool visible = false;
        public bool input = false;

        public void Run()
        {
            Awake();
            Start();
            
            t = new Thread(
                () => {
                while (ativo)
                {
                    Update();
                    LateUpdate();
                    Thread.Sleep(800);  // Pausa por 100 milissegundos entre atualizações
                }

                OnDestroy();

                }
            );

            t.Start();
        }

        public virtual void Stop()
        {
            this.ativo = false;
            t.Join();        
        }

        

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }

        public virtual void OnDestroy() { }
        public abstract void Draw();
    }
}
