using System;

namespace TP{
  public class Colectivo{
    public string Linea;
    private float precio = 940;

    public Colectivo(string linea){
      this.Linea = linea;
    }

    public Boleto pagarCon(Tarjeta tarjeta){

      if(tarjeta.VerSaldo() >= (tarjeta.saldo_negativo+precio)){ 
        tarjeta.RestarSaldo(precio);
        return new Boleto(Linea, tarjeta.VerSaldo());
      }
      else{
        Console.WriteLine("No tiene saldo suficiente");
        return null; 
      }  
    }
  }
}