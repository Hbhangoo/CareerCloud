using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic:BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository):base(repository)
        {

        }
        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (String.IsNullOrEmpty(poco.CompanyDescription)||poco.CompanyDescription.Length<=2)
                {
                    exceptions.Add(new ValidationException(107, $"CompanyDescription{poco.CompanyDescription} must be greater than 2 characters for CompanyDescription"));
                }
                if (String.IsNullOrEmpty(poco.CompanyName)||poco.CompanyName.Length <= 2)
                {
                    exceptions.Add(new ValidationException(106, $"Company Name{poco.CompanyName} must be greater than 2 characters for CompanyDescription"));
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
