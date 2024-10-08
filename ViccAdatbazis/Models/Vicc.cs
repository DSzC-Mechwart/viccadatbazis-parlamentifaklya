namespace ViccAdatbazis.Models
{
    public class Vicc
    {
        public int Id { get; set; }
        public string Tartalom { get; set; }
        public string Feltolto { get; set; }
        public int Tetszik { get; set; }
        public int NemTetszik { get; set; }
        public bool Aktiv { get; set; }
    }
}
