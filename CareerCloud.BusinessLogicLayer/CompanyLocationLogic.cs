using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic:BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository):base(repository)
        {

        }
        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach(var Poco in pocos)
            {
                if(string.IsNullOrEmpty(Poco.CountryCode))
                {
                    exceptions.Add(new ValidationException(500, $"CountryCode {Poco.CountryCode} for Company Location cannot be empty"));
                }
                if(string.IsNullOrEmpty(Poco.Province))
                {
                    exceptions.Add(new ValidationException(501, $"Province{Poco.Province} for Company Location cannot be empty"));
                }
                if(string.IsNullOrEmpty(Poco.Street))
                {
                    exceptions.Add(new ValidationException(502, $"Street {Poco.Street} for Company Location cannot be empty"));
                }
                if(string.IsNullOrEmpty(Poco.City))
                {
                    exceptions.Add(new ValidationException(503, $"City {Poco.City} for Company Profile cannot be empty"));
                }
                if(string.IsNullOrEmpty(Poco.PostalCode))
                {
                    exceptions.Add(new ValidationException(504, $"PostalCode {Poco.PostalCode} for Company Location cannot be empty"));
                }
            }
            if(exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
