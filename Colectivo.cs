using System;

namespace TP{
  public class Colectivo{
    public string Linea;
    private float precio = 940;
    private Boleto boleto;

    public Colectivo(string linea){
      this.Linea = linea;
    }

    public Boleto pagarCon(Tarjeta tarjeta){

      if(tarjeta is MedioBoleto){
        tarjeta.RestarSaldo(precio*0.5f);
        boleto = new Boleto(tarjeta.id,"Medio boleto",precio,Linea, tarjeta.VerSaldo());
        tarjeta.historial.Add(boleto);
        return boleto;
      }
      
      else if(tarjeta is BoletoGratuito){

        tarjeta.RestarSaldo(precio*0);
        boleto = new Boleto(tarjeta.id,"Boleto gratuito",precio,Linea, tarjeta.VerSaldo());
        tarjeta.historial.Add(boleto);
        return boleto;
      }

      else{ 
        if(tarjeta.VerSaldo() >= (tarjeta.saldo_negativo+precio)){ 
          tarjeta.RestarSaldo(precio);
          boleto = new Boleto(tarjeta.id,"Boleto normal", precio,Linea, tarjeta.VerSaldo());
          tarjeta.historial.Add(boleto);
          return boleto;
        }

        else{
          Console.WriteLine("No tiene saldo suficiente");
          return null; 
        }
      }
    }
  }
}