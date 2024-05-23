namespace capanna.alessandro._5H.prenota.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Carrello
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
    public int Id { get; set; }

    public int? NumeroDiOggetti { get; set; }

    public string? Username_Utente { get; set; }

    public string? Nome_Prodotto { get; set; }
}