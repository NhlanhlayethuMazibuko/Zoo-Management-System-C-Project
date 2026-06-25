using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using PA1.BL.Animals;

namespace PA1.DAL
{
    // SQLiteProvider class 
    public class SQLiteProvider : ProviderBase
    {
        //Context object to access Animal database
        private readonly AnimalContext _context;

        //Initialize the SQLiteProvider with a context
        public SQLiteProvider(AnimalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //Retrieve all parrots from the database and return them as a list
        public override List<Parrot> SelectAll()
        {
            List<Parrot> parrots = new List<Parrot>();

            try
            {
                //Retrieve all parrots from the database
                parrots = _context.Parrots.Include(p => p.Trainer).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving parrots: {ex.Message}");
            }

            return parrots; 
        }

        //Validate the parrot object to ensure it meets the required criteria
        public override int SelectParrot(string ID, out Parrot parrot)
        {
            parrot = null;
            int result = 0;

            try
            {
                //Retrieve the parrot with the specified ID
                parrot = _context.Parrots.Include(p => p.Trainer).FirstOrDefault(p => p.ID == ID);
                result = parrot != null ? 1 : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving parrot: {ex.Message}");
            }

            return result;
        }

        //Insert a new parrot into the database
        public override int Insert(Parrot newParrot)
        {
            int rowsAffected = 0;

            //Validate the new parrot object to ensure it meets the required criteria
            if (!ValidateParrot(newParrot))
            {
                return rowsAffected;
            }

            try
            {
                var trainerToSave = newParrot.Trainer;

                if (trainerToSave != null)
                {
                    var existingTrainer = _context.Trainers
                        .FirstOrDefault(t => t.Name == trainerToSave.Name);

                    if (existingTrainer != null)
                    {
                        _context.Entry(newParrot).Reference(p => p.Trainer).CurrentValue = existingTrainer;
                    }
                    else
                    {
                        _context.Trainers.Add(trainerToSave);
                    }
                }

                _context.Parrots.Add(newParrot);
                rowsAffected = _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Catches and prints any database specific errors 
                Console.WriteLine($"Error inserting parrot: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }

            return rowsAffected;
        }

        public override int Update(Parrot existingParrot)
        {
            int rowsAffected = 0;

            if (!ValidateParrot(existingParrot))
            {
                return rowsAffected; 
            }

            try
            {
                var trainerToSave = existingParrot.Trainer;

                if (trainerToSave != null)
                {
                    var existingTrainer = _context.Trainers
                        .FirstOrDefault(t => t.Name == trainerToSave.Name);

                    if (existingTrainer != null)
                    {
                        _context.Entry(existingParrot).Reference(p => p.Trainer).CurrentValue = existingTrainer;
                    }
                    else
                    {
                        _context.Trainers.Add(trainerToSave);
                    }
                }

                _context.Parrots.Update(existingParrot);
                rowsAffected = _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error updating parrot: {ex.Message}");
            }

            return rowsAffected;
        }

        // Deletes a parrot from the database by its ID
        public override int Delete(string ID)
        {
            int rowsAffected = 0; 

            try
            {
                var parrot = _context.Parrots.FirstOrDefault(p => p.ID == ID);
                if (parrot != null)
                {
                    _context.Parrots.Remove(parrot);
                    rowsAffected = _context.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error deleting parrot: {ex.Message}");
            }

            return rowsAffected; 
        }
        public void Dispose()
        {
            _context?.Dispose(); 
        }
    }
}