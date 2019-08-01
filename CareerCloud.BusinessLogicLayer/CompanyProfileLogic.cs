using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
   public class CompanyProfileLogic:BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository):base(repository)
        {

        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            //string[] Domain = { ".ca", ".com", ".biz" };
            string[] validWebsites = new string[] {".ca",".com",".biz" };


            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite) || !validWebsites.Any(t => poco.CompanyWebsite.Contains(t)))
                    exceptions.Add(new ValidationException(600,$"Invalid Website{poco.CompanyWebsite}for Company Profile"));


                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, $"Invalid Contact Number for CompanyProfile {poco.ContactPhone}"));
                }
                else
                {
                    string[] phoneComponents = poco.ContactPhone.Split('-');
                    if (phoneComponents.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, $"Invalid Contact Number for CompanyProfile {poco.ContactPhone}"));
                    }
                    else
                    {
                        if (phoneComponents[0].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, $"Invalid Contact Number for CompanyProfile {poco.ContactPhone}"));
                        }
                        else if (phoneComponents[1].Length != 3)
                        {
                            exceptions.Add(new ValidationException(601, $"Invalid Contact Number for CompanyProfile {poco.ContactPhone}"));
                        }
                        else if (phoneComponents[2].Length != 4)
                        {
                            exceptions.Add(new ValidationException(601, $"Invalid Contact Number for CompanyProfile {poco.ContactPhone}"));
                        }
                    }
                }

            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
