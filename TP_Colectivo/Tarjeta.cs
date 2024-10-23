using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Colectivo
{
    public class Tarjeta
    {
        private float saldo;
        public int id;
        public float saldo_negativo = -480;
        private float limite = 9900;
        private float[] montos_disponibles = { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };
        public List<Boleto> historial = new List<Boleto>();
        public int ViajesHoy;

        public Tarjeta(int id)
        {
            this.id = id;
        }

        public void CargarTarjeta(float monto)
        {
            if (Array.Exists(montos_disponibles, x => x == monto))
            {
                if ((saldo + monto) < limite)
                {
                    if (saldo < 0)
                    {
                        saldo += monto;
                        Console.WriteLine("Saldo adeudado descontado. Saldo Actual: " + saldo);
                    }
                    else 
                    {
                    saldo += monto;
                    Console.WriteLine("Saldo Actual: " + saldo);
                    }
                }
                else
                    Console.WriteLine("El monto excede el limite de la tarjeta");
            }
        }



        public void MostrarSaldo()
        {
            Console.Write("Saldo : " + saldo);
        }

        public float VerSaldo()
        {
            return saldo;
        }
        public float RestarSaldo(float monto){
            return saldo -= monto;
        }
    }
}