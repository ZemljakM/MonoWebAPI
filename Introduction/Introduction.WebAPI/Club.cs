namespace Introduction.WebAPI
{
    public class Club
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Sport { get; set; }

        public DateOnly? DateOfEstablishment { get; set; }

        public int? NumberOfMembers { get; set; }
    }
}
