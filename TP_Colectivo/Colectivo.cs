using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Colectivo
{
  public class Colectivo{
    public string Linea;
    public float tarifa = 1200;
    public float precio; //Esto tiene que estar en el archivo de cada coso

    public Colectivo(string linea){
      this.Linea = linea;
    }

    public Boleto pagarCon(Tarjeta tarjeta, Tiempo tiempo){
      precio = tarifa;
      
      float saldoDisponible = tarjeta.VerSaldo();

      bool tieneSaldoMedioBoleto = saldoDisponible >= (tarjeta.saldo_negativo + (precio * 0.5f));
      bool tieneSaldoBoletoNormal = saldoDisponible >= (tarjeta.saldo_negativo + precio);

      bool tieneSaldo20 = saldoDisponible >= (tarjeta.saldo_negativo + precio * 0.80f);
      bool tieneSaldo25 = saldoDisponible >= (tarjeta.saldo_negativo + precio * 0.75f);

        if(tarjeta is MedioBoleto && tieneSaldoMedioBoleto && estaEnHora(tiempo))
	        {

	        var ultimoViaje = tarjeta.historial.LastOrDefault();

	        if (ultimoViaje != null && pasaronCincoMinutos(ultimoViaje.fecha,tiempo) && tarjeta.ViajesHoy < 4){
		        precio *= 0.5f;
		        tarjeta.ViajesHoy++;
	        }

	        else if(ultimoViaje == null){
		        precio *= 0.5f;
		        tarjeta.ViajesHoy++;
	        } 

            }

        else if(tarjeta is BoletoGratuito && tarjeta.ViajesHoy<2 && estaEnHora(tiempo)){
	        precio = 0;
	        tarjeta.ViajesHoy++;  
        }

        else if (tarjeta.historial.Count != 0)
            {
                if(tarjeta.historial.LastOrDefault().fecha.Month != tiempo.Now().Month || tarjeta.historial.LastOrDefault().fecha.Year != tiempo.Now().Year)
                {
                    tarjeta.ViajesMes = 0;
                }
                if (tarjeta.ViajesMes >= 0 && tarjeta.ViajesMes < 30 && tieneSaldoBoletoNormal)
                {
                    precio = tarifa;
                    tarjeta.ViajesMes++;
                }
                
                else if (tarjeta.ViajesMes > 29 && tarjeta.ViajesMes < 80 && tieneSaldo20)
                {
                    precio *= 0.80f;
                    tarjeta.ViajesMes++;
                }
            
                else if (tarjeta.ViajesMes >= 80 && tieneSaldo25)
                {
                    precio *= 0.75f;
                    tarjeta.ViajesMes++;
                }
                  
                else
                {
                    Console.WriteLine("No tiene saldo suficiente");
                    return null;
                }
            }
        
        else if (tarjeta.historial.Count == 0 && tieneSaldoBoletoNormal)
            {
                precio = tarifa;
                tarjeta.ViajesMes ++;
            }
           
        else
            {
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

      Boleto boleto = new Boleto(tarjeta.id,tarjeta.GetType().Name,precio,Linea, tarjeta.VerSaldo(),tiempo.Now());
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

        private bool estaEnHora(Tiempo tiempo)
        {
            DateTime ahora = tiempo.Now();
            DayOfWeek diaActual = ahora.DayOfWeek;

            if (diaActual >= DayOfWeek.Monday && diaActual <= DayOfWeek.Friday)
            {
                TimeSpan horaActual = ahora.TimeOfDay;
                TimeSpan inicio = new TimeSpan(6, 0, 0);
                TimeSpan fin = new TimeSpan(22, 0, 0);

                return horaActual >= inicio && horaActual <= fin;
            }

            return false;
        }

        public float VerTarifa()
        {
            return tarifa;
        }
    }
}

