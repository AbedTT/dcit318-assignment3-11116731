namespace HealthcareSystem
{
    public class Patient
    {
        public int Id { get; }
        public string Name { get; }
        public int Age { get; }
        public string Gender { get; }
        public string Illness { get; }

        public Patient(int id, string name, int age, string gender, string illness)
        {
            Id = id;
            Name = name;
            Age = age;
            Gender = gender;
            Illness = illness;
        }
    }
}