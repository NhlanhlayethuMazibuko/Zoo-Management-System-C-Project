using System;
using System.Collections.Generic;
using PA1.BL.Animals;
using PA1.DAL;

namespace PA1.BL
{
  
    public class ParrotBL : IDisposable
    {
        //Field to store the data provider instance
        private readonly ProviderBase _provider;

        public ParrotBL(string providerType)
        {
            _provider = SetupProviderBase(providerType);
        }

        ///Method to retrieve all parrots from the data source
        public List<Parrot> SelectAll()
        {
            return _provider.SelectAll();
        }

        //Method to retrieve a specific parrot by ID
        public int SelectParrot(string id, out Parrot parrot)
        {
            return _provider.SelectParrot(id, out parrot);
        }

        //Method to insert a new parrot into the data source
        public int Insert(Parrot newParrot)  
        {
            return _provider.Insert(newParrot);
        }

        //Method to update an existing parrot in the data source
        public int Update(Parrot existingParrot)  
        {
            return _provider.Update(existingParrot);
        }

        //Method to delete a parrot from the data source by ID
        public int Delete(string id)  
        {
            return _provider.Delete(id);
        }

        //Method to validate a parrot object
        private ProviderBase SetupProviderBase(string providerType)
        {
            switch (providerType.ToLower())
            {
                case "sqlite":
                    return new SQLiteProvider(new AnimalContext());
                default:
                    throw new ArgumentException("Invalid provider type specified");
            }
        }

        // Disposes the underlying data provider if it implements IDisposable.
        public void Dispose()
        {
            (_provider as IDisposable)?.Dispose();
        }
    }
}