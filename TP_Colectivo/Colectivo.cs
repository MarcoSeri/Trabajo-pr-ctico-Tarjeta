using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Colectivo
{
  public class Colectivo{
    public string Linea;
    public float precio;
    private Boleto boleto;

    public Colectivo(string linea){
      this.Linea = linea;
    }

    public Boleto pagarCon(Tarjeta tarjeta){
      precio = 940;
      string tipo = "Boleto Normal";
      float saldoDisponible = tarjeta.VerSaldo();
      bool tieneSaldoMedioBoleto = saldoDisponible >= (tarjeta.saldo_negativo + (precio * 0.5f));
      bool tieneSaldoBoletoNormal = saldoDisponible >= (tarjeta.saldo_negativo + precio);
      
      if(tarjeta is MedioBoleto && tieneSaldoMedioBoleto){
        var ultimoViaje = tarjeta.historial.LastOrDefault();
        if (ultimoViaje != null && pasaronCincoMinutos(ultimoViaje.fecha) && tarjeta.ViajesHoy < 4){
          Console.WriteLine("5 minutos");
          precio *= 0.5f;
          tarjeta.ViajesHoy++;
        }
        else if(ultimoViaje == null){
          Console.WriteLine("ultimo null");
          precio *= 0.5f;
          tarjeta.ViajesHoy++;
        } 
      }
      
      else if(tarjeta is BoletoGratuito && tarjeta.ViajesHoy<=2){
        precio = 0;
        tipo = "Boleto gratuito";
        tarjeta.ViajesHoy++;  
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

    private bool pasaronCincoMinutos(DateTime ultimoViaje){
      if(ultimoViaje.Hour - DateTime.Now.Hour == 0 || ultimoViaje.Minute - DateTime.Now.Minute > 5)
        return true;

      return false;
    }
    
  }
}