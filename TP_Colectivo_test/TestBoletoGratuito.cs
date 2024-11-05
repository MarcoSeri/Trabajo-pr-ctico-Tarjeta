using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Colectivo;

namespace TP_Colectivo_test
{
    internal class TestBoletoGratuito
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
        public void LimitacionPagoFranquiciasCompletas()
        {
            gratuitoBoleto.setear(4000);

            tiempo.AgregarMinutos(600);

            q.pagarCon(gratuitoBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            q.pagarCon(gratuitoBoleto, tiempo);
            tiempo.AgregarMinutos(10);
            

            //Se terminaron los boletos gratuitos por hoy

            q.pagarCon(gratuitoBoleto, tiempo);

            Assert.That(gratuitoBoleto.VerSaldo, Is.EqualTo(4000 - tarifa));

        }
    }
}
