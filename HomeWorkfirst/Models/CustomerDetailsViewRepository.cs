using System;
using System.Linq;
using System.Collections.Generic;
	
namespace HomeWorkfirst.Models
{   
	public  class CustomerDetailsViewRepository : EFRepository<CustomerDetailsView>, ICustomerDetailsViewRepository
	{

	}

	public  interface ICustomerDetailsViewRepository : IRepository<CustomerDetailsView>
	{

	}
}