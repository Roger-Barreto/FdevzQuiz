using FDevQuiz.Commands;
using FDevQuiz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FDevQuiz.Controllers
{
    [Controller]
    [Route("quizzes")]
    public class QuizController : ControllerBase
    {
        private ICollection<T> loadData<T>()
        {
            using var openStream = System.IO.File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "quizzes.json"));
            return JsonSerializer.DeserializeAsync<ICollection<T>>(openStream, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }).Result;
        }
        private ICollection<T> writeData<T>(ICollection<T> data)
        {
            using var createStream = System.IO.File.Create(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "quizzes.json"));
            var writedUser = JsonSerializer.SerializeAsync<ICollection<T>>(createStream, data, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return data;
        }

        [HttpGet]
        public IActionResult getQuiz()
        {
            var quizzes = loadData<Quiz>();

            return Ok(quizzes);
        }

        [HttpGet("{id}")]
        public IActionResult getQuiz([FromRoute] long id)
        {
            var quizzes = loadData<Quiz>();
            var quiz = quizzes.Where(quiz => quiz.Id == id);

            return Ok(quiz);
        }

        [HttpPost]
        public IActionResult PostQuiz([FromBody] CommandQuiz command)
        {
            if (command == null)
                return BadRequest("Arquivo mal formatado");

            var quizzes = loadData<Quiz>();
            var quizz = new Quiz()
            {
                DifficultyId = command.DifficultyId,
                Title = command.Title,
                Questions = command.Questions
            };

            quizz.Id = quizzes.Select(quizz => quizz.Id).ToList().Max() + 1;
            quizzes.Add(quizz);
            writeData(quizzes);

            return Created("quizzes/{id}", quizz);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] long id, CommandQuiz command)
        {
            var quizzes = loadData<Quiz>();
            var quizz = quizzes.Where(quizz => quizz.Id == id).FirstOrDefault();

            quizzes.Remove(quizz);

            quizz.Title = command.Title;
            quizz.DifficultyId = command.DifficultyId;
            quizz.Questions = command.Questions;

            quizzes.Add(quizz);

            writeData(quizzes);

            return NoContent();
        }

        [HttpDelete("/{id}")]
        public IActionResult DeleteQuiz([FromRoute] long id)
        {
            var quizzes = loadData<Quiz>();
            quizzes = quizzes.Where(quizz => quizz.Id != id).ToList();

            writeData(quizzes);

            return NoContent();
        }
    }
}
