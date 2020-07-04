using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using VidlyStore.Dtos;
using VidlyStore.Models;

namespace VidlyStore.Controllers.Api
{
    public class CustomerController : ApiController
    {

        private VidlyContext _context;


        public CustomerController()
        {
            _context = new VidlyContext();
        }

        //GET /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customerQuery = _context.customers.Include(c => c.MemberShipType);

            if(!string.IsNullOrEmpty(query))
            {
                customerQuery = customerQuery.Where(c => c.Name.Contains(query));
            }

            var customerDto = customerQuery.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDto);
           // return _context.customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        //GET /api/customers/1
        public IHttpActionResult GetCustomer( int id)
        {
            var customer = _context.customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }


        //POST /api/customer
        [HttpPost]
        [Authorize(Roles =RoleName.CanManageMovies)]
        public IHttpActionResult  CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            //use created to redirect created value.
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/customer/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var c = _context.customers.SingleOrDefault(cs => cs.Id == id);
            if (c == null)
                return NotFound();

            Mapper.Map(customerDto, c);
            //c.Name = customer.Name;
            //c.DOB = customer.DOB;
            //c.IsSubscribeToNewsLetter = customer.IsSubscribeToNewsLetter;
            //c.MemberShipTypeId = customer.MemberShipTypeId;

            _context.SaveChanges();
            return Ok();
        }


        //DELETE /api/customer/1
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var c = _context.customers.SingleOrDefault(cs => cs.Id == id);
            if (c == null)
                return NotFound();

            _context.customers.Remove(c);
            _context.SaveChanges();

            return Ok();
            
        }
    }
}
