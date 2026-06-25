using System.Collections.Generic;
using PA1.BL.Animals;

namespace PA1.DAL
{
    // Abstract base class that defines a common interface for data providers dealing with Parrots.
    public abstract class ProviderBase
    {
        //Retrieve all parrots from the database and return them as a list
        public abstract List<Parrot> SelectAll();

        // Retrieves a specific Parrot by ID. Returns 1 if found, 0 if not found
        public abstract int SelectParrot(string ID, out Parrot parrot);

        // Inserts a new Parrot into the data source
        public abstract int Insert(Parrot newParrot);

        // Updates an existing Parrot in the data source
        public abstract int Update(Parrot existingParrot);

        // Deletes a Parrot from the data source by ID
        public abstract int Delete(string ID);

        // Validates the Parrot object to ensure it meets the required criteria
        protected bool ValidateParrot(Parrot parrot)
        {
            // Check if the parrot object is null
            if (string.IsNullOrWhiteSpace(parrot.ID))
                return false;
            // Parrot ID must be unique and not empty
            if (string.IsNullOrWhiteSpace(parrot.Name))
                return false;
            // Parrot Age must be greater than 0
            if (parrot.Age <= 0)
                return false;
            // Parrot FeatherColor must not be empty
            if (string.IsNullOrWhiteSpace(parrot.FeatherColor))
                return false;
            
            return true;
        }
    }
}