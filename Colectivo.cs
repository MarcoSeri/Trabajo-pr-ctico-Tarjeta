using System;
using System.Linq;

namespace TP{
  public class Colectivo{
    public string Linea;
    private float precio = 940;
    private Boleto boleto;

    public Colectivo(string linea){
      this.Linea = linea;
    }

    public Boleto pagarCon(Tarjeta tarjeta){
      string tipo = "Boleto Normal";
      float saldoDisponible = tarjeta.VerSaldo();
      bool tieneSaldoMedioBoleto = saldoDisponible >= (tarjeta.saldo_negativo + (precio * 0.5f));
      bool tieneSaldoBoletoNormal = saldoDisponible >= (tarjeta.saldo_negativo + precio);
      
      if(tarjeta is MedioBoleto && tieneSaldoMedioBoleto ){
        var ultimoViaje = tarjeta.historial.LastOrDefault();
        if (ultimoViaje != null && (DateTime.Now - ultimoViaje.fecha).TotalMinutes > 5 && tarjeta.ViajesHoy < 4){
          precio *= 0.5f;
          tarjeta.ViajesHoy++;
        }
        else if(ultimoViaje == null){
          precio *= 0.5f;
          tarjeta.ViajesHoy++;
        } 
      }
      
      else if(tarjeta is BoletoGratuito){
        precio = 0;
        tipo = "Boleto gratuito";
      }

      else if(tieneSaldoBoletoNormal){
        tipo = "Boleto normal";
      }

      else{
          Console.WriteLine("No tiene saldo suficiente");
          return null; 
      }

      tarjeta.RestarSaldo(precio);
      boleto = new Boleto(tarjeta.id,tipo,precio,Linea, tarjeta.VerSaldo());
      tarjeta.historial.Add(boleto);
      return boleto; 
    }
  }
}

/*
tarjeta.RestarSaldo(precio*0);
        boleto = new Boleto(tarjeta.id,"Boleto gratuito",precio,Linea, tarjeta.VerSaldo());
        tarjeta.historial.Add(boleto);
        return boleto;
*/