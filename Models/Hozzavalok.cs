namespace ReceptAPI.Models
{
    public class Hozzavalok
    {
        public int id {  get; set; }
        public string IngredientName { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int szakcsId { get; set; }
    }
}
