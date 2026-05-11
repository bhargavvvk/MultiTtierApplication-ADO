namespace SimpleNotificationSystem.Models
{
    public class User
    {
        public int Id { get; set; }=0;
        public string Name { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;

        public string PhoneNumber { get; set; }= string.Empty;
         public User()
        {
            //cause we may create user object without passing parameters, so we need to have a parameterless constructor
        }
        public User(int id, string name, string email, string phonenumber)
        {
            Id = id;    
            Name = name;
            Email = email;
            PhoneNumber = phonenumber;
         }
         public override string ToString()
        {
            return $"User Id : {Id}\n" +
                   $"Name : {Name}\n" +
                   $"Email : {Email}\n" +
                   $"Phone Number : {PhoneNumber}";
        }
    }
}
