using System.Linq;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;

namespace DeveloperTest.Business
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public JobModel[] GetJobs()
        {
            return context.Jobs.Select(x => new JobModel
            {
                JobId = x.JobId,
                Engineer = x.Engineer,
                Customer = context.Customers.Where(c => c.CustomerId == x.CustomerId).Select(x => new CustomerModel
                {
                    CustomerId = x.CustomerId,
                    Name = x.Name,
                    Type = x.Type
                }).SingleOrDefault(),
                When = x.When
            }).ToArray();
        }

        public JobModel GetJob(int jobId)
        {
            return context.Jobs.Where(x => x.JobId == jobId).Select(x => new JobModel
            {
                JobId = x.JobId,
                Engineer = x.Engineer,
                Customer = context.Customers.Where(c => c.CustomerId == x.CustomerId).Select(x => new CustomerModel
                {
                    CustomerId = x.CustomerId,
                    Name = x.Name,
                    Type = x.Type
                }).SingleOrDefault(),
                When = x.When
            }).SingleOrDefault();
        }

        public JobModel CreateJob(BaseJobModel model)
        {
            var addedJob = context.Jobs.Add(new Job
            {
                Engineer = model.Engineer,
                CustomerId = model.Customer.CustomerId,
                When = model.When
            });

            context.SaveChanges();

            return new JobModel
            {
                JobId = addedJob.Entity.JobId,
                Engineer = addedJob.Entity.Engineer,
                Customer = context.Customers.Where(c => c.CustomerId == addedJob.Entity.CustomerId).Select(x => new CustomerModel
                {
                    CustomerId = x.CustomerId,
                    Name = x.Name,
                    Type = x.Type
                }).SingleOrDefault(),
                When = addedJob.Entity.When
            };
        }
    }
}
