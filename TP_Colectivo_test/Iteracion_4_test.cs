using Newtonsoft.Json;
using NUnit.Framework;
using TP_Colectivo;

namespace TP_Colectivo_test
{
    public class Test4
    {
        private TiempoFalso tiempo;
        private DateTime tiempoahora;
        private Colectivo q;
        private Tarjeta tarjeta;
        private Tarjeta medioBoleto;
        private Tarjeta gratuitoBoleto;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1974);
            medioBoleto = new MedioBoleto(2006);
            gratuitoBoleto = new BoletoGratuito(2077);
            q = new Colectivo("Q");
        }

        [Test]
        public void Franquicia()
        {
            //Es un sabado y cobra normal
            tiempo.AgregarDias(6);

            medioBoleto.setear(2000);
            q.pagarCon(medioBoleto, tiempo);

            Assert.That(medioBoleto.VerSaldo, Is.EqualTo(1060));

            //Es un lunes pero son las antes de las 6
            tiempo.AgregarDias(2);
            tiempo.AgregarMinutos(300);

            medioBoleto.setear(2000);
            q.pagarCon(medioBoleto, tiempo);

            Assert.That(medioBoleto.VerSaldo, Is.EqualTo(1060));

            //Es un lunes pero son las despues de las 10
            tiempo.AgregarMinutos(1080);

            medioBoleto.setear(2000);
            q.pagarCon(medioBoleto, tiempo);

            Assert.That(medioBoleto.VerSaldo, Is.EqualTo(1060));

            Assert.Pass();
        }


        [Test]

        public void usofrecuente129()
        {
            tarjeta.setviajesmes(5);
            tarjeta.CargarTarjeta(3000);
            q.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(3000 - q.precio));
            Assert.That(tarjeta.ViajesMes, Is.EqualTo(6));
        }

        [Test]

        public void usofrecuente3079()
        {

            tarjeta.CargarTarjeta(3000);
            q.pagarCon(tarjeta, tiempo);
            tarjeta.setviajesmes(30);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(3000 - (q.precio * 0.80)));
            Assert.That(tarjeta.ViajesMes, Is.EqualTo(31));
        }

        [Test]
        public void checkeomes()
        {
            tarjeta.setviajesmes(88);
            Assert.That(tarjeta.ViajesMes, Is.EqualTo(88));
        }


        [Test]
        public void usofrecuente80()
        {
            tarjeta.setviajesmes(88);
            tarjeta.CargarTarjeta(3000);
            q.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(3000 - q.precio * 0.75));
            Assert.That(tarjeta.ViajesMes, Is.EqualTo(89));
        }
    
    }
}