using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Colectivo{
  public class Boleto{
    private string Linea;
    private float Saldo;
    private float precio;
    private string tipoDeBoleto;
    private int id;
    public DateTime fecha;


    public Boleto(int id,string tipo, float precio, string linea, float saldo, DateTime tiempo){
      fecha = tiempo;
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