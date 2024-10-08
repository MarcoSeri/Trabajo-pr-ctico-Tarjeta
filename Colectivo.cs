using System;
using System.Linq;

namespace TP {
  public class Colectivo {
    public string Linea;
    private float precio = 940;
    private Boleto boleto;

    public Colectivo(string linea) {
      this.Linea = linea;
    }

    public Boleto pagarCon(Tarjeta tarjeta) {
      float saldoDisponible = tarjeta.VerSaldo();
      bool tieneSaldoMB = saldoDisponible >= (tarjeta.saldo_negativo + (precio * 0.5f));
      bool tieneSaldoBN = saldoDisponible >= (tarjeta.saldo_negativo + precio);

      if (tarjeta is MedioBoleto medioBoleto) {
        float precioMedioBoleto = precio * 0.5f;

        // Revisar si hay viajes en el historial y si han pasado 5 minutos desde el último viaje
        var ultimoViaje = medioBoleto.historial.LastOrDefault();
        if (ultimoViaje != null && (DateTime.Now - ultimoViaje.fecha).TotalMinutes < 5) {
          if (tieneSaldoBN) {
            medioBoleto.RestarSaldo(precio);
            boleto = new Boleto(tarjeta.id, "Boleto normal", precio, Linea, tarjeta.VerSaldo());
            medioBoleto.historial.Add(boleto);
            return boleto;
          }
          return null;
        }

        if (medioBoleto.ViajesHoy < 4 && tieneSaldoMB) {
          medioBoleto.RestarSaldo(precioMedioBoleto);
          medioBoleto.ViajesHoy++;
          boleto = new Boleto(tarjeta.id, "Medio boleto", precioMedioBoleto, Linea, tarjeta.VerSaldo());
          medioBoleto.historial.Add(boleto);
          return boleto;
        }

        if (tieneSaldoBN) {
          medioBoleto.RestarSaldo(precio);
          boleto = new Boleto(tarjeta.id, "Boleto normal", precio, Linea, tarjeta.VerSaldo());
          medioBoleto.historial.Add(boleto);
          return boleto;
        }
        return null;
      }

      if (tarjeta is BoletoGratuito) {
        boleto = new Boleto(tarjeta.id, "Boleto gratuito", 0, Linea, tarjeta.VerSaldo());
        tarjeta.historial.Add(boleto);
        return boleto;
      }

      if (tieneSaldoBN) {
        tarjeta.RestarSaldo(precio);
        boleto = new Boleto(tarjeta.id, "Boleto normal", precio, Linea, tarjeta.VerSaldo());
        tarjeta.historial.Add(boleto);
        return boleto;
      }

      Console.WriteLine("No tiene saldo suficiente.");
      return null;
    }
  }
}
