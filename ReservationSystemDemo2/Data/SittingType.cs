namespace ReservationSystem.Data
{
    public class SittingType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ResDuration { get; set; }

        public List<Sitting> Sittings { get; set; } = new List<Sitting>();

    }
}
