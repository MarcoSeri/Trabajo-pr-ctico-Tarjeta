using NUnit.Framework;
using TP_Colectivo;

namespace TP_Colectivo_test
{
    public class Test3
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

        [Test]
        public void SaldoDeLaTarjeta()
        {
            tarjeta.setear(35000);
            tarjeta.CargarTarjeta(2000);


            Assert.That(tarjeta.VerSaldo, Is.EqualTo(36000));
            Assert.That(tarjeta.acreditacionPendiente, Is.EqualTo(37000 - 36000));

            q.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.acreditacionPendiente, Is.EqualTo(1000 - tarifa));

            q.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.acreditacionPendiente, Is.EqualTo(0));

            tarjeta.CargarTarjeta(2000);

            Assert.Pass();
        }

        [Test]
        public void LimitaciónPagoMedioBoletos()
        {
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

        [Test]
        public void LimitacionPagoFranquiciasCompletas()
        {
            gratuitoBoleto.setear(4000);

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