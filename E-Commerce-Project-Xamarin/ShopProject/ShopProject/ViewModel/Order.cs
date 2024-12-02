using System; // Add this using directive

public class Order
{
    public long Id { get; set; }
    public string CommandeId { get; set; }
    public DateTime? DateCommande { get; set; }
    public long? ProductId { get; set; }
    public int? Prix { get; set; }
    public int? Quantite { get; set; }
    public long? UserId { get; set; }
    public string Statue { get; set; }
    public string TypePayment { get; set; }
    public string Adresse { get; set; }
    public Product Product { get; set; } // Assuming you have a Product class
}