namespace capanna.alessandro._5H.prenota.Models;
using System.ComponentModel.DataAnnotations;
public class Fishing
{
    public Fishing()
    {
        
    }
    [Key]
    public string? Nome { get; set; }

    public string? Descrizione { get; set; }
    
    public double? Prezzo { get; set; }

    public string? Img{ get; set;}
 
}