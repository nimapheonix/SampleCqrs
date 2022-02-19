namespace SampleCqrs.Application.Contract
{
    public sealed class PersonDto
    {
        //
        public string Id { get; set; }
        public string RowVersion { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastModified { get; set; }
        //
        public override bool Equals(object obj)
        {
            var other = obj as PersonDto;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id == other.Id 
                && RowVersion == other.RowVersion 
                && FirstName == other.FirstName 
                && LastName == other.LastName;
        }
    }
}
