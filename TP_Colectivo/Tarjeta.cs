using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Colectivo
{
    public class Tarjeta
    {
        public float saldo_negativo = -480;
        public List<Boleto> historial = new List<Boleto>();
        private float[] montos_disponibles = { 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };
        private float limite = 36000;

        public int id;
        public int ViajesMes;
        public int ViajesHoy;
        public float acreditacionPendiente;
        private float saldo;

        public Tarjeta(int id)
        {
            this.id = id;
        }

        public void setear(float saldo2)
        {
            saldo = saldo2;
        }

        public void CargarTarjeta(float monto)
        {
            if (Array.Exists(montos_disponibles, x => x == monto))
            {
                if ((saldo + monto) < limite)
                {
                    saldo += monto;
                }

                else
                {
                    acreditacionPendiente = (saldo + monto) - limite;
                    saldo = limite;
                }
            }
        }
        public float RestarSaldo(float monto){
            return saldo -= monto;
        }

        public float RestarPendiente(float monto)
        {
            return saldo -= monto;
        }

        public int viajemes()
        {
            return ViajesMes;
        }

        public float VerSaldo()
        {
            return saldo;
        }
        public void setviajesmes(int num)
        {
            ViajesMes = num;
        }
    }
}