namespace FineArts.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        //Propiedad no automaticamente implementada
        private string? FirstName_BF;
        public string? FirstName
        {
            get { return FirstName_BF; }
            set { FirstName_BF = value; }
        }
        public string? LastName { get; set; }
        public string? Class { get; set; }
        public string? Email { get; set; }
        public List<Student>? Students { get; set; }

        public Teacher(string firstName, 
            string lastName, string @class, string email, List<Student> students)
        {
            FirstName = firstName;
            LastName = lastName;
            Class = @class;
            Email = email;
            Students = students;
        }
        public Teacher()
        {

        }
    }
}