using NUnit.Framework;
using TP_Colectivo;

namespace TP_Colectivo_test
{
    public class TestColectivo
    {
        private TiempoFalso tiempo;
        private DateTime tiempoahora;
        private Colectivo q;

        private Tarjeta tarjeta;
        private Tarjeta medioBoleto;
        private Tarjeta gratuitoBoleto;
        private Tarjeta boletoJubilados;


        private float tarifa;
        private float medio;

        [SetUp]
        public void Setup()
        {
            tiempo = new TiempoFalso();
            tarjeta = new Tarjeta(1974);
            medioBoleto = new MedioBoleto(2006);
            gratuitoBoleto = new BoletoGratuito(2077);
            boletoJubilados = new BoletoGratuito(1917);

            q = new Colectivo("Q");

            tarifa = q.VerTarifa();
            medio = tarifa * 0.5f;

        }

        [Test]
        public void LimitaciónPagoMedioBoletos()
        {
            tiempo.AgregarMinutos(600);

            medioBoleto.setear(4000);

            tiempo.AgregarMinutos(10);
            q.pagarCon(medioBoleto, tiempo);

            tiempo.AgregarMinutos(10);
            q.pagarCon(medioBoleto, tiempo);

            tiempo.AgregarMinutos(10);
            q.pagarCon(medioBoleto, tiempo);

            tiempo.AgregarMinutos(10);
            q.pagarCon(medioBoleto, tiempo);

            //Se terminaron los medio boletos por hoy

            tiempo.AgregarMinutos(10);
            q.pagarCon(medioBoleto, tiempo);

            Assert.That(medioBoleto.VerSaldo, Is.EqualTo(4000 - (medio * 4) - (tarifa)));

            Assert.Pass();
        }

        [Test]
        public void CincoMinutosDeDiferencia()
        {
            tiempo.AgregarMinutos(600);

            medioBoleto.setear(4000);
            q.pagarCon(medioBoleto, tiempo);

            tiempo.AgregarMinutos(2);
            q.pagarCon(medioBoleto, tiempo);
            Assert.That(medioBoleto.VerSaldo, Is.EqualTo(4000 - medio - tarifa));

            Assert.Pass();
        }

        [Test]public void usofrecuenteDel_1_Al_29()
        {
            tarjeta.setviajesmes(5);
            tarjeta.CargarTarjeta(3000);
            q.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(3000 - tarifa));
            Assert.That(tarjeta.ViajesMes, Is.EqualTo(6));
        }

        [Test]
        public void usofrecuenteDel_30_Al_79()
        {
            tarjeta.CargarTarjeta(3000);
            q.pagarCon(tarjeta, tiempo);
            tarjeta.setviajesmes(30);
            q.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(3000 - (tarifa * 0.8f) - tarifa));
            Assert.That(tarjeta.ViajesMes, Is.EqualTo(31));
        }

        [Test]
        public void usofrecuente80()
        {
            tarjeta.CargarTarjeta(3000);
            q.pagarCon(tarjeta, tiempo);
            tarjeta.setviajesmes(88);
            q.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(3000 - (tarifa * 0.75f) - tarifa));
            Assert.That(tarjeta.ViajesMes, Is.EqualTo(89));
        }

        [Test]
        public void DiaHabilMedioBoleto()
        {
            //Es un sabado y cobra normal
            tiempo.AgregarDias(6);

            medioBoleto.setear(2000);
            q.pagarCon(medioBoleto, tiempo);
            Assert.Pass();

            Assert.That(medioBoleto.VerSaldo, Is.EqualTo(2000 - tarifa));
        }
        [Test]
        public void HoraHabilMedioBoleto()
        {
            //Es un lunes pero son las antes de las 6
            tiempo.AgregarDias(2);

            medioBoleto.setear(2000);
            q.pagarCon(medioBoleto, tiempo);

            Assert.That(medioBoleto.VerSaldo, Is.EqualTo(2000 - tarifa));

            //Es un lunes pero son las despues de las 10
            tiempo.AgregarMinutos(1380);

            medioBoleto.setear(2000);
            q.pagarCon(medioBoleto, tiempo);

            Assert.That(medioBoleto.VerSaldo, Is.EqualTo(2000 - tarifa));
        }

        [Test]
        public void DiaHabilBoletoGratuito()
        {
            //Es un sabado y cobra normal
            tiempo.AgregarDias(6);

            gratuitoBoleto.setear(2000);
            q.pagarCon(gratuitoBoleto, tiempo);
            Assert.Pass();

            Assert.That(gratuitoBoleto.VerSaldo, Is.EqualTo(2000 - tarifa));
        }
        [Test]
        public void HoraHabilBoletoGratuito()
        {
            //Es un lunes pero son las antes de las 6
            tiempo.AgregarDias(2);

            gratuitoBoleto.setear(2000);
            q.pagarCon(gratuitoBoleto, tiempo);

            Assert.That(gratuitoBoleto.VerSaldo, Is.EqualTo(2000 - tarifa));

            //Es un lunes pero son las despues de las 10
            tiempo.AgregarMinutos(1380);

            gratuitoBoleto.setear(2000);
            q.pagarCon(gratuitoBoleto, tiempo);

            Assert.That(gratuitoBoleto.VerSaldo, Is.EqualTo(2000 - tarifa));
        }

        [Test]
        public void DiaHabilBoletoJubilado()
        {
            //Es un sabado y cobra normal
            tiempo.AgregarDias(6);

            boletoJubilados.setear(2000);
            q.pagarCon(boletoJubilados, tiempo);
            Assert.Pass();

            Assert.That(boletoJubilados.VerSaldo, Is.EqualTo(2000 - tarifa));
        }
        [Test]
        public void HoraHabilBoletoJubilado()
        {
            //Es un lunes pero son las antes de las 6
            tiempo.AgregarDias(2);

            boletoJubilados.setear(2000);
            q.pagarCon(boletoJubilados, tiempo);

            Assert.That(boletoJubilados.VerSaldo, Is.EqualTo(2000 - tarifa));

            //Es un lunes pero son las despues de las 10
            tiempo.AgregarMinutos(1380);

            boletoJubilados.setear(2000);
            q.pagarCon(boletoJubilados, tiempo);

            Assert.That(boletoJubilados.VerSaldo, Is.EqualTo(2000 - tarifa));
        }


    }
}