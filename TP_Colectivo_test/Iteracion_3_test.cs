using NUnit.Framework;
using TP_Colectivo;

namespace TP_Colectivo_test
{
    public class Test3
    {
        public Colectivo q;
        public Tarjeta tarjeta;

        [SetUp]
        public void Setup()
        {
            tarjeta = new Tarjeta(1974);
            q = new Colectivo("Q");
        }

        [Test]
        public void Test()
        {
            Assert.Pass();
        }

        [Test]
        public void SaldoDeLaTarjeta()
        {
            tarjeta.setear(35000);
            tarjeta.CargarTarjeta(2000);


            Assert.That(tarjeta.VerSaldo, Is.EqualTo(36000));
            Assert.That(tarjeta.acreditacionPendiente, Is.EqualTo(37000 - 36000));

            q.pagarCon(tarjeta);
            Assert.That(tarjeta.acreditacionPendiente, Is.EqualTo(1000 - 940));

            q.pagarCon(tarjeta);
            Assert.That(tarjeta.acreditacionPendiente, Is.EqualTo(0));

            tarjeta.CargarTarjeta(2000);



            Assert.Pass();
        }
    }
}