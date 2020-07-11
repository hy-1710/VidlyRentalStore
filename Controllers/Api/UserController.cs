using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidlyStore.Dtos;
using VidlyStore.Models;

namespace VidlyStore.Controllers.Api
{
    public class UserController : ApiController
    {
        private VidlyContext _context;


        public UserController()
        {
            _context = new VidlyContext();
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET /api/user
        public IHttpActionResult  GetUsers()
        {
            var userQuery = _context.usersList.ToList();

            
            var userDto = userQuery.ToList().Select(Mapper.Map<User, UserDto>);
            return Ok(userDto);

          }


        [HttpPost]
        public IHttpActionResult CreateUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = Mapper.Map<UserDto, User>(userDto);
            _context.usersList.Add(user);
            _context.SaveChanges();

            userDto.Id = user.Id;

            //use created to redirect created value.
            return Created(new Uri(Request.RequestUri + "/" + user.Id), userDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            var c = _context.usersList.SingleOrDefault(cs => cs.Id == id);
            if (c == null)
                return NotFound();

            _context.usersList.Remove(c);
            _context.SaveChanges();

            return Ok();

        }


        [HttpPut]
        public IHttpActionResult UpdateUser(int id, UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var c = _context.usersList.SingleOrDefault(cs => cs.Id == id);
            if (c == null)
                return NotFound();

            Mapper.Map(userDto, c);
           
            _context.SaveChanges();
            return Ok();
        }

        public IHttpActionResult GetUser(int id)
        {
            var user = _context.usersList.SingleOrDefault(c => c.Id == id);

            if (user == null)
                return NotFound();
            return Ok(Mapper.Map<User, UserDto>(user));
        }




    }
}
