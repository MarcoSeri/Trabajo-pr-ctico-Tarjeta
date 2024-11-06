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
        private Tarjeta boletoJubilado;


        private float tarifa;
        private float medio;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1974);
            medioBoleto = new MedioBoleto(2006);
            gratuitoBoleto = new BoletoGratuito(2077);
            boletoJubilado = new BoletoGratuito(1917);

            q = new Colectivo("Q");
            tiempo.AgregarMinutos(600);

            tarifa = q.VerTarifa();
            medio = tarifa * 0.5f;
        }

        [Test]
        public void MostrarBoletoNormal()
        {
            tarjeta.CargarTarjeta(6000);
            q.pagarCon(tarjeta, tiempo);
            tarjeta.historial.Last().MostrarBoleto();

            Assert.Pass();
        }

        [Test]
        public void MostrarMedioBoleto()
        {
            medioBoleto.CargarTarjeta(6000);
            q.pagarCon(medioBoleto, tiempo);
            medioBoleto.historial.Last().MostrarBoleto();

            Assert.Pass();
        }

        [Test]
        public void MostrarBoletoGratuito()
        {
            gratuitoBoleto.CargarTarjeta(6000);
            q.pagarCon(gratuitoBoleto, tiempo);
            gratuitoBoleto.historial.Last().MostrarBoleto();

            Assert.Pass();
        }

        [Test]
        public void MostrarBoletoJubilado()
        {
            boletoJubilado.CargarTarjeta(6000);
            q.pagarCon(boletoJubilado, tiempo);
            boletoJubilado.historial.Last().MostrarBoleto();

            Assert.Pass();
        }

    }
}
