using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Colectivo
{
  public class Colectivo{
    public string Linea;
    public float tarifa = 940;
    public float precio; //Esto tiene que estar en el archivo de cada coso
    private Boleto boleto; //No hacer esto, returnearlo directamente

    public Colectivo(string linea){
      this.Linea = linea;
    }

    public Boleto pagarCon(Tarjeta tarjeta, Tiempo tiempo){
      precio = 940;
      string tipo = "Boleto Normal";
      float saldoDisponible = tarjeta.VerSaldo();

      bool tieneSaldoMedioBoleto = saldoDisponible >= (tarjeta.saldo_negativo + (precio * 0.5f));
      bool tieneSaldoBoletoNormal = saldoDisponible >= (tarjeta.saldo_negativo + precio);
      
      if(tarjeta is MedioBoleto && tieneSaldoMedioBoleto){

        var ultimoViaje = tarjeta.historial.LastOrDefault();

        if (ultimoViaje != null && pasaronCincoMinutos(ultimoViaje.fecha,tiempo) && tarjeta.ViajesHoy < 4){
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
      
      else if(tarjeta is BoletoGratuito && tarjeta.ViajesHoy<2){
        precio = 0;
        tipo = "Boleto gratuito";
        tarjeta.ViajesHoy++;  
      }

      else if(tieneSaldoBoletoNormal){
        precio = tarifa;
        tipo = "Boleto normal";
      }

      else{
          Console.WriteLine("No tiene saldo suficiente");
          return null; 
      }

      if(tarjeta.acreditacionPendiente != 0)
        {
            tarjeta.acreditacionPendiente -= precio;

            if(tarjeta.acreditacionPendiente < 0)
            {
                tarjeta.RestarSaldo(-tarjeta.acreditacionPendiente);
                tarjeta.acreditacionPendiente = 0;
            }
        }

      else
        {
                tarjeta.RestarSaldo(precio);
        }

      boleto = new Boleto(tarjeta.id,tipo,precio,Linea, tarjeta.VerSaldo(),tiempo.Now());
      tarjeta.historial.Add(boleto);
      return boleto; 

    }

        private bool pasaronCincoMinutos(DateTime ultimoViaje, Tiempo tiempo){
      if(DiferenciaMinutos(ultimoViaje,tiempo.Now()) > 5)
        {
            return true;
        }

      return false;
    }

        public static int DiferenciaMinutos(DateTime fechaInicio, DateTime fechaFin)
        {
            TimeSpan diferencia = fechaFin - fechaInicio;
            return (int)diferencia.TotalMinutes;
        }


    }
}

