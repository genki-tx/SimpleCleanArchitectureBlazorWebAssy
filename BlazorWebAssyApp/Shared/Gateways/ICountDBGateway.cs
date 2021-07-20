namespace SCA
{
    // IGateway
    // Interface for Gateway
    public interface ICountDBGateway
    {
        // Set new count value to database
        void SetCount(CountType type, int new_value);
        // Get value from database
        int GetCount(CountType type);
    }
}