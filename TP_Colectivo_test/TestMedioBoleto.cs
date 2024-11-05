using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Colectivo;

namespace TP_Colectivo_test
{
    internal class TestMedioBoleto
    {
        private TiempoFalso tiempo;
        private DateTime tiempoahora;
        private Colectivo q;


        private Tarjeta tarjeta;
        private Tarjeta medioBoleto;
        private Tarjeta gratuitoBoleto;

        private float tarifa;
        private float medio;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1974);
            medioBoleto = new MedioBoleto(2006);
            gratuitoBoleto = new BoletoGratuito(2077);
            q = new Colectivo("Q");

            tarifa = q.VerTarifa();
            medio = tarifa * 0.5f;

        }

        [Test]
        public void LimitaciónPagoMedioBoletos()
        {
            tiempo.AgregarMinutos(600);
            medioBoleto.setear(4000);
            q.pagarCon(medioBoleto, tiempo);

            tiempo.AgregarMinutos(2);
            q.pagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.VerSaldo, Is.EqualTo(4000 - medio - tarifa));

            tiempo.AgregarMinutos(10);
            q.pagarCon(medioBoleto, tiempo);

            tiempo.AgregarMinutos(10);
            q.pagarCon(medioBoleto, tiempo);

            tiempo.AgregarMinutos(10);
            q.pagarCon(medioBoleto, tiempo);

            //Se terminaron los medio boletos por hoy

            tiempo.AgregarMinutos(10);
            q.pagarCon(medioBoleto, tiempo);

            Assert.That(medioBoleto.VerSaldo, Is.EqualTo(4000 - (medio * 4) - (tarifa * 2)));

            Assert.Pass();
        }

    }
}
