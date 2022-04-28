namespace ReservationSystem.Data
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }


        public List<Area> Areas { get; set; } = new List<Area>();

        public List<Person> People { get; set; } = new List<Person>();

        public List<Sitting> Sittings { get; set; } = new List<Sitting>();

        public int DefaultCapacity { get; set; }

    }
}
