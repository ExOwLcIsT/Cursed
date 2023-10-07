namespace Cursed.Models
{
    public class Skin
    {
        public int Id { get; set; }
        public string BorderColor { get; set; }
        public string FieldColor { get; set; }
        public string BackgroundColor { get; set; }
        public List<User> Users { get; set; }
    }
}
