using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Domain.ValueObject;

public class AirpotAirlinesEndDate
{
 public DateTime Value { get; }
 private AirpotAirlinesEndDate(DateTime value) => Value = value;

 public static AirpotAirlinesEndDate Create(DateTime value)
 {
     if (value == DateTime.MinValue)
         throw new ArgumentException("El campo end_date no puede ser igual a DateTime.MinValue");

     return new AirpotAirlinesEndDate(value);
 }
}
