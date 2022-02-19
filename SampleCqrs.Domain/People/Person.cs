using SampleCqrs.Domain.Common;

namespace SampleCqrs.Domain.People
{
    public class Person : Entity
    {
        //d
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //
        public Person() : base() { }
        public Person(string firstName, string lastName) : this() => (FirstName, LastName) = (firstName, lastName);
        public Person(string id, DateTime lastModified, string rowVersion, string firstName, string lastName) : this(firstName, lastName) => (Id, LastModified, RowVersion) = (id, lastModified, rowVersion);
        public void Update(string firstName, string lastName)
        {  
            this.FirstName = firstName;
            this.LastName = lastName;
            base.Update();
        }
        //
    }
}
