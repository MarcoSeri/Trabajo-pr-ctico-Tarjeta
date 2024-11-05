using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Colectivo;

namespace TP_Colectivo_test
{
    public class TestBoleto
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
            tiempo.AgregarMinutos(600);
            tarifa = q.VerTarifa();
            medio = tarifa * 0.5f;
        }

        [Test]
        public void MasDatosSobreBoleto()
        {
            tarjeta.CargarTarjeta(6000);
            q.pagarCon(tarjeta, tiempo);
            tarjeta.historial.Last().MostrarBoleto();

            medioBoleto.CargarTarjeta(6000);
            q.pagarCon(medioBoleto, tiempo);
            medioBoleto.historial.Last().MostrarBoleto();

            gratuitoBoleto.CargarTarjeta(6000);
            q.pagarCon(gratuitoBoleto, tiempo);
            gratuitoBoleto.historial.Last().MostrarBoleto();

            Assert.Pass();
        }

    }
}
