using NUnit.Framework;
using TP_Colectivo;

namespace TP_Colectivo_test
{
    public class Tests

    {
        private Colectivo q;
        private Tarjeta tarjeta;
        private Tiempo tiempo;
        private float tarifa;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1);
            q = new Colectivo("Q");
            tarifa = q.VerTarifa();
        }

        [Test]
        public void chequeo_saldo()
        {
            tarjeta.CargarTarjeta(2000);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(2000));

            tarjeta.setear(0);

            tarjeta.CargarTarjeta(3000);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(3000));

            tarjeta.setear(0);

            tarjeta.CargarTarjeta(4000);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(4000));

            tarjeta.setear(0);

            tarjeta.CargarTarjeta(5000);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(5000));

            tarjeta.setear(0);

            tarjeta.CargarTarjeta(6000);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(6000));

            tarjeta.setear(0);

            tarjeta.CargarTarjeta(7000);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(7000));

            tarjeta.setear(0);

            tarjeta.CargarTarjeta(8000);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(8000));

            tarjeta.setear(0);

            tarjeta.CargarTarjeta(9000);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(9000));

            Assert.Pass();
        }
        [Test]
        public void chequeo_saldonegativo()
        {
            tarjeta.CargarTarjeta(5000);
            q.pagarCon(tarjeta,tiempo);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(5000 - tarifa));

            tarjeta.setear(-480 + q.VerTarifa());
            q.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(-480));
            tarjeta.CargarTarjeta(7000);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(6520));

            tarjeta.setear(0);
            q.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(0));
        }
    }
}