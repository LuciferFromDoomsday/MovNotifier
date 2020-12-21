using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovNotifier.Services;
using MovNotifier.Utilities;
using MovNotifier.Utilities.Validations;

namespace MovNotifier.Models
{
    public class Genre : AbstractValidatableObject
    {

        public int Id { get; set; }
        
        [Required]
        [MakeMeUpperCase]
        public string Name { get; set; }
        public IList<ListGenres> Movies { get; set; }
        public Genre()
        {
        }
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
        CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();

            if (Name.Length < 3)
                errors.Add(new ValidationResult("Nane length cannot be less than 3", new[] { nameof(Name) }));

            if (Name.Length > 30)
                errors.Add(new ValidationResult($"Name: {Name} not allowed to work. Max name length is 30 ", new[] { nameof(Name) }));
                

            // Database call through service for validation
            var dbContext = new GenreServiceImpl();
            bool isExists = false;
            try
            {
                isExists = await dbContext.IsGenreExist(Name);
            }
            catch
            {
                 isExists = false;
            }
                if (isExists)
                {
                    errors.Add(new ValidationResult("Genre exist", new[] { nameof(Genre) }));
                }
            
 
            return errors;
        }
    }
}

