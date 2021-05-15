using FDevQuiz.Commands;
using FDevQuiz.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FDevQuiz.Controllers
{
    [Controller]
    [Route("users")]
    public class userController : ControllerBase
    {
        private ICollection<T> loadData<T>()
        {
            using var openStream = System.IO.File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json"));
            return JsonSerializer.DeserializeAsync<ICollection<T>>(openStream, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }).Result;
        }
        private ICollection<T> writeData<T>(ICollection<T> data)
        {
            using var createStream = System.IO.File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "users.json"));
            var writedUser = JsonSerializer.SerializeAsync<ICollection<T>>(createStream,data, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return data;
        }

        [HttpGet]
        public IActionResult getUsers()
        {
            var users = loadData<User>();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult getUser([FromRoute] long id)
        {
            var users = loadData<User>();
            var user = users.Where(user => user.Id == id);

            return Ok(user);
        }

        [HttpPost]
        public IActionResult postUser([FromBody] CommandUser command)
        {
            if (command == null)
                return BadRequest("Arquivo mal formatado");

            if (string.IsNullOrEmpty(command.Name))
                throw new Exception("Nome do usuário obrigatório");


            var users = loadData<User>();
            var user = new User()
            {
                Name = command.Name,
                Score = 0,
                ImgUrl = command.ImgUrl
            };

            user.Id = users.Select(user => user.Id).ToList().Max() + 1;
            users.Add(user);
            writeData<User>(users);

            return Created("usuarios/{id}", user);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] long id, CommandUpdateUser command)
        {
            var users = loadData<User>();
            var user = users.Where(user => user.Id == id).FirstOrDefault();

            users.Remove(user);

            user.Name = command.Name;
            user.ImgUrl = command.ImgUrl;

            users.Add(user);

            writeData(users);

            return NoContent();
        }

        [HttpDelete("/{id}")]
        public IActionResult deleteUser([FromRoute] long id)
        {
            var users = loadData<User>();
            users = users.Where(user => user.Id != id).ToList();

            writeData(users);

            return NoContent();
        }
    }
}
