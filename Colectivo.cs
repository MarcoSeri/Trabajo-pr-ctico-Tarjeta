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
        return new Boleto(tarjeta.id,"Medio boleto",precio,Linea, tarjeta.VerSaldo());
      }
      
      else if(tarjeta is BoletoGratuito){
        tarjeta.RestarSaldo(precio*0);
        return new Boleto(tarjeta.id,"Boleto gratuito",precio,Linea, tarjeta.VerSaldo());
      }

      else{ 
        if(tarjeta.VerSaldo() >= (tarjeta.saldo_negativo+precio)){ 
          tarjeta.RestarSaldo(precio);
          return new Boleto(tarjeta.id,"Boleto normal", precio,Linea, tarjeta.VerSaldo());
        }

        else{
          Console.WriteLine("No tiene saldo suficiente");
          return null; 
        }
      }
    }
  }
}