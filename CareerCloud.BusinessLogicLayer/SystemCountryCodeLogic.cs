using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
  public class SystemCountryCodeLogic:BaseSystem<SystemCountryCodePoco>
    {
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository):base(repository)
        {

        }

        public override void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        public virtual SystemCountryCodePoco Get(string id)
        {
            return _repository.GetSingle(c => c.Code == id);
        }
        protected override void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, $"Code cannot be empty for SytemCountryCode{poco.Code}"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, $"Name cannot be empty for SystemCountryCode{poco.Name}"));
                }
                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }

            }
        }
    }
}
