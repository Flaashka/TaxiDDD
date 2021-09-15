namespace Ddd.Taxi.Domain
{
    public class Client
    {
        public Client(string firstName, string lastName, int clientId = default(int))
        {
            ClientName = new PersonName(firstName, lastName);
            ClientId = clientId;
        }

        public int ClientId { get; }
        public PersonName ClientName { get; }
    }
}