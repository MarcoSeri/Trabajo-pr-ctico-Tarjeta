using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Colectivo;

namespace TP_Colectivo_test
{
    internal class TestTarjeta
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
            q.pagarCon(tarjeta, tiempo);
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

        [Test]
        public void FuncionaElCredito()
        {
            tarjeta.setear(35000);
            tarjeta.CargarTarjeta(4000);


            Assert.That(tarjeta.VerSaldo, Is.EqualTo(36000));
            Assert.That(tarjeta.acreditacionPendiente, Is.EqualTo(39000 - 36000));

            q.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.acreditacionPendiente, Is.EqualTo(3000 - tarifa));

            q.pagarCon(tarjeta, tiempo);
            q.pagarCon(tarjeta, tiempo);

            Assert.That(tarjeta.acreditacionPendiente, Is.EqualTo(0));

            tarjeta.CargarTarjeta(2000);

            Assert.Pass();
        }

        [Test]
        public void checkeoMes()
        {
            tarjeta.setviajesmes(88);
            Assert.That(tarjeta.ViajesMes, Is.EqualTo(88));
        }
    }
}
