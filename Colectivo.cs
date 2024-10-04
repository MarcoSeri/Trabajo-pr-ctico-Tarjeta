using System;

namespace TP{
  public class Colectivo{
    public string Linea;
    private float precio = 940;

    public Colectivo(string linea){
      this.Linea = linea;
    }

    public Boleto pagarCon(Tarjeta tarjeta){
      if(tarjeta is MedioBoleto){
        tarjeta.RestarSaldo(precio*0.5f);
      }
      else if(tarjeta is BoletoGratuito){
        tarjeta.RestarSaldo(precio*0);
      }
      else{ 
        if(tarjeta.VerSaldo() >= (tarjeta.saldo_negativo+precio)){ 
          tarjeta.RestarSaldo(precio);
          return new Boleto(Linea, tarjeta.VerSaldo());
        }
        else{
          Console.WriteLine("No tiene saldo suficiente");
          return null; 
        }
      }
      return null;
    }
  }
}