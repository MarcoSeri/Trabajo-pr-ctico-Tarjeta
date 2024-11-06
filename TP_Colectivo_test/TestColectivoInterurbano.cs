using NUnit.Framework;
using TP_Colectivo;

namespace TP_Colectivo_test
{
    public class TestColectivoInterurbano

    {
        private TiempoFalso tiempo;
        private ColectivoInterurbano Swift;

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
            Swift = new ColectivoInterurbano("Expresso Swift");
            

            tarifa = Swift.VerTarifa();
            medio = tarifa * 0.5f;

        }

        [Test]
        public void interurbano()
        {
            tarjeta.CargarTarjeta(3000);
            Swift.pagarCon(tarjeta, tiempo);
            Assert.That(tarjeta.VerSaldo, Is.EqualTo(3000 - tarifa));
        }
    }
}