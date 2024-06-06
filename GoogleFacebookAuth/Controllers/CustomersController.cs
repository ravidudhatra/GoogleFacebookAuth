using Azure.Core;
using GoogleFacebookAuth.Data;
using GoogleFacebookAuth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace GoogleFacebookAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly GoogleFacebookAuthContext _context;

        public CustomersController(GoogleFacebookAuthContext context)
        {
            _context = context;
        }

            
        // Data load on serverside using datatable
        [HttpPost]
        public IActionResult GetCustomers()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var customerData = (from tempcustomer in _context.Customers select tempcustomer);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.FirstName.Contains(searchValue)
                                                || m.LastName.Contains(searchValue)
                                                || m.Contact.Contains(searchValue)
                                                || m.Email.Contains(searchValue));
                }
                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);

            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}



