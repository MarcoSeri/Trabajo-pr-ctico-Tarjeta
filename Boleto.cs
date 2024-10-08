using System;

namespace TP{
  public class Boleto{
    private string Linea;
    private float Saldo;
    private float precio;
    private string tipoDeBoleto;
    private int id;
    private DateTime fecha;


    public Boleto(int id,string tipo, float precio, string linea, float saldo){
      tipoDeBoleto = tipo;
      this.id = id;
      this.precio = precio;
      Linea = linea;
      Saldo = saldo;
    }

    public void MostrarBoleto(){
      Console.WriteLine("Tarifa: " + precio);
      Console.WriteLine("Linea: " + Linea);
      Console.WriteLine("Fecha: " + fecha);
      Console.WriteLine("Saldo Restante: " + Saldo);
      Console.WriteLine("Tipo de Tarjeta: " + tipoDeBoleto);
      Console.WriteLine("Id de la Tarjeta: " + id);
    }
  }  
}